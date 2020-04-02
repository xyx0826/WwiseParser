namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Unknown : HIRCObjectBase
    {
        public Unknown(HIRCObjectType type, int length) : base(type, (uint)length)
        {

        }

        public byte[] Blob { get; set; }
    }
}
