using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class ActorMixer : HIRCObject
    {
        public ActorMixer(int length) : base(HIRCObjectType.ActorMixer, (uint)length)
        {

        }

        /// <summary>
        /// <para>Additional properties of the Actor-Mixer.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// <para>The count of children of the Actor-Mixer.</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>IDs of children of the Actor-Mixer.</para>
        /// </summary>
        public uint[] ChildIds { get; set; }
    }
}
