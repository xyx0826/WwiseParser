using System;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class AudioBus : HIRCObject
    {
        public AudioBus(int length) : base(HIRCObjectType.AudioBus, (uint)length)
        {

        }

        /// <summary>
        /// <para>The ID of the parent bus of the audio bus.</para>
        /// <para>Is zero for master busses.</para>
        /// <para>Located at: Project Explorer > Audio > Master-Mixer Hierarchy</para>
        /// </summary>
        public uint ParentId { get; set; }

        /// <summary>
        /// <para>The count of parameters of the audio bus.</para>
        /// </summary>
        public byte ParameterCount { get; set; }

        /// <summary>
        /// <para>The types of parameters of the audio bus.</para>
        /// </summary>
        public AudioParameterType[] ParameterTypes { get; set; }

        /// <summary>
        /// <para>The values of parameters of the audio bus.</para>
        /// </summary>
        public float[] ParameterValues { get; set; }

        /// <summary>
        /// <para>The type of positioning of the audio bus.</para>
        /// </summary>
        public AudioBusPositioningBehavior Positioning { get; set; }

        /// <summary>
        /// <para>The behavior when the playback limit of the audio bus is reached.</para>
        /// </summary>
        public AudioBusLimitBehavior OnPlaybackLimitReached { get; set; }

        /// <summary>
        /// <para>The count of maximum sound instances of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > Limit sound instances to</para>
        /// </summary>
        public ushort SoundInstanceLimit { get; set; }

        /// <summary>
        /// <para>The type of channel of the audio bus.</para>
        /// <para>The value should likely be separated into bytes.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Channel Configuration</para>
        /// </summary>
        public AudioBusChannelType Channel { get; set; }

        /// <summary>
        /// <para>The HDR dynamics release mode of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Release Mode</para>
        /// </summary>
        public AudioBusHdrReleaseMode HdrReleaseMode { get; set; }

        /// <summary>
        /// <para>The auto-ducking recovery time, in milliseconds, of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Recovery time</para>
        /// </summary>
        public uint AutoDuckingRecoveryTime { get; set; }

        /// <summary>
        /// <para>The auto-ducking maximum volume of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Maximum ducking volume</para>
        /// </summary>
        public float AutoDuckingMaxVolume { get; set; }

        /// <summary>
        /// <para>The count of other buses auto-ducking the audio bus.</para>
        /// <para>Determined by: Audio Bus Property Editor > General Settings > Auto-ducking > Busses</para>
        /// </summary>
        public uint DuckedBusCount { get; set; }

        /// <summary>
        /// <para>Other buses auto-ducking the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Busses</para>
        /// </summary>
        public AudioBusDuckedBus[] DuckedBuses { get; set; }

        /// <summary>
        /// <para>The count of effects on the audio bus.</para>
        /// <para>Determined by: Audio Bus Property Editor > Effects > Effects > (Select)</para>
        /// </summary>
        public byte EffectCount { get; set; }

        /// <summary>
        /// <para>The effects to bypass on the audio bus.</para>
        /// <para>Only included when <see cref="EffectCount"/> > 1.</para>
        /// <para>Located at: Audio Bus Property Editor > Effects > Effects > Bypass</para>
        /// </summary>
        public AudioBypassedEffects BypassedEffects { get; set; }

        /// <summary>
        /// <para>Effects on the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > Effects > Effects > (Select)</para>
        /// </summary>
        public AudioEffect[] Effects { get; set; }

        /// <summary>
        /// Six unknown bytes. Appears to be all zero.
        /// </summary>
        public byte[] UnknownBytes { get; set; }

        /// <summary>
        /// <para>The count of RTPCs of the audio bus.</para>
        /// <para>Determined by: </para>
        /// </summary>
        public ushort RtpcCount { get; set; }

        /// <summary>
        /// <para>RTPCs of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Select)</para>
        /// </summary>
        public AudioRtpc[] Rtpcs { get; set; }

        /// <summary>
        /// <para>The count of State Groups belonging to the audio bus.</para>
        /// <para>Determined by: Audio Bus Property Editor > States</para>
        /// </summary>
        public uint StateGroupCount { get; set; }

        /// <summary>
        /// <para>State Groups belonging to the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > States</para>
        /// </summary>
        public AudioStateGroup[] StateGroups { get; set; }
    }

    [Flags]
    public enum AudioBusPositioningBehavior : byte
    {
        /// <summary>
        /// <para>Positioning is disabled.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning</para>
        /// </summary>
        Disabled = 0b_0000_0000,

        /// <summary>
        /// <para>Positioning is enabled.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > Enable Positioning</para>
        /// </summary>
        Enabled       = 0b_0000_0001,

        /// <summary>
        /// <para>Panner is enabled.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > 2D > Enable Panner</para>
        /// </summary>
        PannerEnabled = 0b_0000_0010
    }

    [Flags]
    public enum AudioBusLimitBehavior : byte
    {
        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > When limit is reached:</para>
        /// </summary>
        KillVoice = 0b_0000,

        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > When priority is equal:</para>
        /// </summary>
        DiscardOldest = 0b_0000,

        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > When priority is equal:</para>
        /// </summary>
        DiscardNewest = 0b_0001,

        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > When limit is reached:</para>
        /// </summary>
        UseVirtual = 0b_0010,

        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > Advanced Settings > Playback Limit > Override parent</para>
        /// </summary>
        OverrideParent = 0b_0100,

        /// <summary>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Mute for Background Music</para>
        /// </summary>
        MuteForBackgroundMusic = 0b_1000
    }

    public enum AudioBusChannelType : uint
    {
        Parent = 0x00000000,
        Ambisonics1_1 = 0x00000204,
        Ambisonics2_2 = 0x00000209,
        Ambisonics3_3 = 0x00000210,
        Type2_0 = 0x00003102,
        Type1_0 = 0x00004101,
        Type3_0 = 0x00007103,
        Type4_0 = 0x00603104,
        Type5_1 = 0x0060F106,
        Type7_1 = 0x0063F108,
        Type5_1_2 = 0x0560F108,
        Type7_1_2 = 0x0563F10A,
        Type7_1_4 = 0x2D63F10C, // Note: value is larger than Auro9_1
        Auro9_1 = 0x2D60F10A,
        Auro10_1 = 0x2DE0F10B,
        Auro11_1 = 0x2FE0F10C,
        AUro13_1 = 0x2FE3F10E
    }

    public enum AudioBusHdrReleaseMode : byte
    {
        Disabled    = 0x00,
        Linear      = 0x01,
        Unknown     = 0x02,
        Exponential = 0x03
    }

    public struct AudioBusDuckedBus
    {
        /// <summary>
        /// <para>The ID of the ducked bus.</para>
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>The volume of the ducked bus.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Volume</para>
        /// </summary>
        public float Volume { get; set; }

        /// <summary>
        /// <para>The fade-out time of the ducked bus, in milliseconds.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Fade Out</para>
        /// </summary>
        public uint FadeOut { get; set; }

        /// <summary>
        /// <para>The fade-in time of the ducked bus, in milliseconds.</para>
        /// <para>Location: Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Fade In</para>
        /// </summary>
        public uint FadeIn { get; set; }

        /// <summary>
        /// <para>The shape of the ducking curve of the ducked bus.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Curve</para>
        /// </summary>
        public AudioCurveShapeByte CurveShape { get; set; }

        /// <summary>
        /// <para>The target volume to duck of the ducked bus.</para>
        /// <para>Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Target</para>
        /// </summary>
        public AudioBusDuckTarget Target { get; set; }
    }

    public enum AudioBusDuckTarget : byte
    {
        VoiceVolume = 0x00,
        BusVolume = 0x05
    }

    public struct AudioStateGroup
    {
        /// <summary>
        /// The ID of the State Group.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>The key point where interactive music changes should occur at.</para>
        /// <para>Is zero if the State Group does not play Interactive Music.</para>
        /// <para>Located at: Audio Bus Property Editor > States > Change occurs at</para>
        /// </summary>
        public MusicKeyPointByte MusicChangeAt { get; set; }

        /// <summary>
        /// <para>The count of States with custom Settings.</para>
        /// </summary>
        public ushort StateWithSettingsCount { get; set; }

        /// <summary>
        /// <para>States with custom Settings.</para>
        /// </summary>
        public AudioStateWithSettings[] StatesWithSettings { get; set; }
    }

    public struct AudioStateWithSettings
    {
        /// <summary>
        /// <para>The ID of the State.</para>
        /// </summary>
        public uint StateId;

        /// <summary>
        /// <para>The ID of the custom Settings object.</para>
        /// </summary>
        public uint SettingsId;
    }
}
