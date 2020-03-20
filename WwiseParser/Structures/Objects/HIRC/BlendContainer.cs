using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class BlendContainer : HIRCObjectBase
    {
        public BlendContainer(int length) : base(HIRCObjectType.BlendContainer, (uint)length)
        {

        }

        /// <summary>
        /// Additional properties of the Blend Container.
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// <para>The count of children of the Blend Container.</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>IDs of children of the Blend Container.</para>
        /// </summary>
        public uint[] ChildIds { get; set; }

        /// <summary>
        /// <para>The count of Blend Tracks of the Blend Container.</para>
        /// <para>Determined by: Blend Container Property Editor > Blend Tracks > Edit...</para>
        /// </summary>
        public uint BlendTrackCount { get; set; }

        /// <summary>
        /// <para>Blend Tracks of the Blend Container.</para>
        /// <para>Located by: Blend Container Property Editor > Blend Tracks > Edit...</para>
        /// </summary>
        public BlendTrack[] BlendTracks { get; set; }
    }

    struct BlendTrack
    {
        /// <summary>
        /// The ID of the Blend Track.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>The count of rules of the Blend Track.</para>
        /// <para>Determined by: Blend Track Editor > (Add)</para>
        /// </summary>
        public ushort RuleCount { get; set; }

        /// <summary>
        /// <para>Rules of the Blend Track.</para>
        /// <para>Located at: Blend Track Editor > (Add)</para>
        /// </summary>
        public BlendRule[] Rules { get; set; }

        /// <summary>
        /// <para>The ID of the Game Parameter, MIDI, LFO, or Envelope used for crossfading.</para>
        /// <para>Located at: Blend Track Editor > Crossfade > (Select)</para>
        /// </summary>
        public uint CrossfadeId { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x00.
        /// </summary>
        public byte Unknown { get; set; }

        /// <summary>
        /// <para>The count of children of the Blend Track.</para>
        /// <para>Determined by: Contents Editor > Blend Tracks</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>Children of the Blend Track.</para>
        /// <para>Located at: Contents Editor > Blend Tracks</para>
        /// </summary>
        public BlendTrackChild[] Children { get; set; }
    }

    struct BlendRule
    {
        /// <summary>
        /// <para>The ID of the x-axis parameter of the Blend Track.</para>
        /// <para>Located at: Blend Track Editor > X Axis</para>
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// <para>The type of the x-axis parameter of the Blend Track.</para>
        /// <para>Determined by: Blend Track Editor > X Axis</para>
        /// </summary>
        public BlendTrackXAxisType XType { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x01.
        /// </summary>
        public byte Unknown_05 { get; set; }

        /// <summary>
        /// <para>The type of the y-axis parameter of the Blend Track.</para>
        /// <para>Determined by: Blend Track Editor > Y Axis</para>
        /// </summary>
        public BlendTrackYAxisType YType { get; set; }

        /// <summary>
        /// An unknown ID.
        /// </summary>
        public uint UnknownId { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x00.
        /// Is 0x02 when <see cref="YType"/> is <see cref="BlendTrackYAxisType.VoiceVolume"/>.
        /// </summary>
        public byte Unknown_0B { get; set; }

        /// <summary>
        /// <para>The count of points on the RTPC editor of the Blend Track.</para>
        /// <para>Determined by: Blend Track Editor > (Editor)</para>
        /// </summary>
        public ushort PointCount { get; set; }

        /// <summary>
        /// <para>Points on the RTPC editor of the Blend Track.</para>
        /// <para>Located at: Blend Track Editor > (Editor)</para>
        /// </summary>
        public Point[] Points { get; set; }
    }

    enum BlendTrackXAxisType : byte
    {
        GameParameter,
        Midi,
        LfoOrEnvelope
    }

    enum BlendTrackYAxisType : byte
    {
        VoiceVolume,
        VoicePitch = 0x02,
        VoiceLowPassFilter,
        VoiceHighPassFilter
    }

    struct Point
    {
        /// <summary>
        /// <para>The x-axis coordinate of the point.</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// <para>The y-axis coordinate of the point.</para>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// <para>The shape of the curve following the point.</para>
        /// </summary>
        public BlendCurveShape FollowingCurveShape { get; set; }
    }

    enum BlendCurveShape : uint
    {
        LogarithmicBase3,
        SineConstantPowerFadeIn,
        LogarithmicBase1_41,
        InvertedSCurve,
        Linear,
        SCurve,
        ExponentialBase1_41,
        SineConstantPowerFadeOut,
        ExponentialBase3,
        Constant
    }

    struct BlendTrackChild
    {
        /// <summary>
        /// The ID of the child object.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>The count of crossfade points.</para>
        /// </summary>
        public uint CrossfadePointCount { get; set; }

        /// <summary>
        /// <para>Crossfade point definitions.</para>
        /// </summary>
        public CrossfadePoint[] CrossfadePoints { get; set; }
    }

    struct CrossfadePoint
    {
        /// <summary>
        /// <para>The x-axis coordinate of the point.</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// <para>The y-axis coordinate of the point.</para>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// <para>The shape of the curve following the point.</para>
        /// </summary>
        public BlendCurveShape FollowingCurveShape { get; set; }
    }
}
