using System;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Container : HIRCObject
    {
        public Container(int length) : base(HIRCObjectType.Container, (uint)length)
        {

        }

        /// <summary>
        /// <para>Additional properties of the Random Container.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// <para>The number of loops of the Random Container.</para>
        /// <para>Zero if infinite. <see cref="uint"/>?</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Loop > No. of loops</para>
        /// </summary>
        public ushort LoopCount { get; set; }

        /// <summary>
        /// Unknown value. Appears to be zero.
        /// </summary>
        public uint Unknown_1 { get; set; }

        /// <summary>
        /// <para>The duration of transition, in milliseconds, of the Random Container.</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Play Mode > Transitions > Duration</para>
        /// </summary>
        public float TransitionDuration { get; set; }

        /// <summary>
        /// Eight unknown bytes. Appears to be zero.
        /// </summary>
        public byte[] Unknown_2 { get; set; }

        /// <summary>
        /// <para>The count of last played audio to avoid repeating.</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Play Type > Random > Avoid repeating last ... played</para>
        /// </summary>
        public ushort AvoidLastPlayedCount { get; set; }

        /// <summary>
        /// <para>The type of transition of the Random Container.</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Play Mode > Transitions > Type</para>
        /// </summary>
        public ContainerTransitionType Transition { get; set; }

        /// <summary>
        /// <para>Whether the Random Container should shuffle random audio.</para>
        /// <para>Otherwise standard.</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Play Type > Random > Shuffle</para>
        /// </summary>
        public bool Shuffle { get; set; }

        /// <summary>
        /// <para>The play type of the Random Container.</para>
        /// <para>Located at: Random/Sequence Container Property Editor > Play Type</para>
        /// </summary>
        public ContainerPlayType PlayType { get; set; }

        public ContainerSequenceBehavior Behavior { get; set; }

        /// <summary>
        /// <para>The count of children of the Random Container.</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>IDs of children of the Random Container.</para>
        /// </summary>
        public uint[] ChildIds { get; set; }

        /// <summary>
        /// Unknown value. Appears to be zero.
        /// </summary>
        public ushort Unknown_3 { get; set; }
    }

    public enum ContainerTransitionType : byte
    {
        NoTransition,
        CrossFadeAmp,
        CrossFadePower,
        Delay,
        SampleAccurate,
        TriggerRate
    }

    public enum ContainerPlayType : byte
    {
        Random,
        Sequence
    }

    [Flags]
    public enum ContainerSequenceBehavior : byte
    {
        AlwaysResetPlaylist = 0b_0000_0010,
        PlayInReverseOrder  = 0b_0000_0100,
        PlayContinuously    = 0b_0000_1000,
        GlobalScope         = 0b_0001_0000
    }
}
