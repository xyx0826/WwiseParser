using System.IO;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Parsers.BKHD
{
    public static class BKHDParser
    {
        public static BKHDSection Parse(byte[] blob)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var bkhdSection = new BKHDSection(blob.Length);
                bkhdSection.SoundBankVersion = reader.ReadUInt32();
                bkhdSection.SoundBankId = reader.ReadUInt32();

                return bkhdSection;
            }
        }
    }
}
