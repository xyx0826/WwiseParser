using System;

namespace WwiseParserLib.Structures.Objects.HIRC.Structs
{
    public enum AudioCurveShapeByte : byte
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

    public enum AudioCurveShapeUInt : uint
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

    public enum MusicKeyPointByte : byte
    {
        Immediate,
        NextGrid,
        NextBar,
        NextBeat,
        NextCue,
        CustomCue,
        EntryCue,
        ExitCue
    }

    public enum MusicKeyPointUInt : uint
    {
        Immediate,
        NextGrid,
        NextBar,
        NextBeat,
        NextCue,
        CustomCue,
        EntryCue,
        ExitCue
    }

    [Flags]
    public enum MusicMidiBehavior : byte
    {
        OverrideMidiClipTempo = 0b_0010,
        OverrideParentMidiTarget = 0b_0100
    }

    public struct MusicCurvePoint
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
        public AudioCurveShapeUInt FollowingCurveShape { get; set; }
    }
}
