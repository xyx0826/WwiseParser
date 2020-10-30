using System.IO;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Parsers
{
    /// <summary>
    /// Parser for BKHD (header) chunks.
    /// </summary>
    public static class BKHDParser
    {
        /// <summary>
        /// Parses a BKHD chunk.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHeaderChunk Parse(byte[] blob)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var bkhdSection = new SoundBankHeaderChunk(blob.Length);
                bkhdSection.SoundBankVersion = reader.ReadUInt32();
                bkhdSection.SoundBankId = reader.ReadUInt32();

                return bkhdSection;
            }
        }
    }
}
