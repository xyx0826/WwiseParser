using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class EventAction : HIRCObjectBase
    {
        public EventAction(int length) : base(HIRCObjectType.EventAction, (uint)length)
        {

        }

        public Scope Scope { get; set; }

        public ActionType ActionType { get; set; }

        public uint ObjectId { get; set; }

        public byte Unknown_06 { get; set; }   // 0

        public byte ParameterCount { get; set; }

        public AudioParameterType[] ParameterTypes { get; set; }

        public float[] ParameterValues { get; set; }

        public byte Unknown_08 { get; set; }   // 0

        public EventActionSettings Settings { get; set; }
    }

    enum Scope : byte
    {
        SwitchOrTrigger = 0x01,
        Global,
        GameObject,
        State,
        All,
        AllAlt = 0x08,
        AllExcept
    }

    enum ActionType : byte
    {
        Stop = 0x01,
        Pause,
        Resume,
        Play,
        Trigger,
        Mute,
        UnMute,
        SetVoicePitch,
        ResetVoicePitch,
        SetVoiceVolume,
        ResetVoiceVolume,
        SetBusVolume,
        ResetBusVolume,
        SetVoiceLowPassFilter,
        ResetVoiceLowPassFilter,
        EnableState,
        DisableState,
        SetState,
        SetGameParameter,
        ResetGameParameter,
        SetSwitch,
        ToggleBypass,
        ResetBypassEffect,
        Break,
        Seek
    }

    class EventActionSettings
    {

    }

    class UnknownSettings : EventActionSettings
    {
        public byte[] Blob { get; set; }
    }

    class PlaySettings : EventActionSettings
    {
        public CurveShape FadeInCurve { get; set; }

        public uint ObjectSoundBankId { get; set; }
    }
}
