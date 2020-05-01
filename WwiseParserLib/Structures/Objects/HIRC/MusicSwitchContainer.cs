using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class MusicSwitchContainer : Actor
    {
        public MusicSwitchContainer(int length) : base(HIRCObjectType.MusicSwitchContainer, (uint)length)
        {

        }

        /// <summary>
        /// <para>The MIDI behavior of the Music Switch Container.</para>
        /// <para>Location: Music Switch Container Property Editor > MIDI</para>
        /// </summary>
        public MusicMidiBehavior MidiBehavior { get; set; }

        ///// <summary>
        ///// <para>The count of children Music Tracks of the Music Switch Container.</para>
        ///// </summary>
        //public uint ChildCount { get; set; }

        ///// <summary>
        ///// <para>IDs of children Music Tracks of the Music Switch Container.</para>
        ///// </summary>
        //public uint[] ChildIds { get; set; }

        /// <summary>
        /// <para>The music grid duration of the Music Switch Container, in milliseconds.</para>
        /// <para>Used for switching between music tracks.</para>
        /// <para>Determined by: Music Switch Container Property Editor > General Settings > Time Settings</para>
        /// </summary>
        public double GridPeriodTime { get; set; }

        /// <summary>
        /// <para>The duration offset before the music grid of the Music Switch Container begins, in milliseconds.</para>
        /// <para>Determined by: Music Switch Container Property Editor > General Settings > Time Settings > Grid > Offset</para>
        /// </summary>
        public double GridOffsetTime { get; set; }

        /// <summary>
        /// <para>The tempo (BPM) of the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Property Editor > General Settings > Time Settings > Tempo</para>
        /// </summary>
        public float Tempo { get; set; }

        /// <summary>
        /// <para>The upper part of the time signature of the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureUpper { get; set; }

        /// <summary>
        /// <para>The lower part of the time signature of the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureLower { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x01.
        /// </summary>
        public byte Unknown_1 { get; set; }

        /// <summary>
        /// <para>The count of Stingers in the Music Switch Container.</para>
        /// <para>Determined by: Music Switch Container Property Editor > Stingers > (Add)</para>
        /// </summary>
        public uint StingerCount { get; set; }

        /// <summary>
        /// <para>Stingers in the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Property Editor > Stingers > (Add)</para>
        /// </summary>
        public MusicStinger[] Stingers { get; set; }

        /// <summary>
        /// <para>The count of Transitions in the Music Switch Container.</para>
        /// <para>Determined by: Music Switch Container Property Editor > Transitions</para>
        /// </summary>
        public uint TransitionCount { get; set; }

        /// <summary>
        /// <para>Transitions in the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions</para>
        /// </summary>
        public MusicTransition[] Transitions { get; set; }

        /// <summary>
        /// <para>Whether to continue playing on Switch changes.</para>
        /// <para>Located at: Music Switch Container Property Editor > Play Options > Continue to play on Switch change</para>
        /// </summary>
        public bool ContinueOnSwitchChange { get; set; }

        /// <summary>
        /// <para>The count of Switch Groups or State Groups used by the Music Switch Container to define paths.</para>
        /// <para>Determined by: Music Switch Container Association Editor</para>
        /// </summary>
        public uint GroupCount { get; set; }

        /// <summary>
        /// <para>IDs of Switch Groups or State Groups used by the Music Switch Container to define paths.</para>
        /// <para>Determined by: Music Switch Container Association Editor</para>
        /// </summary>
        public uint[] GroupIds { get; set; }

        /// <summary>
        /// <para>Whether the group is a State Group. Otherwise it is a Switch Group.</para>
        /// </summary>
        public bool[] GroupTypes { get; set; }

        /// <summary>
        /// <para>The length of the Path data section at the end of the object, in bytes.</para>
        /// </summary>
        public uint PathSectionLength { get; set; }

        /// <summary>
        /// <para>Whether to use weighted association mode. Otherwise use best match.</para>
        /// <para>Located at: Music Switch Container Association Editor > Mode:</para>
        /// </summary>
        public bool UseWeighted { get; set; }

        /// <summary>
        /// <para>The association paths of the Music Switch Container.</para>
        /// <para>Located at: Music Switch Container Association Editor</para>
        /// </summary>
        public AudioPathNode Paths { get; set; }
    }

    public struct MusicTransition
    {
        /// <summary>
        /// Unknown value. Appears to always be 1.
        /// </summary>
        public uint Unknown_1 { get; set; }

        /// <summary>
        /// <para>The ID of the transition source object.</para>
        /// <para>Location: Music Switch Container Property Editor > Transitions > Source</para>
        /// </summary>
        public uint SourceId { get; set; }

        /// <summary>
        /// Unknown value. Appears to always be 1.
        /// </summary>
        public uint Unknown_2 { get; set; }

        /// <summary>
        /// <para>The ID of the transition destination object.</para>
        /// <para>Location: Music Switch Container Property Editor > Transitions > Destination</para>
        /// </summary>
        public uint DestinationId { get; set; }

        /// <summary>
        /// <para>The duration of source fade-out, in milliseconds.</para>
        /// <para>Location: Music Switch Container Property Editor > Transitions > Source > Edit... > Time</para>
        /// </summary>
        public uint FadeOutDuration { get; set; }

        /// <summary>
        /// <para>The shape of source fade-out curve.</para>
        /// <para>Location: Music Switch Container Property Editor > Transitions > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt FadeOutCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of source fade-out relative to the exit cue, in milliseconds.</para>
        /// <para>Location: Music Switch Container Property Editor > Transitions > Edit... > (Editor)</para>
        /// </summary>
        public int FadeOutOffset { get; set; }

        /// <summary>
        /// <para>The key point at which the source exits.</para>
        /// <para><see cref="MusicPlaylistContainer"/> uses <see cref="MusicKeyPointUInt"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Source > Exit source at</para>
        /// </summary>
        public MusicKeyPointByte ExitSourceAt { get; set; }

        /// <summary>
        /// <para>The ID of the Cue at which the source exits.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Source > Match:</para>
        /// </summary>
        public uint ExitSourceAtCueId { get; set; }

        /// <summary>
        /// <para>Whether to play the post-exit section of the source.</para>
        /// <para>A value of 0xFF converts to true.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Source > Play post-exit</para>
        /// </summary>
        public bool PlayPostExit { get; set; }

        /// <summary>
        /// <para>The duration of destination fade-in, in milliseconds.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Edit... > Time</para>
        /// </summary>
        public uint FadeInDuration { get; set; }

        /// <summary>
        /// <para>The shape of destination fade-in curve.</para>
        /// <para?>Located at: Music Switch Container Property Editor > Transitions > Destination > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt FadeInCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of destination fade-in relative to the sync target, in milliseconds.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Edit... > (Editor)</para>
        /// </summary>
        public int FadeInOffset { get; set; }

        /// <summary>
        /// <para>The ID of the destination custom Cue used as a filter.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Custom Cue Filter > Match:</para>
        /// </summary>
        public uint CustomCueFilterId { get; set; }

        /// <summary>
        /// <para>The ID of the destination playlist item to jump to.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Jump to playlist item</para>
        /// </summary>
        public uint JumpToPlaylistItemId { get; set; }

        /// <summary>
        /// <para>The target at which the destination enters.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Sync to</para>
        /// </summary>
        public MusicTransitionSyncTarget DestinationSyncTo { get; set; }

        /// <summary>
        /// <para>Whether to play the pre-entry section of the destination.</para>
        /// <para>A value of 0xFF converts to true.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Play pre-entry</para>
        /// </summary>
        public bool PlayPreEntry { get; set; }

        /// <summary>
        /// <para>Whether to use the source Cue as the Cue filter.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Destination > Custom Cue Filter > Match source cue name</para>
        /// </summary>
        public bool MatchSourceCueName { get; set; }

        /// <summary>
        /// <para>Whether to use a transition Music Segment.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Use transition segment</para>
        /// </summary>
        public bool UseTransitionSegment { get; set; }

        /// <summary>
        /// <para>The ID of the transition Music Segment to be used.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > (Select)</para>
        /// </summary>
        public uint TransitionSegmentId { get; set; }

        /// <summary>
        /// <para>The duration of transition segment fade-in, in milliseconds.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-in > Edit... > Time</para>
        /// </summary>
        public uint TransitionFadeInDuration { get; set; }

        /// <summary>
        /// <para>The shape of transition segment fade-in curve.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-in > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt TransitionFadeInCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of transition segment fade-in relative to the entry Cue, in milliseconds.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-in > Edit... > (Editor)</para>
        /// </summary>
        public int TransitionFadeInOffset { get; set; }

        /// <summary>
        /// <para>The duration of transition segment fade-out, in milliseconds.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-out > Edit... > Time</para>
        /// </summary>
        public uint TransitionFadeOutDuration { get; set; }

        /// <summary>
        /// <para>The shape of transition segment fade-out curve.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-out > Edit... > Curve</para>
        /// </summary>
        public AudioCurveShapeUInt TransitionFadeOutCurveShape { get; set; }

        /// <summary>
        /// <para>The offset of transition segment fade-out relative to the exit Cue, in milliseconds.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Fade-out > Edit... > (Editor)</para>
        /// </summary>
        public int TransitionFadeOutOffset { get; set; }

        /// <summary>
        /// <para>Whether to play the pre-entry section of the transition segment.</para>
        /// <para>A value of 0xFF converts to true.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Play transition pre-entry</para>
        /// </summary>
        public bool PlayTransitionPreEntry { get; set; }

        /// <summary>
        /// <para>Whether to play the post-exit section of the transition segment.</para>
        /// <para>A value of 0xFF converts to true.</para>
        /// <para>Only exists when <see cref="UseTransitionSegment"/>.</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions > Transition Segment > Play transition post-exit</para>
        /// </summary>
        public bool PlayTransitionPostExit { get; set; }
    }

    public enum MusicTransitionSyncTarget : ushort
    {
        EntryCue,
        SameTimeAsPlayingSegment,
        RandomCue,
        RandomCustomCue
    }

    public class AudioPathElement
    {
        /// <summary>
        /// <para>The ID of the parent State or Switch.</para>
        /// <para>Zero if any.</para>
        /// </summary>
        public uint FromStateOrSwitchId { get; set; }

        /// <summary>
        /// <para>The weight of the path element.</para>
        /// </summary>
        public ushort Weight { get; set; }

        /// <summary>
        /// <para>The probability of the path element.</para>
        /// </summary>
        public ushort Probability { get; set; }
    }

    public class AudioPathNode : AudioPathElement
    {
        /// <summary>
        /// <para>The beginning position of the node's children in all nodes.</para>
        /// </summary>
        public ushort ChildrenStartAtIndex { get; set; }

        /// <summary>
        /// <para>The count of children of the path parent.</para>
        /// <para>Children can be path parents or path endpoints.</para>
        /// </summary>
        public ushort ChildCount { get; set; }

        /// <summary>
        /// <para>Children of the path node.</para>
        /// </summary>
        public AudioPathElement[] Children { get; set; }
    }

    public class MusicPathEndpoint : AudioPathElement
    {
        /// <summary>
        /// <para>The ID of the audio object to play.</para>
        /// <para>For Music Switch Containers, the object must be a direct children.</para>
        /// </summary>
        public uint AudioId { get; set; }
    }
}
