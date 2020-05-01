namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class ActorMixer : Actor
    {
        public ActorMixer(int length) : base(HIRCObjectType.ActorMixer, (uint)length)
        {

        }

        ///// <summary>
        ///// <para>The count of children of the Actor-Mixer.</para>
        ///// </summary>
        //public uint ChildCount { get; set; }

        ///// <summary>
        ///// <para>IDs of children of the Actor-Mixer.</para>
        ///// </summary>
        //public uint[] ChildIds { get; set; }
    }
}
