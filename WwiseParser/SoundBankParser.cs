using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WwiseParserLib.Structures;
using WwiseParserLib.Structures.Objects.HIRC;
using WwiseParserLib.Structures.Sections;

namespace WwiseParser
{
    /// <summary>
    /// A parser responsible for parsing sections of a SoundBank.
    /// </summary>
    class SoundBankParser
    {
        private SoundBank _soundBank;

        private string _outputPath;

        private bool _parseStmg;

        private bool _parseHirc;

        private bool _isInspector;

        private SoundBankParser() { }

        public SoundBankParser(string filePath, bool skipSTMG, bool skipHIRC, bool isInspector)
        {
            ValidateSoundBank(filePath);
            EnsureOutputPath(filePath);

            _parseStmg = !skipSTMG;
            _parseHirc = !skipHIRC;
            _isInspector = isInspector;
        }

        public void Parse()
        {
            SoundBankSection section;
            if (_parseHirc)
            {
                section = _soundBank.ParseSection(SoundBankSectionName.HIRC);
                if (_isInspector)
                {
                    ParseHIRCInspector(section as HIRCSection);
                }
                else
                {
                    WriteJson(section, "hirc");
                }
            }

            if (_parseStmg)
            {
                section = _soundBank.ParseSection(SoundBankSectionName.STMG);
                WriteJson(section, "stmg");
            }
        }

        private void ParseHIRCInspector(HIRCSection hircSection)
        {
            var types = hircSection.Objects.GroupBy(x => x.Type);
            foreach (var type in types)
            {
                // Append type ID to type name if unknown
                var typeName = Enum.GetName(typeof(HIRCObjectType), type.Key);
                var typePath = Path.Combine(_outputPath, typeName);
                Directory.CreateDirectory(typePath);

                // Create folder for each type
                foreach (var obj in type)
                {
                    byte[] objBlob;
                    var objPath = Path.Combine(typePath, obj.Id.ToString("X8") + '_' + obj.Id.ToString());
                    if (!(obj is Unknown))
                    {
                        // Dump JSON for parsed types
                        objBlob = GetJsonBytes(obj);
                        objPath += ".json";
                    }
                    else
                    {
                        // Dump blob for unknown types
                        objBlob = (obj as Unknown).Blob;
                        objPath += ".bin";
                    }

                    if (File.Exists(objPath))
                    {
                        if (HasFileChanged(objPath, objBlob))
                        {
                            Console.WriteLine($"Changed {typeName}: " + objPath);
                            OverwriteFile(objPath, objBlob);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Created {typeName}: " + objPath);
                        File.WriteAllBytes(objPath, objBlob);
                    }
                }
            }
        }

        static void OverwriteFile(string path, byte[] blob)
        {
            var fileExtension = Path.GetExtension(path);
            var oldFile = path.Replace(fileExtension, ".old" + fileExtension);
            File.Delete(oldFile);
            File.Move(path, oldFile);
            File.WriteAllBytes(path, blob);
        }

        private void ValidateSoundBank(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            // Check for BKHD header
            _soundBank = new FileSoundBank(filePath);
            var bkhdSection = _soundBank.ParseSection(SoundBankSectionName.BKHD);
            if (bkhdSection == null)
            {
                throw new Exception("The specified file does not have a valid SoundBank header.");
            }
        }

        private void EnsureOutputPath(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            _outputPath = Path.Combine("./", fileName);
            Directory.CreateDirectory(_outputPath);
        }

        private void WriteJson(object obj, string fileName)
        {
            var path = Path.Combine(_outputPath, fileName + ".json");
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new StringEnumConverter());
            File.WriteAllText(path, json);
        }

        private byte[] GetJsonBytes(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, new StringEnumConverter());
            return Encoding.UTF8.GetBytes(json);
        }

        private static bool HasFileChanged(string path, byte[] blob)
        {
            using (var md5 = MD5.Create())
            {
                var originalBlob = File.ReadAllBytes(path);
                return !Enumerable.SequenceEqual(md5.ComputeHash(originalBlob), md5.ComputeHash(blob));
            }
        }
    }
}
