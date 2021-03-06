﻿using System;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class Sound : SoundObject
    {
        public Sound(int length) : base(HIRCObjectType.Sound, (uint)length)
        {

        }

        /// <summary>
        /// Unknown byte. Appears to always be 0x01.
        /// </summary>
        public byte Unknown_04 { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x00.
        /// </summary>
        public byte Unknown_05 { get; set; }

        /// <summary>
        /// <para>The conversion type of the sound object.</para>
        /// <para>Located at: Sound Property Editor > Source Settings > Conversion Settings</para>
        /// </summary>
        public SoundConversionType Conversion { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x00.
        /// </summary>
        public byte Unknown_07 { get; set; }

        /// <summary>
        /// <para>The source location of the audio object.</para>
        /// <para>Located at: Sound Property Editor > General Settings > Stream</para>
        /// </summary>
        public SoundSource Source { get; set; }

        /// <summary>
        /// <para>The DATA section object ID, or streamed audio WEM file ID, of the audio object.</para>
        /// </summary>
        public uint AudioId { get; set; }

        /// <summary>
        /// <para>The length of the audio object, in bytes.</para>
        /// <para>When not <see cref="SoundSource.Embedded"/>, represents duration of some sort.</para>
        /// </summary>
        public uint AudioLength { get; set; }

        /// <summary>
        /// <para>The type of the audio object.</para>
        /// </summary>
        public SoundType AudioType { get; set; }

        public new uint ChildCount
        {
            get
            {
                return 0;
                throw new InvalidOperationException("This type of Actor does not have any children.");
            }
        }

        public new uint[] ChildIds
        {
            get
            {
                return null;
                throw new InvalidOperationException("This type of Actor does not have any children.");
            }
        }

        public override string Serialize()
        {
            return AudioId.ToHex() + ".wem";
        }
    }

    [Flags]
    public enum SoundConversionType : byte
    {
        PCM    = 0b_0001,
        ADPCM  = 0b_0010,
        Vorbis = 0b_0100
    }

    public enum SoundSource : uint
    {
        Embedded,
        StreamedZeroLatency,
        Streamed
    }

    public enum SoundLocation : uint
    {
        Embedded,
        StreamedZeroLatency,
        Streamed
    }

    public enum SoundType : byte
    {
        SoundSfx,
        SoundVoice,
        StreamedSoundSfx = 0x08
    }
}
