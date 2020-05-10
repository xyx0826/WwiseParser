using System;

namespace WwiseParserLib.Structures.Objects.HIRC.Structs
{
    /// <summary>
    /// The shared structure used by many Wwise objects in the Actor-Mixer or Interactive Music hierarchies.
    /// </summary>
    public class AudioProperties
    {
        /// <summary>
        /// <para>Whether to override parent effects for the audio object.</para>
        /// <para>Located at: Sound Property Editor > Effects > Override parent</para>
        /// </summary>
        public bool OverrideEffects { get; set; }

        /// <summary>
        /// <para>The count of effects on the audio object.</para>
        /// <para>Determined by: Sound Property Editor > Effects > Effects</para>
        /// </summary>
        public byte EffectCount { get; set; }

        /// <summary>
        /// <para>Bypassed effects on the audio object.</para>
        /// <para>Only exists when <see cref="EffectCount"/> > 0.</para>
        /// <para>Located at: Sound Property Editor > Effects > Effects > Bypass</para>
        /// </summary>
        public AudioBypassedEffects BypassedEffects { get; set; }

        /// <summary>
        /// <para>Effects on the audio object.</para>
        /// <para>Located at: Sound Property Editor > Effects > Effects</para>
        /// </summary>
        public AudioEffect[] Effects { get; set; }

        /// <summary>
        /// Unknown byte. Seems to be always zero.
        /// </summary>
        public byte Unknown_1 { get; set; }

        /// <summary>
        /// <para>The ID of the output bus of the sound object.</para>
        /// <para>Is zero if using parent object audio bus.</para>
        /// </summary>
        public uint OutputBusId { get; set; }

        /// <summary>
        /// <para>The ID of the parent object of the sound object.</para>
        /// </summary>
        public uint ParentId { get; set; }

        /// <summary>
        /// <para>The playback and MIDI override behaviors of the sound object.</para>
        /// <para>Located at: MIDI, Advanced Settings</para>
        /// </summary>
        public AudioPlaybackBehavior PlaybackBehavior { get; set; }

        /// <summary>
        /// <para>The count of parameters of the sound object.</para>
        /// </summary>
        public byte ParameterCount { get; set; }

        /// <summary>
        /// <para>The types of parameters of the sound object.</para>
        /// </summary>
        public AudioParameterType[] ParameterTypes { get; set; }

        /// <summary>
        /// <para>The parameters of the sound object.</para>
        /// <para>Most parameters are interpreted as <see cref="float"/>, yet some are <see cref="int"/> or <see cref="uint"/>.</para>
        /// </summary>
        public ValueType[] ParameterValues { get; set; }

        /// <summary>
        /// The count of unknown parameter pairs.
        /// </summary>
        public byte ParameterPairCount { get; set; }

        /// <summary>
        /// Types of unknown parameter pairs.
        /// </summary>
        public byte[] ParameterPairTypes { get; set; }

        /// <summary>
        /// Values of unknown parameter pairs.
        /// </summary>
        public AudioParameterPair[] ParameterPairValues { get; set; }

        /// <summary>
        /// <para>The spatial positioning behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > Positioning</para>
        /// </summary>
        public AudioPositioningBehavior Positioning { get; set; }

        /// <summary>
        /// <para>Whether to use game-defined 3D position source for the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Position Source > Game-defined</para>
        /// </summary>
        public bool IsGameDefined { get; set; }

        /// <summary>
        /// <para>The ID of the attenuation object for the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Attenuation > (Select)</para>
        /// </summary>
        public uint AttenuationId { get; set; }

        /// <summary>
        /// <para>The settings of user-defined position source editor for the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Position Source > Edit...</para>
        /// </summary>
        public AudioUserDefinedPositioningBehavior UserDefinedPlaySettings { get; set; }

        /// <summary>
        /// <para>The transition time of this audio object, in milliseconds.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Play Mode > Transition time</para>
        /// </summary>
        public uint TransitionTime { get; set; }

