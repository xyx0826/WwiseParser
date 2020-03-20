using System;
using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class AudioBus : HIRCObjectBase
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
        public AudioBusParameterType[] ParameterTypes { get; set; }

        /// <summary>
        /// <para>The values of parameters of the audio bus.</para>
        /// </summary>
        public float[] ParameterValues { get; set; }

        /// <summary>
        /// <para>The type of positioning of the audio bus.</para>
        /// </summary>
        public PositioningType Positioning { get; set; }

        /// <summary>
        /// <para>The behavior when the playback limit of the audio bus is reached.</para>
        /// </summary>
        public PlaybackLimitBehavior OnPlaybackLimitReached { get; set; }

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
        public ChannelType Channel { get; set; }

        /// <summary>
        /// <para>The HDR dynamics release mode of the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Release Mode</para>
        /// </summary>
        public HdrReleaseMode HdrReleaseMode { get; set; }

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
        public DuckedBus[] DuckedBuses { get; set; }

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
        public BypassedEffects BypassedEffects { get; set; }

        /// <summary>
        /// <para>Effects on the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > Effects > Effects > (Select)</para>
        /// </summary>
        public Effect[] Effects { get; set; }

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
        public Rtpc[] Rtpcs { get; set; }

        /// <summary>
        /// <para>The count of State Groups belonging to the audio bus.</para>
        /// <para>Determined by: Audio Bus Property Editor > States</para>
        /// </summary>
        public uint StateGroupCount { get; set; }

        /// <summary>
        /// <para>State Groups belonging to the audio bus.</para>
        /// <para>Located at: Audio Bus Property Editor > States</para>
        /// </summary>
        public StateGroup[] StateGroups { get; set; }
    }

    enum AudioBusParameterType : byte
    {
        /// <summary>
        /// <para>The voice volume.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Volume</para>
        /// </summary>
        VoiceVolume = 0x00,

        /// <summary>
        /// <para>The voice pitch.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Pitch</para>
        /// </summary>
        VoicePitch = 0x02,

        /// <summary>
        /// <para>The voice low-pass filter.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Low-pass filter</para>
        /// </summary>
        VoiceLowPass = 0x03,

        /// <summary>
        /// <para>The voice high-pass filter.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > High-pass filter</para>
        /// </summary>
        VoiceHighPass = 0x04,

        /// <summary>
        /// <para>The bus volume.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Bus > Volume</para>
        /// </summary>
        BusVolume = 0x05,

        /// <summary>
        /// <para>The x-axis coordinate of the point on the positioning panner.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > 2D > Edit... > X:</para>
        /// </summary>
        PositioningPannerX = 0x0C,

        /// <summary>
        /// <para>The y-axis coordinate of the point on the positioning panner.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > 2D > Edit... > Y:</para>
        /// </summary>
        PositioningPannerY = 0x0D,

        /// <summary>
        /// <para>The positioning center percentage.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > Center %</para>
        /// </summary>
        PositioningCenterPercentage = 0x0E,

        /// <summary>
        /// An unknown parameter type.
        /// </summary>
        Unknown1A = 0x1A,

        /// <summary>
        /// <para>The HDR dynamics threshold.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Threshold</para>
        /// </summary>
        HdrThreshold = 0x1B,

        /// <summary>
        /// <para>The HDR dynamics ratio.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Ratio</para>
        /// </summary>
        HdrRatio = 0x1C,

        /// <summary>
        /// <para>The HDR dynamics release time.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Release Time</para>
        /// </summary>
        HdrReleaseTime = 0x1D,

        /// <summary>
        /// <para>The HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > (Select)</para>
        /// </summary>
        HdrOutputGameParam = 0x1E,

        /// <summary>
        /// <para>The minimum HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > Min</para>
        /// </summary>
        HdrOutputGameParamMin = 0x1F,

        /// <summary>
        /// <para>The maximum HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > Max</para>
        /// </summary>
        HdrOutputGameParamMax = 0x20
    }

    [Flags]
    enum PositioningType : byte
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
    enum PlaybackLimitBehavior : byte
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

    enum ChannelType : uint
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

    enum HdrReleaseMode : byte
    {
        Disabled    = 0x00,
        Linear      = 0x01,
        Unknown     = 0x02,
        Exponential = 0x03
    }

    struct DuckedBus
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
        public CurveShape CurveShape { get; set; }

        /// <summary>
        /// <para>The target volume to duck of the ducked bus.</para>
        /// <para>Audio Bus Property Editor > General Settings > Auto-ducking > Busses > Target</para>
        /// </summary>
        public DuckTarget Target { get; set; }
    }

    enum CurveShape : byte
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

    enum DuckTarget : byte
    {
        VoiceVolume = 0x00,
        BusVolume = 0x05
    }

    [Flags]
    enum BypassedEffects : byte
    {
        First  = 0b_0000_0001,
        Second = 0b_0000_0010,
        Third  = 0b_0000_0100,
        Fourth = 0b_0000_1000,
        All    = 0b_0001_0000
    }

    struct Effect
    {
        /// <summary>
        /// <para>The index of the effect on its audio bus.</para>
        /// <para>Determined by: Effects > Effects > ID</para>
        /// </summary>
        public byte Index { get; set; }

        /// <summary>
        /// <para>The ID of the effect.</para>
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// <para>Whether the effect uses ShareSets.</para>
        /// <para>Located at: Effects > Effects > Mode</para>
        /// </summary>
        public bool UseShareSets { get; set; }

        /// <summary>
        /// <para>Whether the effect is pre-rendered onto the media.</para>
        /// <para>If true, other fields will be set to zero.</para>
        /// <para>Located at: Effects > Effects > Render</para>
        /// </summary>
        public bool Rendered { get; set; }
    }

    struct StateGroup
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
        public InteractiveMusicKeyPoint ChangeOccursAt;

        /// <summary>
        /// <para>The count of States with custom Settings.</para>
        /// </summary>
        public ushort StateWithCustomSettingsCount;

        /// <summary>
        /// <para>States with custom Settings.</para>
        /// </summary>
        public StateWithCustomSettings[] StateWithCustomSettings;
    }

    enum InteractiveMusicKeyPoint : byte
    {
        ImmediateOrNotApplicable,
        NextGrid,
        NextBar,
        NextBeat,
        NextCue,
        CustomCue,
        EntryCue,
        ExitCue
    }

    struct StateWithCustomSettings
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
