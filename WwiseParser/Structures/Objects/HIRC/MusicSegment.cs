using System;
using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class MusicSegment : HIRCObjectBase
    {
        public MusicSegment(int length) : base(HIRCObjectType.MusicSegment, (uint)length)
        {

        }

        /// <summary>
        /// <para>The MIDI behavior of the Music Segment.</para>
        /// <para>Location: Music Segment Property Editor > MIDI</para>
        /// </summary>
        public MidiInteractiveMusicBehavior MidiBehavior { get; set; }

        /// <summary>
        /// <para>Additional properties of the Music Segment.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// <para>The count of children Music Tracks of the Music Segment.</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>IDs of children Music Tracks of the Music Segment.</para>
        /// </summary>
        public uint[] ChildIds { get; set; }

        /// <summary>
        /// <para>The music grid duration of the Music Segment, in milliseconds.</para>
        /// <para>Used for switching between music tracks.</para>
        /// <para>Determined by: Music Segment Property Editor > General Settings > Time Settings</para>
        /// </summary>
        public double GridPeriodTime { get; set; }

        /// <summary>
        /// <para>The duration offset before the music grid of the Music Segment begins, in milliseconds.</para>
        /// <para>Determined by: Music Segment Property Editor > General Settings > Time Settings > Grid > Offset</para>
        /// </summary>
        public double GridOffsetTime { get; set; }

        /// <summary>
        /// <para>The tempo (BPM) of the Music Segment.</para>
        /// <para>Located at: Music Segment Property Editor > General Settings > Time Settings > Tempo</para>
        /// </summary>
        public float Tempo { get; set; }

        /// <summary>
        /// <para>The upper part of the time signature of the Music Segment.</para>
        /// <para>Located at: Music Segment Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureUpper { get; set; }

        /// <summary>
        /// <para>The lower part of the time signature of the Music Segment.</para>
        /// <para>Located at: Music Segment Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureLower { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0xFF.
        /// </summary>
        public byte Unknown { get; set; }

        /// <summary>
        /// <para>The count of stingers in the Music Segment.</para>
        /// <para>Determined by: Music Segment Property Editor > Stingers > (Add)</para>
        /// </summary>
        public uint StingerCount { get; set; }

        /// <summary>
        /// <para>Stingers in the Music Segment.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > (Add)</para>
        /// </summary>
        public Stinger[] Stingers { get; set; }

        /// <summary>
        /// <para>The end offset of this Music Segment.</para>
        /// <para>Determined by Time Settings and Music Editor.</para>
        /// </summary>
        public double EndTrimOffset { get; set; }

        /// <summary>
        /// <para>The count of Cues of the Music Segment.</para>
        /// <para>Determined by: Music Editor > (Editor)</para>
        /// </summary>
        public uint MusicCueCount { get; set; }

        /// <summary>
        /// <para>Cues of the Music Segment.</para>
        /// <para>Determined by: Music Editor > (Editor)</para>
        /// </summary>
        public MusicCue[] MusicCues { get; set; }
    }
    
    [Flags]
    enum MidiInteractiveMusicBehavior : byte
    {
        OverrideMidiClipTempo    = 0b_0010,
        OverrideParentMidiTarget = 0b_0100
    }

    struct Stinger
    {
        /// <summary>
        /// The ID of the Trigger that causes the Stinger to start playing.
        /// </summary>
        public uint TriggerId { get; set; }

        /// <summary>
        /// The ID of the Segment that the Stinger plays.
        /// </summary>
        public uint SegmentId { get; set; }

        /// <summary>
        /// <para>The key point that the Stinger plays at.</para>
        /// <para>Read this value as a <see cref="uint"/> and convert it to a <see cref="StingerKeyPoint"/>.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > Play At</para>
        public StingerKeyPoint PlayAt { get; set; }

        /// <summary>
        /// <para>The ID of the Cue that the Stinger plays at.</para>
        /// <para>Zero if not applicable.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > Cue Name (Match)</para>
        /// </summary>
        public uint CueId { get; set; }

        /// <summary>
        /// <para>The duration in which the Stinger should not be played again, in milliseconds.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > Stinger Options > Don't play this ...</para>
        /// </summary>
        public uint DoNotRepeatIn { get; set; }

        /// <summary>
        /// <para>Whether to allow the Stinger to keep playing in the next segment.</para>
        /// <para>Read this value as a <see cref="uint"/> and convert it to a <see cref="bool"/>.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > Stinger Options > Allow playing ...</para>
        /// </summary>
        public bool AllowPlayingInNextSegment { get; set; }
    }

    enum StingerKeyPoint : uint
    {
        ImmediateOrNotApplicable,
        NextGrid,
        NextBar,
        NextBeat,
        NextCue,
        NextCustomCue,
        EntryCue,
        ExitCue
    }

    struct MusicCue
    {
        /// <summary>
        /// The ID of the Cue.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>The timestamp of the Cue, in milliseconds.</para>
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// <para>The length of the name of a Custom Cue.</para>
        /// </summary>
        public uint CustomNameLength { get; set; }

        /// <summary>
        /// <para>The null-terminated name of a Custom Cue.</para>
        /// <para>Only exists when <see cref="CustomNameLength"/> > 0.</para>
        /// </summary>
        public string CustomName { get; set; }
    }

    #region Ignore
    //enum GridFrequencyPreset : byte
    //{
    //    FourBars               = 0x32,
    //    TwoBars                = 0x33,
    //    OneBar                 = 0x34,
    //    WholeNote              = 0x35,
    //    HalfNote               = 0x36,
    //    QuarterNote            = 0x37,
    //    EighthNote             = 0x38,
    //    SixteenthNote          = 0x39,
    //    ThirtySecondNote       = 0x43,
    //    OneHalfTriplet         = 0x3A,
    //    OneFourthTriplet       = 0x3B,
    //    OneEighthTriplet       = 0x3C,
    //    OneSixteenthTriplet    = 0x41,
    //    OneThirtySecondTriplet = 0x44,
    //    OneHalfDotted          = 0x3D,
    //    OneFourthDotted        = 0x3E,
    //    OneEighthDotted        = 0x3F,
    //    OneSixteenthDotted     = 0x42,
    //    OneThirtySecondDotted  = 0x45
    //}

    //enum GridFrequencyPresetSorted : byte
    //{
    //    FourBars               = 0x32,
    //    TwoBars                = 0x33,
    //    OneBar                 = 0x34,
    //    WholeNote              = 0x35,
    //    HalfNote               = 0x36,
    //    QuarterNote            = 0x37,
    //    EighthNote             = 0x38,
    //    SixteenthNote          = 0x39,
    //    OneHalfTriplet         = 0x3A,
    //    OneFourthTriplet       = 0x3B,
    //    OneEighthTriplet       = 0x3C,
    //    OneHalfDotted          = 0x3D,
    //    OneFourthDotted        = 0x3E,
    //    OneEighthDotted        = 0x3F,
    //    // 0x40 is skipped
    //    OneSixteenthTriplet    = 0x41,
    //    OneSixteenthDotted     = 0x42,
    //    ThirtySecondNote       = 0x43,
    //    OneThirtySecondTriplet = 0x44,
    //    OneThirtySecondDotted  = 0x45
    //}
    #endregion
}
