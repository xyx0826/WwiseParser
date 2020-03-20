using WwiseParser.Structures.Objects.HIRC;

namespace WwiseParser.Structures.Sections
{
    class HIRCSection : SectionBase
    {
        public HIRCSection()
        {
            Name = SectionName.HIRC;
        }

        /// <summary>
        /// <para>The count of objects in the section.</para>
        /// </summary>
        public uint ObjectCount { get; set; }

        /// <summary>
        /// <para>The objects in the section.</para>
        /// </summary>
        HIRCObjectBase[] Objects { get; set; }
    }
}