        /// <summary>
        /// <para>The count of control point keys on the timeline of the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > (Editor)</para>
        /// </summary>
        public uint ControlPointKeyCount { get; set; }

        /// <summary>
        /// <para>Control point keys on the timeline of the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > (Editor)</para>
        /// </summary>
        public AudioControlPointKey[] ControlPointKeys { get; set; }

        /// <summary>
        /// <para>The count of random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > Random Range</para>
        /// </summary>
        public uint RandomRangeCount { get; set; }

        /// <summary>
        /// <para>Unknown values of random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > Random Range</para>
        /// </summary>
        public AudioPathRandomUnknown[] RandomRangeUnknowns { get; set; }

        /// <summary>
        /// <para>Random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="AudioPositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Random Range</para>
        /// </summary>
        public AudioPathRandomRange[] RandomRanges { get; set; }

        /// <summary>
        /// <para>The Auxiliary Sends behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > General Settings</para>
        /// </summary>
        public AudioAuxSendsBehavior AuxSendsBehavior { get; set; }

        /// <summary>
        /// <para>The IDs of overriden auxiliary send buses of the audio object.</para>
        /// <para>Only exists when <see cref="AudioAuxSendsBehavior.OverrideAuxSends"/> is set.</para>
        /// </summary>
        public uint[] AuxiliarySendBusIds { get; set; }

        /// <summary>
        /// <para>The behavior when the playback limit of the audio object is reached.</para>
        /// <para>Located at: Sound Property Editor > Advaned Settings</para>
        /// </summary>
        public AudioLimitBehavior LimitBehavior { get; set; }

        /// <summary>
        /// <para>The behavior when the audio object is returning from being converted to a virtual voice.</para>
        /// <para>Located at: Sound Property Editor > Advanced Settings > Virtual Voice</para>
        /// </summary>
        public AudioVirtualVoiceReturnBehavior VirtualVoiceReturnBehavior { get; set; }

        /// <summary>
        /// Unknown byte. Appears to always be zero.
        /// </summary>
        public byte Unknown_3 { get; set; }

        /// <summary>
        /// Unknown byte. Appears to be always zero.
        /// </summary>
        public byte Unknown_4 { get; set; }

        /// <summary>
        /// <para>The behavior when the audio object is being converted to a virtual voice.</para>
        /// <para>Located at: Sound Property Editor > Advanced Settings > Virtual Voice</para>
        /// </summary>
        public AudioVirtualVoiceBehavior VirtualVoiceBehavior { get; set; }

        /// <summary>
        /// <para>The HDR behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > HDR > Envelope Tracking</para>
        /// </summary>
        public AudioHdrSettings HdrSettings { get; set; }

        /// <summary>
        /// <para>The count of State Groups belonging to the audio object.</para>
        /// <para>Determined by: Sound Property Editor > States</para>
        /// </summary>
        public uint StateGroupCount { get; set; }

        /// <summary>
        /// <para>State Groups belonging to the audio object.</para>
        /// <para>Located at: Sound Property Editor > States</para>
        /// </summary>
        public AudioStateGroup[] StateGroups { get; set; }

        /// <summary>
        /// <para>The count of RTPCs of the audio object.</para>
        /// <para>Determined by: Sound Property Editor > RTPC > (Add)</para>
        /// </summary>
        public ushort RtpcCount { get; set; }

        /// <summary>
        /// <para>RTPCs of the audio object.</para>
        /// <para>Located at: Sound Property Editor > RTPC > (Add)</para>
        /// </summary>
        public AudioRtpc[] Rtpcs { get; set; }
    }

    [Flags]
    public enum AudioBypassedEffects : byte
    {
        First = 0b_0000_0001,
        Second = 0b_0000_0010,
        Third = 0b_0000_0100,
        Fourth = 0b_0000_1000,
        All = 0b_0001_0000
    }

