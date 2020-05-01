using System;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class MusicTrack : Actor
    {
        public MusicTrack(int length) : base(HIRCObjectType.MusicTrack, (uint)length)
        {

        }

        /// <summary>
        /// An unknown value. Seems to always be zero.
        /// </summary>
        public byte Unknown { get; set; }

        /// <summary>
        /// The count of sound objects included in the Music Track.
        /// </summary>
        public uint SoundCount { get; set; }

        /// <summary>
        /// <para>Sound objects included in the Music Track.</para>
        /// </summary>
        public Sound[] Sounds { get; set; }

        /// <summary>
        /// <para>The count of time parameter sets for Sound objects of the Music Track.</para>
        /// </summary>
        public uint TimeParameterCount { get; set; }

        /// <summary>
        /// <para>Time parameter sets for Sound objects of the Music Track.</para>
        /// </summary>
        public MusicTrackTimeParameter[] TimeParameters { get; set; }

        /// <summary>
        /// <para>The count of Sub-Tracks of the Music Track.</para>
        /// </summary>
        public uint SubTrackCount { get; set; }

        /// <summary>
        /// <para>The count of parameter curves on Sounds of the Music Track.</para>
        /// </summary>
        public uint CurveCount { get; set; }

        /// <summary>
        /// <para>Parameter curves of the Music Track.</para>
        /// </summary>
        public MusicTrackCurve[] Curves { get; set; }

        /// <summary>
        /// <para>The type of the Music Track.</para>
        /// <para>Located at: Music Track Property Editor > General Settings > Track Type</para>
        /// </summary>
        public MusicTrackType TrackType { get; set; }

        /// <summary>
        /// <para>The switch parameters of the Music Track.</para>
        /// <para>Only exists when <see cref="TrackType"/> is <see cref="MusicTrackType.Switch"/>.</para>
        /// </summary>
        public MusicSwitchParameters SwitchParameters { get; set; }

        /// <summary>
        /// <para>The look-ahead time of the Music Track, in milliseconds.</para>
        /// <para>Located at: Music Track Property Editor > General Settings > Stream > Look-ahead time (ms)</para>
        /// </summary>
        public uint LookAheadTime { get; set; }

        public new uint ChildCount
        {
            get
            {
                throw new InvalidOperationException("This type of Actor does not have any children.");
            }
        }

        public new uint[] ChildIds
        {
            get
            {
                throw new InvalidOperationException("This type of Actor does not have any children.");
            }
        }
    }

    public struct MusicTrackTimeParameter
    {
        /// <summary>
        /// <para>The index of Sub-Track that the time parameter belongs to.</para>
        /// </summary>
        public uint SubTrackIndex { get; set; }

        /// <summary>
        /// <para>The DATA section object ID, or streamed audio WEM file ID, of the Music Track.</para>
        /// </summary>
        public uint AudioId { get; set; }

        /// <summary>
        /// <para>The start offset of this Music Track, relative to the timeline of its parent.</para>
        /// </summary>
        public double BeginOffset { get; set; }

        /// <summary>
        /// <para>The length trimmed from the beginning of this Music Track.</para>
        /// </summary>
        public double BeginTrimOffset { get; set; }

        /// <summary>
        /// <para>The end offset of this Music Track, relative to the timeline of its parent.</para>
        /// </summary>
        public double EndOffset { get; set; }

        /// <summary>
        /// <para>The length trimmed from the end of this Music Track.</para>
        /// </summary>
        public double EndTrimOffset { get; set; }
    }

    public struct MusicTrackCurve
    {
        /// <summary>
        /// <para>The index of the segment defined by the time parameter that the curve applies to.</para>
        /// </summary>
        public uint TimeParameterIndex { get; set; }

        /// <summary>
        /// <para>The parameter type of the curve.</para>
        /// </summary>
        public MusicFadeCurveType Type { get; set; }

        /// <summary>
        /// <para>The count of points on the curve.</para>
        /// </summary>
        public uint PointCount { get; set; }

        /// <summary>
        /// <para>Points on the curve.</para>
        /// </summary>
        public MusicCurvePoint[] Points { get; set; }
    }

    public enum MusicFadeCurveType : uint
    {
        VoiceVolume,
        VoiceLowPassFilter,
        VoiceHighPassFilter,
        FadeIn,
        FadeOut
    }

    public enum MusicTrackType : byte
    {
        Normal,
        RandomStep,
        SequenceStep,
        Switch
    }

    public struct MusicSwitchParameters
    {
        /// <summary>
        /// Unknown value. Appears to always be zero.
        /// </summary>
        public byte Unknown { get; set; }

        /// <summary>
        /// <para>The ID of the Switch or State group of the Music Track.</para>
        /// <para>Located at: Music Track Property Editor > General Settings > Switch > Group</para>
        /// </summary>
        public uint GroupId { get; set; }

        /// <summary>
        /// <para>The ID of the default Switch or State of the Music Track.</para>
        /// <para>Located at: Music Track Property Editor > General Settings > Switch > Default Switch/State</para>
        /// </summary>
        public uint DefaultSwitchOrStateId { get; set; }

        /// <summary>
        /// <para>The count of Sub-Tracks of the Music Track.</para>
        /// </summary>
        public uint SubTrackCount { get; set; }

        /// <summary>
        /// <para>IDs of Switch or State objects associated with Sub-Tracks in order.</para>
        /// <para>Located at: Music Segment Editor > Association</para>
        /// </summary>
        public uint[] AssociatedSwitchOrStateIds { get; set; }

        /// <summary>
        /// <para>The duration of source fade-out, in milliseconds.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Source > Edit... > Time</para>
        /// </summary>
        public uint FadeOutDuration { get; set; }

        /// <summary>
        /// <para>The shape of source fade-out curve.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt FadeOutCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of source fade-out relative to the exit cue, in milliseconds.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Edit... > (Editor)</para>
        /// </summary>
        public int FadeOutOffset { get; set; }

        /// <summary>
        /// <para>The key point at which the source exits.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Source > Exit source at</para>
        /// </summary>
        public MusicKeyPointByte ExitSourceAt { get; set; }

        /// <summary>
        /// <para>The ID of the Cue at which the source exits.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Source > Match:</para>
        /// </summary>
        public uint ExitSourceAtCueId { get; set; }

        /// <summary>
        /// <para>The duration of destination fade-in, in milliseconds.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Destination > Edit... > Time</para>
        /// </summary>
        public uint FadeInDuration { get; set; }

        /// <summary>
        /// <para>The shape of destination fade-in curve.</para>
        /// <para?>Located at: Music Track Container Property Editor > Transitions > Destination > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt FadeInCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of destination fade-in relative to the sync target, in milliseconds.</para>
        /// <para>Located at: Music Track Container Property Editor > Transitions > Destination > Edit... > (Editor)</para>
        /// </summary>
        public int FadeInOffset { get; set; }
    }
}
