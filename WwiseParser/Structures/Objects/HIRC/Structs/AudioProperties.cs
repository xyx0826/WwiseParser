using System;

namespace WwiseParser.Structures.Objects.HIRC.Structs
{
    struct AudioProperties
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
        public BypassedEffects BypassedEffects { get; set; }

        /// <summary>
        /// <para>Effects on the audio object.</para>
        /// <para>Located at: Sound Property Editor > Effects > Effects</para>
        /// </summary>
        public Effect[] Effects { get; set; }

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
        /// <para>The MIDI override behaviors of the sound object.</para>
        /// <para>Located at: Sound Property Editor > MIDI</para>
        /// </summary>
        public Override MidiOverride { get; set; }

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
        /// Unknown byte. Seems to be always zero.
        /// </summary>
        public byte Unknown_2 { get; set; }

        /// <summary>
        /// <para>The spatial positioning behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > Positioning</para>
        /// </summary>
        public PositioningBehavior Positioning { get; set; }

        /// <summary>
        /// <para>Whether to use game-defined 3D position source for the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Position Source > Game-defined</para>
        /// </summary>
        public bool IsGameDefined { get; set; }

        /// <summary>
        /// <para>The ID of the attenuation object for the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Attenuation > (Select)</para>
        /// </summary>
        public uint AttenuationId { get; set; }

        /// <summary>
        /// <para>The settings of user-defined position source editor for the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Sound Property Editor > Positioning > 3D > Position Source > Edit...</para>
        /// </summary>
        public PositionEditorSettings UserDefinedPlaySettings { get; set; }

        /// <summary>
        /// <para>The transition time of this audio object, in milliseconds.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Play Mode > Transition time</para>
        /// </summary>
        public uint TransitionTime { get; set; }

        /// <summary>
        /// <para>The count of control point keys on the timeline of the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > (Editor)</para>
        /// </summary>
        public uint ControlPointKeyCount { get; set; }

        /// <summary>
        /// <para>Control point keys on the timeline of the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > (Editor)</para>
        public ControlPointKey[] ControlPointKeys { get; set; }

        /// <summary>
        /// <para>The count of random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > Random Range</para>
        public uint RandomRangeCount { get; set; }

        /// <summary>
        /// <para>Unknown values of random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Determined by: Position Editor (3D User-defined) > Random Range</para>
        public RandomRangeUnknown[] RandomRangeUnknowns { get; set; }

        /// <summary>
        /// <para>Random ranges of the audio object.</para>
        /// <para>Only exists when <see cref="PositioningBehavior.ThreeDimensional"/> is set, and <see cref="IsGameDefined"/> is false.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Random Range</para>
        public RandomRange[] RandomRanges { get; set; }

        /// <summary>
        /// <para>The Auxiliary Sends behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > General Settings</para>
        /// </summary>
        public AuxSendsBehavior AuxSendsBehavior { get; set; }

        /// <summary>
        /// <para>The IDs of overriden auxiliary send buses of the audio object.</para>
        /// <para>Only exists when <see cref="AuxSendsBehavior.OverrideAuxSends"/> is set.</para>
        /// </summary>
        public uint[] AuxiliarySendBusIds { get; set; }

        /// <summary>
        /// <para>The behavior when the playback limit of the audio object is reached.</para>
        /// <para>Located at: Sound Property Editor > Advaned Settings</para>
        /// </summary>
        public SoundLimitBehavior LimitBehavior { get; set; }

        /// <summary>
        /// <para>The behavior when the audio object is returning from being converted to a virtual voice.</para>
        /// <para>Located at: Sound Property Editor > Advanced Settings > Virtual Voice</para>
        /// </summary>
        public VirtualVoiceReturnBehavior VirtualVoiceReturnBehavior { get; set; }

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
        public VirtualVoiceBehavior VirtualVoiceBehavior { get; set; }