    public struct AudioEffect
    {
        /// <summary>
        /// <para>The index of the effect in the effect list.</para>
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
        public bool ShouldUseShareSets { get; set; }

        /// <summary>
        /// <para>Whether the effect is pre-rendered onto the media.</para>
        /// <para>If true, other fields will be set to zero.</para>
        /// <para>Located at: Effects > Effects > Render</para>
        /// </summary>
        public bool IsRendered { get; set; }
    }

    [Flags]
    public enum AudioPlaybackBehavior : byte
    {
        OverridePlaybackPriority = 0b0000_0001,
        OffsetPlaybackPriority = 0b0000_0010,
        OverrideMidiEvents = 0b0000_0100,
        MidiEventsBreakOnNoteOff = 0b0010_0000,
        OverrideMidiNoteTracking = 0b0000_1000,
        EnableMidiNoteTracking = 0b0001_0000
    }

    public enum AudioParameterType : byte
    {
        /// <summary>
        /// <para>The voice volume.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Volume</para>
        /// </summary>
        VoiceVolume,

        /// <summary>
        /// <para>The voice pitch.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Pitch</para>
        /// </summary>
        VoicePitch = 0x02,

        /// <summary>
        /// <para>The voice low-pass filter.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > Low-pass filter</para>
        /// </summary>
        VoiceLowPass,

        /// <summary>
        /// <para>The voice high-pass filter.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Voice > High-pass filter</para>
        /// </summary>
        VoiceHighPass,

        /// <summary>
        /// <para>The bus volume.</para>
        /// <para>Located at: Audio Bus Property Editor > General Settings > Bus > Volume</para>
        /// </summary>
        BusVolume,
        MakeUpGain,
        PlaybackPriority,
        PlaybackPriorityOffset,
        MotionToVolumeOffset,
        MotionLowPass,

        /// <summary>
        /// <para>The x-axis coordinate of the point on the positioning panner.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > 2D > Edit... > X:</para>
        /// </summary>
        PositioningPannerX = 0x0C,

        /// <summary>
        /// <para>The y-axis coordinate of the point on the positioning panner.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > 2D > Edit... > Y:</para>
        /// </summary>
        PositioningPannerY,

        /// <summary>
        /// <para>The positioning center percentage.</para>
        /// <para>Located at: Audio Bus Property Editor > Positioning > Center %</para>
        /// </summary>
        PositioningCenterPercentage,
        ActionDelay,
        ActionFadeInTime,
        Probability,
        OverrideAuxBus0Volume = 0x13,
        OverrideAuxBus1Volume,
        OverrideAuxBus2Volume,
        OverrideAuxBus3Volume,
        GameDefinedAuxSendVolume,
        OverrideBusVolume,
        OverrideBusHighPassFilter,

        /// <summary>
        /// <para>The low-pass filter value of the bus used as override.</para>
        /// </summary>
        OverrideBusLowPassFilter,

        /// <summary>
        /// <para>The HDR dynamics threshold.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Threshold</para>
        /// </summary>
        HdrThreshold,

        /// <summary>
        /// <para>The HDR dynamics ratio.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Ratio</para>
        /// </summary>
        HdrRatio,

        /// <summary>
        /// <para>The HDR dynamics release time.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Dynamics > Release Time</para>
        /// </summary>
        HdrReleaseTime,

        /// <summary>
        /// <para>The HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > (Select)</para>
        /// </summary>
        HdrOutputGameParam,

        /// <summary>
        /// <para>The minimum HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > Min</para>
        /// </summary>
        HdrOutputGameParamMin,

        /// <summary>
        /// <para>The maximum HDR window top output Game Parameter value.</para>
        /// <para>Located at: Audio Bus Property Editor > HDR > Window Top Output Game Parameter > Max</para>
        /// </summary>
        HdrOutputGameParamMax,
        HdrEnvelopeActiveRange,
        MidiNoteTrackingUnknown = 0x2E,
        MidiTransposition_Int,
        MidiVelocityOffset_Int,
        MidiFiltersKeyRangeMin,
        MidiFiltersKeyRangeMax,
        MidiFiltersVelocityRangeMin,
        MidiFiltersVelocityRangeMax,
        PlaybackSpeed = 0x36,
        MidiClipTempoSourceIsFile,
        LoopTime_UInt = 0x3A,
        InitialDelay
    }

