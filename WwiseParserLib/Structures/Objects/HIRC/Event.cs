namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Event : HIRCObject
    {
        public Event(int length) : base(HIRCObjectType.Event, (uint)length)
        {

        }

        /// <summary>
        /// <para>The count of Event Actions belonging to the Event.</para>
        /// <para>Determined by: Event Editor > Event Actions</para>
        /// </summary>
        public uint ActionCount { get; set; }

        /// <summary>
        /// <para>IDs of Event Actions belonging to the Event.</para>
        /// <para>Located at: Event Editor > Event Actions</para>
        /// </summary>
        public uint[] ActionIds { get; set; }
    }
}
