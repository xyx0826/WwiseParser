using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WwiseParserLib.Structures.Objects.HIRC;
using WwiseParserLib.Structures.Sections;
using WwiseParserLib.Structures.SoundBanks;

namespace WwiseParser
{
    /// <summary>
    /// A parser responsible for parsing sections of a SoundBank.
    /// </summary>
    class SoundBankParser
    {
        public SoundBank SoundBank { get; private set; }

        private string _outputPath;

        private bool _parseStmg;

        private bool _parseHirc;

        private bool _isInspector;

        private static JsonSerializerSettings _jsonSettings;

        private SoundBankParser()
        {
            // Initialize universal JSON serializer settings
            _jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            _jsonSettings.Converters.Add(new StringEnumConverter());

            // Initialize resolver to ignore invalid fields
            var jsonResolver = new PropertySerializationContractResolver();
            jsonResolver.IgnoreProperty(typeof(Sound), "ChildCount", "ChildIds");
            jsonResolver.IgnoreProperty(typeof(MusicTrack), "ChildCount", "ChildIds");
            _jsonSettings.ContractResolver = jsonResolver;
        }

        /// <summary>
        /// Creates a new parser for a file-based SoundBank.
        /// </summary>
        /// <param name="filePath">The path to the SoundBank file.</param>
        /// <param name="skipSTMG">Whether to skip STMG section.</param>
        /// <param name="skipHIRC">Whether to skip HIRC section.</param>
        /// <param name="isInspector">Whether to parse in inspector mode.</param>
        public SoundBankParser(string filePath, bool skipSTMG, bool skipHIRC, bool isInspector) : this()
        {
            ValidateSoundBank(filePath);
            EnsureOutputPath(filePath);

            _parseStmg = !skipSTMG;
            _parseHirc = !skipHIRC;
            _isInspector = isInspector;
        }

        /// <summary>
        /// Parses the SoundBank and dumps parsed sections to JSON files.
        /// </summary>
        public void Parse()
        {
            SoundBankSection section;
            if (_parseHirc)
            {
                section = SoundBank.GetSection(SoundBankSectionName.HIRC);
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
                section = SoundBank.GetSection(SoundBankSectionName.STMG);
                WriteJson(section, "stmg");
            }
        }

        /// <summary>
        /// Parses the HIRC section in inspector mode,
        /// dumping documented Wwise objects to JSON files and undocumented ones to binary files.
        /// </summary>
        /// <param name="hircSection">The HIRC section to parse.</param>
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
                    // File name example: .\ActorMixer\DEADBEEF_3735928559.(?)
                    var dumpPath = Path.Combine(typePath,
                        obj.Id.ToString("X8") + '_' + obj.Id.ToString());
                    byte[] blob;
                    if (!(obj is Unknown))
                    {
                        // Dump JSON for parsed types
                        blob = GetJsonBytes(obj);
                        dumpPath += ".json";
                    }
                    else
                    {
                        // Dump blob for unknown types
                        blob = (obj as Unknown).Blob;
                        dumpPath += ".bin";
                    }

                    if (File.Exists(dumpPath))
                    {
                        // Diff it, unchanged objects are skipped
                        if (HasFileChanged(dumpPath, blob))
                        {
                            Console.WriteLine($"Changed {typeName}: " + dumpPath);
                            OverwriteFile(dumpPath, blob);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Created {typeName}: " + dumpPath);
                        File.WriteAllBytes(dumpPath, blob);
                    }
                }
            }
        }

        #region Helpers
        /// <summary>
        /// Creates the SoundBank object from the specified file path
        /// and validates that the file is a SoundBank with a BKHD section.
        /// </summary>
        /// <param name="filePath">The path to the SoundBank.</param>
        private void ValidateSoundBank(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            // Check for BKHD header
            SoundBank = new FileSoundBank(filePath);
            var bkhdSection = SoundBank.ParseSection(SoundBankSectionName.BKHD);
            if (bkhdSection == null)
            {
                throw new Exception("The specified file does not have a valid SoundBank header.");
            }
        }

        /// <summary>
        /// Ensures that the output path for the current SoundBank is created.
        /// </summary>
        /// <param name="filePath">The output path for the SoundBank.</param>
        private void EnsureOutputPath(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            _outputPath = Path.Combine("./", fileName);
            Directory.CreateDirectory(_outputPath);
        }

        /// <summary>
        /// Serializes the specified object to JSON and writes it to file.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="fileName">The output file.</param>
        private void WriteJson(object obj, string fileName)
        {
            var path = Path.Combine(_outputPath, fileName + ".json");
            var json = JsonConvert.SerializeObject(obj, _jsonSettings);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Serializes the specified object to JSON bytes.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The UTF-8 JSON bytes.</returns>
        private byte[] GetJsonBytes(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, _jsonSettings);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Checks whether a binary file has changed.
        /// </summary>
        /// <param name="path">The path to the original file.</param>
        /// <param name="blob">The new binary blob.</param>
        /// <returns>Whether the file is different from the blob.</returns>
        private static bool HasFileChanged(string path, byte[] blob)
        {
            using (var md5 = MD5.Create())
            {
                var originalBlob = File.ReadAllBytes(path);
                return !Enumerable.SequenceEqual(
                    md5.ComputeHash(originalBlob), md5.ComputeHash(blob));
            }
        }

        /// <summary>
        /// Safely overwrites the specified file and create a backup.
        /// </summary>
        /// <param name="path">The file to overwrite.
        /// A backup will be created with ".old" inserted before its original extension.</param>
        /// <param name="blob">The new content of the file to write.</param>
        private static void OverwriteFile(string path, byte[] blob)
        {
            var fileExtension = Path.GetExtension(path);
            var oldFile = path.Replace(fileExtension, ".old" + fileExtension);
            File.Delete(oldFile);
            File.Move(path, oldFile);
            File.WriteAllBytes(path, blob);
        }
        #endregion
    }
}
