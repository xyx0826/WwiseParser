namespace WwiseParserLib.Structures.Chunks
{
    /// <summary>
    /// STID chunk containing SoundBank ID to name mappings.
    /// </summary>
    public class SoundBankIDToStringChunk : SoundBankChunk
    {
        /// <summary>
        /// Creates a new STID chunk.
        /// </summary>
        /// <param name="length">The data length of the chunk excluding the type magic.</param>
        public SoundBankIDToStringChunk(int length) : base(SoundBankChunkType.STID, (uint)length)
        {

        }

        /// <summary>
        /// Unknown value. Appears to always be 1.
        /// </summary>
        public uint Unknown { get; set; }

        /// <summary>
        /// Count of SoundBank ID to name mappings.
        /// </summary>
        public uint SoundBankMappingCount { get; set; }

        /// <summary>
        /// SoundBank ID to name mappings.
        /// </summary>
        public SoundBankMapping[] SoundBankMappings { get; set; }
    }

    /// <summary>
    /// A SoundBank ID to name mapping.
    /// </summary>
    public class SoundBankMapping
    {
        /// <summary>
        /// The length, in bytes, of the SoundBank name without a null terminator.
        /// </summary>
        public byte NameLength { get; set; }

        /// <summary>
        /// The ID of the SoundBank.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// The name of the SoundBank.
        /// </summary>
        public string Name { get; set; }
    }
}
