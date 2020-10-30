using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Chunks
{
    /// <summary>
    /// HIRC chunk containing the majority of Wwise objects.
    /// </summary>
    public class SoundBankHierarchyChunk : SoundBankChunk
    {
        /// <summary>
        /// Creates a new HIRC chunk.
        /// </summary>
        /// <param name="length">The data length of the chunk excluding the type magic.</param>
        public SoundBankHierarchyChunk(int length) : base(SoundBankChunkType.HIRC, (uint)length)
        {

        }

        /// <summary>
        /// <para>The count of objects in the section.</para>
        /// </summary>
        public uint ObjectCount { get; set; }

        /// <summary>
        /// <para>The objects in the section.</para>
        /// </summary>
        public HIRCObjectBase[] Objects { get; set; }
    }
}
