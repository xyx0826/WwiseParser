using System.IO;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Structures.SoundBanks
{
    public class FileSoundBank : SoundBank
    {
        public string FilePath { get; private set; }

        public FileSoundBank(string filePath) : base()
        {
            FilePath = filePath;
        }

        public override byte[] ReadSection(SoundBankChunkType name)
        {
            using (var reader = new BinaryReader(File.OpenRead(FilePath)))
            {
                while (reader.PeekChar() > -1)
                {
                    var sectionName = reader.ReadUInt32();
                    var sectionLength = reader.ReadUInt32();

                    if (sectionName == (uint)name)
                    {
                        // Section found
                        return reader.ReadBytes((int)sectionLength);
                    }
                    else
                    {
                        // Not the section we're looking for
                        reader.BaseStream.Seek(sectionLength, SeekOrigin.Current);
                    }
                }

                // Section does not exist
                return null;
            }
        }
    }
}
