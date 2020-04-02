namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class DialogueEvent : HIRCObjectBase
    {
        public DialogueEvent(int length) : base(HIRCObjectType.DialogueEvent, (uint)length)
        {

        }

        /// <summary>
        /// <para>The probability of the Dialogue Event.</para>
        /// <para>Location: Dialogue Event Editor > Settings > Probability</para>
        /// </summary>
        public byte Probability { get; set; }

        /// <summary>
        /// <para>The count of state or switch groups used as arguments for the Dialogue Event.</para>
        /// </summary>
        public uint ArgumentCount { get; set; }

        /// <summary>
        /// <para>IDs of state or switch groups used as arguments for the Dialogue Event.</para>
        /// </summary>
        public uint[] ArgumentIds { get; set; }

        /// <summary>
        /// Unknown value. Seems to be always zero.
        /// </summary>
        public byte Unknown_1 { get; set; }

        /// <summary>
        /// Unknown value. Seems to be always zero.
        /// </summary>
        public byte Unknown_2 { get; set; }

        /// <summary>
        /// Unknown value. Seems to be always zero.
        /// </summary>
        public byte Unknown_3 { get; set; }

        /// <summary>
        /// <para>The length of the Path data section at the end of the object, in bytes.</para>
        /// </summary>
        public uint PathSectionLength { get; set; }

        /// <summary>
        /// <para>Whether to use weighted association mode. Otherwise use best match.</para>
        /// <para>Located at: Dialogue Event Editor > Mode:</para>
        /// </summary>
        public bool UseWeighted { get; set; }

        /// <summary>
        /// <para>The association paths of the Dialogue Event.</para>
        /// <para>Located at: Dialogue Event Editor</para>
        /// </summary>
        public AudioPathNode Paths { get; set; }
    }
}
