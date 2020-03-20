using System;
using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class Sound : HIRCObjectBase
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
        public ConversionType Conversion { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be 0x00.
        /// </summary>
        public byte Unknown_07 { get; set; }

        /// <summary>
        /// <para>The source location of the audio object.</para>
        /// <para>Located at: Sound Property Editor > General Settings > Stream</para>
        /// </summary>
        public AudioSource Source { get; set; }

        /// <summary>
        /// <para>The DATA section object ID, or streamed audio WEM file ID, of the audio object.</para>
        /// </summary>
        public uint AudioId { get; set; }

        /// <summary>
        /// <para>The length of the audio object, in bytes.</para>
        /// <para>When not <see cref="AudioSource.Embedded"/>, represents duration of some sort.</para>
        /// </summary>
        public uint AudioLength { get; set; }

        /// <summary>
        /// <para>The type of the audio object.</para>
        /// </summary>
        public AudioType AudioType { get; set; }

        /// <summary>
        /// <para>Additional properties of the audio object.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }
    }

    [Flags]
    enum ConversionType : byte
    {
        PCM    = 0b_0001,
        ADPCM  = 0b_0010,
        Vorbis = 0b_0100
    }

    enum AudioSource : uint
    {
        Embedded,
        StreamedZeroLatency,
        Streamed
    }

    enum SoundLocation : uint
    {
        Embedded,
        StreamedZeroLatency,
        Streamed
    }

    enum AudioType : byte
    {
        SoundSfx,
        SoundVoice,
        StreamedSoundSfx = 0x08
    }
}
