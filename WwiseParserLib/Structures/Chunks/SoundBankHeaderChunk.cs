namespace WwiseParserLib.Structures.Chunks
{
    /// <summary>
    /// BKHD chunk containing SoundBank metadata including version and ID.
    /// </summary>
    public class SoundBankHeaderChunk : SoundBankChunk
    {
        /// <summary>
        /// Creates a new BKHD chunk.
        /// </summary>
        /// <param name="length">The data length of the chunk excluding the type magic.</param>
        public SoundBankHeaderChunk(int length) : base(SoundBankChunkType.BKHD, (uint)length)
        {

        }

        /// <summary>
        /// The version of the SoundBank.
        /// Wwise v2013.2.10 generates version 0x58.
        /// An unknown version of Wwise v2016 generates version 0x71.
        /// Wwise v2016.1.6 generates version 0x76.
        /// </summary>
        public uint SoundBankVersion { get; set; }

        /// <summary>
        /// The ID of the SoundBank.
        /// It is the FNV-1 hash of the name of the SoundBank.
        /// </summary>
        public uint SoundBankId { get; set; }
    }
}
