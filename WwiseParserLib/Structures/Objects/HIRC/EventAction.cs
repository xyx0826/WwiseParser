using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class EventAction : HIRCObject
    {
        public EventAction(int length) : base(HIRCObjectType.EventAction, (uint)length)
        {

        }

        public EventActionScope Scope { get; set; }

        public EventActionType ActionType { get; set; }

        public uint ObjectId { get; set; }

        public byte Unknown_06 { get; set; }   // 0

        public byte ParameterCount { get; set; }

        public AudioParameterType[] ParameterTypes { get; set; }

        public float[] ParameterValues { get; set; }

        public byte Unknown_08 { get; set; }   // 0

        public EventActionSettings Settings { get; set; }
    }

    public enum EventActionScope : byte
    {
        SwitchOrTrigger = 0x01,
        Global,
        GameObject,
        State,
        All,
        AllAlt = 0x08,
        AllExcept
    }

    public enum EventActionType : byte
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

    public abstract class EventActionSettings
    {

    }

    public class EventActionUnknownSettings : EventActionSettings
    {
        public byte[] Blob { get; set; }
    }

    public class EventActionPlaySettings : EventActionSettings
    {
        public AudioCurveShapeByte FadeInCurve { get; set; }

        public uint ObjectSoundBankId { get; set; }
    }
}
