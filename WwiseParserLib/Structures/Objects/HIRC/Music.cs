namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Music : Actor
    {
        public Music(HIRCObjectType type, int length) : base(type, (uint)length)
        {

        }

        /// <summary>
        /// <para>The tempo (BPM) of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Tempo</para>
        /// </summary>
        public float Tempo { get; set; }

        /// <summary>
        /// <para>The upper part of the time signature of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureUpper { get; set; }

        /// <summary>
        /// <para>The lower part of the time signature of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureLower { get; set; }
    }
}
