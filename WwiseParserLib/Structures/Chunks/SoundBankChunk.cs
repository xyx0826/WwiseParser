namespace WwiseParserLib.Structures.Chunks
{
    /// <summary>
    /// Base class representing a Wwise SoundBank chunk.
    /// </summary>
    public abstract class SoundBankChunk
    {
        private SoundBankChunk() { }

        /// <summary>
        /// Creates a new SoundBank chunk with the specified name and length.
        /// </summary>
        /// <param name="type">The type of the chunk.</param>
        /// <param name="length">The length, in bytes, of the chunk excluding the magic.</param>
        protected SoundBankChunk(SoundBankChunkType type, uint length)
        {
            Type = type;
            Length = length;
        }

        /// <summary>
        /// The type of the chunk.
        /// </summary>
        public SoundBankChunkType Type { get; private set; }

        /// <summary>
        /// The data length of the chunk excluding the type magic.
        /// </summary>
        public uint Length { get; private set; }
    }

    /// <summary>
    /// Types of SoundBank chunks.
    /// </summary>
    public enum SoundBankChunkType : uint
    {
        /// <summary>
        /// The SoundBank Header chunk.
        /// Contains SoundBank metadata.
        /// </summary>
        BKHD = 0x44484B42,

        /// <summary>
        /// The SoundBank Data Index chunk.
        /// Contains references to embedded Wwise Encoded Media.
        /// </summary>
        DIDX = 0x58444944,

        /// <summary>
        /// The SoundBank Data chunk.
        /// Contains embedded Wwise Encoded Media.
        /// </summary>
        DATA = 0x41544144,

        /// <summary>
        /// The SoundBank Environment Settings chunk.
        /// Contains information of the platform that the SoundBank is for.
        /// </summary>
        ENVS = 0x53564E45,

        /// <summary>
        /// The SoundBank Hierarchy chunk.
        /// Contains general Wwise objects.
        /// </summary>
        HIRC = 0x43524948,

        /// <summary>
        /// The SoundBank ID to String chunk.
        /// Contains SoundBank ID to name mapping.
        /// </summary>
        STID = 0x44495453,

        /// <summary>
        /// The SoundBank State Manager chunk.
        /// Exists in Init.bnk. Contains project settings and Game Syncs.
        /// </summary>
        STMG = 0x474D5453
    }
}