        /// <summary>
        /// <para>The HDR behaviors of the audio object.</para>
        /// <para>Located at: Sound Property Editor > HDR > Envelope Tracking</para>
        /// </summary>
        public HdrBehavior HdrBehavior { get; set; }

        /// <summary>
        /// <para>The count of State Groups belonging to the audio object.</para>
        /// <para>Determined by: Sound Property Editor > States</para>
        /// </summary>
        public uint StateGroupCount { get; set; }

        /// <summary>
        /// <para>State Groups belonging to the audio object.</para>
        /// <para>Located at: Sound Property Editor > States</para>
        /// </summary>
        public StateGroup[] StateGroups { get; set; }

        /// <summary>
        /// <para>The count of RTPCs of the audio object.</para>
        /// <para>Determined by: Sound Property Editor > RTPC > (Add)</para>
        /// </summary>
        public ushort RtpcCount { get; set; }

        /// <summary>
        /// <para>RTPCs of the audio object.</para>
        /// <para>Located at: Sound Property Editor > RTPC > (Add)</para>
        public Rtpc[] Rtpcs { get; set; }
    }

    [Flags]
    enum Override : byte
    {
        OverridePlaybackPriority = 0b0000_0001,
        OffsetPlaybackPriority = 0b0000_0010,
        OverrideMidiEvents = 0b0000_0100,
        MidiEventsBreakOnNoteOff = 0b0010_0000,
        OverrideMidiNoteTracking = 0b0000_1000,
        EnableMidiNoteTracking = 0b0001_0000
    }

    enum AudioParameterType : byte
    {
        VoiceVolume,
        VoicePitch = 0x02,
        VoiceLowPass,
        VoiceHighPass,
        BusVolume,
        MakeUpGain,
        PlaybackPriority,
        PlaybackPriorityOffset,
        MotionToVolumeOffset,
        MotionLowPass,
        PositioningPannerX = 0x0C,
        PositioningPannerY,
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
        OverrideBusLowPassFilter,
        HdrThreshold,
        HdrRatio,
        HdrReleaseTime,
        HdrOutputGameParam,
        HdrOutputGameParamMin,
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

    [Flags]
    enum PositioningBehavior : byte
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
    enum PositionEditorSettings : byte
    {
        IsRandom = 0b_0001,
        IsContinuous = 0b_0010,
        PickNewPathWhenSoundStarts = 0b_0100
    }

    struct ControlPointKey
    {
        /// <summary>
        /// <para>The x-axis coordinate of the control point key.</para>
        /// <para>Located at: Position Editor (3D User-defined) > X:</para>
        public float X { get; set; }

        /// <summary>
        /// <para>The z-axis coordinate of the control point key.</para>
        /// <para>Located at: Position Editor (3D User-defined) > Z:</para>
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

    struct RandomRangeUnknown
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

    struct RandomRange
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
    enum AuxSendsBehavior : byte
    {
        OverrideGameDefined = 0b_0001,
        UseGameDefinedAuxSends = 0b_0010,
        OverrideUserDefined = 0b_0100,
        OverrideAuxSends = 0b_1000
    }

    [Flags]
    enum SoundLimitBehavior : byte
    {
        DiscardNewest = 0b_0000_0001,
        UseVirtual = 0b_0000_0010,
        LimitGlobally = 0b_0000_0100,
        OverrideParentPlaybackLimit = 0b_0000_1000,
        OverrideParentVirtualVoice = 0b_0001_0000
    }

    enum VirtualVoiceReturnBehavior : byte
    {
        PlayFromBeginning,
        PlayFromElapsedTime,
        Resume
    }

    enum VirtualVoiceBehavior : byte
    {
        ContinueToPlay,
        KillVoice,
        SendToVirtualVoice
    }

    [Flags]
    enum HdrBehavior : byte
    {
        OverrideEnvelopeTracking = 0b_0001,
        OverrideLoudnessNormalization = 0b_0010,
        EnableLoudnessNormalization = 0b_0100,
        EnableEnvelope = 0b_1000
    }
}
