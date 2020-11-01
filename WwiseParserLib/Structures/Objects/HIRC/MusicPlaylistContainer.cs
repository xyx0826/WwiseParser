using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class MusicPlaylistContainer : MusicObject
    {
        public MusicPlaylistContainer(int length) : base(HIRCObjectType.MusicPlaylistContainer, (uint)length)
        {

        }

        /// <summary>
        /// <para>The MIDI behavior of the Music Playlist Container.</para>
        /// <para>Location: Music Playlist Container Property Editor > MIDI</para>
        /// </summary>
        public MusicMidiBehavior MidiBehavior { get; set; }

        /// <summary>
        /// <para>The music grid duration of the Music Playlist Container, in milliseconds.</para>
        /// <para>Used for Playlisting between music tracks.</para>
        /// <para>Determined by: Music Playlist Container Property Editor > General Settings > Time Settings</para>
        /// </summary>
        public double GridPeriodTime { get; set; }

        /// <summary>
        /// <para>The duration offset before the music grid of the Music Playlist Container begins, in milliseconds.</para>
        /// <para>Determined by: Music Playlist Container Property Editor > General Settings > Time Settings > Grid > Offset</para>
        /// </summary>
        public double GridOffsetTime { get; set; }

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
        /// <para>Music key point should be read as <see cref="MusicKeyPointUInt"/> and casted to <see cref="MusicKeyPointByte"/>!</para>
        /// <para>ffs wwise</para>
        /// <para>Located at: Music Switch Container Property Editor > Transitions</para>
        /// </summary>
        public MusicTransition[] Transitions { get; set; }
        
        /// <summary>
        /// <para>Count of entries in the Playlist of the Music Playlist Container.</para>
        /// </summary>
        public uint PlaylistElementCount { get; set; }

        /// <summary>
        /// <para>The Playlist of the Music Playlist Container.</para>
        /// </summary>
        public MusicPlaylistElement Playlist { get; set; }

        public override string Serialize()
        {
            var sb = new StringBuilder(base.Serialize());
            sb.AppendLine();
            sb.AppendLine("====== MUSIC PLAYLIST ======");
            sb.Append(Playlist.Serialize(_children));
            sb.AppendLine("============================");
            return sb.ToString();
        }
    }

    public class MusicPlaylistElement
    {
        public uint SegmentId { get; set; }

        public uint UnknownId { get; set; }

        public uint ChildCount { get; set; }

        public MusicPlaylistElementType Type { get; set; }

        public ushort LoopCount { get; set; }

        public short RandomizerLoopCountMin { get; set; }

        public short RandomizerLoopCountMax { get; set; }

        public uint Weight { get; set; }

        public ushort AvoidRepeatCount { get; set; }

        public bool IsGroup { get; set; }

        public bool IsShuffle { get; set; }

        public MusicPlaylistElement[] Children { get; set; }

        public StringBuilder Serialize(IList<MusicObject> childSegments, StringBuilder sb = null, int depth = 0)
        {
            sb ??= new StringBuilder();
            sb.Append("".Indent(depth));
            if (Type == MusicPlaylistElementType.MusicSegment)
            {
                // Segment ID
                var segmentId = SegmentId;
                var segment = childSegments.Single(s => s is MusicSegment && s.Id == segmentId);
                sb.Append(segment.Serialize());
                //sb.Append("Segment " + SegmentId.ToHex());
            }
            else
            {
                // Play behavior
                sb.Append(Type);
                if (IsShuffle)
                {
                    sb.Append(", shuffle");
                }
            }
            // Loop behavior
            if (LoopCount == 1)
            {
                sb.AppendLine(", play once");
            }
            else if (LoopCount == 0)
            {
                sb.AppendLine(", loop infinitely");
            }
            else
            {
                sb.AppendLine(", loop " + LoopCount + " times");
            }
            foreach (var child in Children)
            {
                child.Serialize(childSegments, sb, depth + 4);
            }
            return sb;
        }

        public override string ToString()
        {
            var result = String.Empty;
            if (Type == MusicPlaylistElementType.MusicSegment)
            {
                if (Segment != null)
                {
                    result += $"Segment, {Segment.Tempo} BPM, " +
                        $"{Segment.TimeSignatureUpper}/{Segment.TimeSignatureLower}, "
                        + Segment.ChildCount + " track"
                        + (Segment.ChildCount > 1 ? "s" : "");
                }
                else
                {
                    result += "Segment (not found)";
                }
            }
            else
            {
                result += Type;
                if (IsShuffle)
                {
                    result += ", shuffle";
                }
            }
            // Loop behavior
            if (LoopCount == 1)
            {
                result += ", play once";
            }
            else if (LoopCount == 0)
            {
                result += ", loop infinitely";
            }
            else
            {
                result += ", loop " + LoopCount + " times";
            }
            return result;
        }

        #region Relationship
        public MusicSegment Segment { get; set; }
        #endregion
    }

    public enum MusicPlaylistElementType : uint
    {
        SequenceContinuous,
        SequenceStep,
        RandomContinuous,
        RandomStep,
        MusicSegment = 0xFFFFFFFF
    }
}
