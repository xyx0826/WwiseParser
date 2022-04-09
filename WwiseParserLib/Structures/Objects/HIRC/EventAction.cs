using System;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class EventAction : HIRCObjectBase
    {
        public EventAction(int length) : base(HIRCObjectType.EventAction, (uint)length)
        {

        }

        public EventActionScope Scope { get; set; }

        public EventActionType ActionType { get; set; }

        public uint ObjectId { get; set; }

        public byte Unknown_06 { get; set; }   // 0, 1 appears in D2BL

        public byte ParameterCount { get; set; }

        public AudioParameterType[] ParameterTypes { get; set; }

        public int[] ParameterValues { get; set; }

        public byte ParameterPairCount { get; set; }

        public byte[] ParameterPairTypes { get; set; }

        public AudioParameterPair[] ParameterPairValues { get; set; }

        public EventActionSettings Settings { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class HasSettingsAttribute : Attribute
    {
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
        [HasSettings] Stop = 0x01,
        [HasSettings] Pause,
        [HasSettings] Resume,
        [HasSettings] Play,
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
        SetSwitch = 0x19,
        ToggleBypass,
        ResetBypassEffect,
        Break,
        [HasSettings] Seek = 0x1e
    }

    public abstract class EventActionSettings
    {

    }

    public class EventActionException
    {
        /// <summary>
        /// The ID of the exception object.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Unknown byte. Always zero.
        /// </summary>
        public byte Unknown { get; set; }
    }

    public abstract class EventActionSettingsWithExceptions : EventActionSettings
    {
        public byte ExceptionCount { get; set; }

        public EventActionException[] Exceptions { get; set; }
    }

    public class EventActionUnknownSettings : EventActionSettings
    {
        public byte[] Blob { get; set; }
    }

    public class EventActionPlaySettings : EventActionSettings
    {
        public AudioCurveShapeByte FadeInCurve { get; set; }

        public uint TargetSoundBankId { get; set; }
    }

    public class EventActionStopSettings : EventActionSettingsWithExceptions
    {
        public AudioCurveShapeByte FadeOutCurve { get; set; }

        /// <summary>
        /// The LSB represents IncludeDelayedResumeActions.
        /// </summary>
        public byte Flag { get; set; }
    }

    public class EventActionPauseSettings : EventActionSettingsWithExceptions
    {
        public AudioCurveShapeByte FadeOutCurve { get; set; }
    }

    public class EventActionResumeSettings : EventActionSettingsWithExceptions
    {
        public AudioCurveShapeByte FadeInCurve { get; set; }

        /// <summary>
        /// The LSB represents MasterResume.
        /// </summary>
        public byte Flag { get; set; }
    }

    public enum EventActionSeekType : byte
    {
        Time,
        Percent
    }

    public class EventActionSeekSettings : EventActionSettingsWithExceptions
    {
        public EventActionSeekType SeekType { get; set; }

        public float Seek { get; set; }

        public uint Unknown_0 { get; set; }

        public uint Unknown_1 { get; set; }

        public bool SeekToNearestMarker { get; set; }
    }
}