    public struct AudioParameterPair
    {
        public float Parameter_1 { get; set; }

        public float Parameter_2 { get; set; }
    }

    [Flags]
    public enum AudioPositioningBehavior : byte
    {
        OverrideParent = 0b_0000_0001,
        TwoDimensional = 0b_0000_0010,
        Enable2dPanner = 0b_0000_0100,
        ThreeDimensional = 0b_0000_1000,
        EnableSpatialization = 0b_0001_0000,
        UserDefinedShouldLoop = 0b_0010_0000,
        UpdateAtEachFrame = 0b_0100_0000,
        IgnoreListenerOrientation = 0b_1000_0000
    }

    [Flags]
    public enum AudioUserDefinedPositioningBehavior : byte
    {
        IsRandom = 0b_0001,
        IsContinuous = 0b_0010,
        PickNewPathWhenSoundStarts = 0b_0100
    }

    public struct AudioControlPointKey
    {
        /// <summary>
        /// <para>The x-axis coordinate of the control point key.</para>
        /// <para>Located at: Position Editor (3D User-defined) > X:</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// <para>The z-axis coordinate of the control point key.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Z:</para>
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// <para>The y-axis coordinate of the control point key.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Y:</para>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// <para>The timestamp of the control point key, in milliseconds.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Key</para>
        /// </summary>
        public uint Timestamp { get; set; }
    }

    public struct AudioPathRandomUnknown
    {
        /// <summary>
        /// An unknown value.
        /// </summary>
        public uint Unknown_0 { get; set; }

        /// <summary>
        /// An unknown value.
        /// </summary>
        public uint Unknown_4 { get; set; }
    }

    public struct AudioPathRandomRange
    {
        /// <summary>
        /// <para>The left/right random range of the path.</para>
        /// <para>Location: Position Editor (3D User-defined) > Random Range > Left/Right</para>
        /// </summary>
        public float LeftRight { get; set; }

        /// <summary>
        /// <para>The front/back random range of the path.</para>
        /// <para>Location: Position Editor (3D User-defined) > Random Range > Front/Back</para>
        /// </summary>
        public float FrontBack { get; set; }

        /// <summary>
        /// <para>The up/down random range of the path.</para>
        /// <para>Location: Position Editor (3D User-defined) > Random Range > Up/Down</para>
        /// </summary>
        public float UpDown { get; set; }
    }

    [Flags]
    public enum AudioAuxSendsBehavior : byte
    {
        OverrideGameDefined = 0b_0001,
        UseGameDefinedAuxSends = 0b_0010,
        OverrideUserDefined = 0b_0100,
        OverrideAuxSends = 0b_1000
    }

    [Flags]
    public enum AudioLimitBehavior : byte
    {
        DiscardNewest = 0b_0000_0001,
        UseVirtual = 0b_0000_0010,
        LimitGlobally = 0b_0000_0100,
        OverrideParentPlaybackLimit = 0b_0000_1000,
        OverrideParentVirtualVoice = 0b_0001_0000
    }

    public enum AudioVirtualVoiceReturnBehavior : byte
    {
        PlayFromBeginning,
        PlayFromElapsedTime,
        Resume
    }

    public enum AudioVirtualVoiceBehavior : byte
    {
        ContinueToPlay,
        KillVoice,
        SendToVirtualVoice
    }

    [Flags]
    public enum AudioHdrSettings : byte
    {
        OverrideEnvelopeTracking = 0b_0001,
        OverrideLoudnessNormalization = 0b_0010,
        EnableLoudnessNormalization = 0b_0100,
        EnableEnvelope = 0b_1000
    }
}
