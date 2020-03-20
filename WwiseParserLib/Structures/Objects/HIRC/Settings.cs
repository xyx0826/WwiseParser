using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Settings : HIRCObject
    {
        public Settings(int length) : base(HIRCObjectType.Settings, (uint)length)
        {

        }

        /// <summary>
        /// <para>The count of parameters of the settings.</para>
        /// </summary>
        public byte ParameterCount { get; set; }

        /// <summary>
        /// <para>The types of parameters of the settings.</para>
        /// </summary>
        public AudioParameterType[] ParameterTypes { get; set; }

        /// <summary>
        /// <para>The values of parameters of the settings.</para>
        /// </summary>
        public float[] ParameterValues { get; set; }
    }
}
