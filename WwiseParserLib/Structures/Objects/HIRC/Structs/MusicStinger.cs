namespace WwiseParserLib.Structures.Objects.HIRC.Structs
{
    public struct MusicStinger
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
        /// <para>Read this value as a <see cref="uint"/> and convert it to a <see cref="MusicKeyPointUInt"/>.</para>
        /// <para>Located at: Music Segment Property Editor > Stingers > Play At</para>
        /// </summary>
        public MusicKeyPointUInt PlayAt { get; set; }

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
}
