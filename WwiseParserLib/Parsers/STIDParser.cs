using System.IO;
using System.Text;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Parsers
{
    /// <summary>
    /// Parser for STID (SoundBank ID-to-String) chunks.
    /// </summary>
    public static class STIDParser
    {
        public static SoundBankIDToStringChunk Parse(byte[] blob)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var stidSection = new SoundBankIDToStringChunk(blob.Length);
                stidSection.Unknown = reader.ReadUInt32();
                stidSection.SoundBankMappingCount = reader.ReadUInt32();
                var soundBankMappings = new SoundBankMapping[stidSection.SoundBankMappingCount];
                for (int i = 0; i < stidSection.SoundBankMappingCount; i++)
                {
                    var soundBankMapping = new SoundBankMapping();
                    soundBankMapping.NameLength = reader.ReadByte();
                    soundBankMapping.Id = reader.ReadUInt32();
                    soundBankMapping.Name = Encoding.UTF8.GetString(reader.ReadBytes(soundBankMapping.NameLength));
                    soundBankMappings[i] = soundBankMapping;
                }
                stidSection.SoundBankMappings = soundBankMappings;
                return stidSection;
            }
        }
    }
}
