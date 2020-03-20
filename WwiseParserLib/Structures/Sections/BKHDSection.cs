namespace WwiseParserLib.Structures.Sections
{
    public class BKHDSection : SoundBankSection
    {
        public BKHDSection(int length) : base(SoundBankSectionName.BKHD, (uint)length)
        {

        }

        public uint SoundBankVersion { get; set; }

        public uint SoundBankId { get; set; }
    }
}
