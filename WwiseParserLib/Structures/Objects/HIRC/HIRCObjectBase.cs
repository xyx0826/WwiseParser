using System;
using System.Collections.Generic;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    /// <summary>
    /// The base class of all HIRC section Wwise object structures.
    /// </summary>
    public class HIRCObjectBase : WwiseObjectBase
    {
        private HIRCObjectBase()
        {

        }

        protected HIRCObjectBase(HIRCObjectType type, uint length)
        {
            Type = type;
            Length = length;
        }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public HIRCObjectType Type { get; private set; }

        /// <summary>
        /// The length of the object, in bytes, excluding the type and the length.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// The ID of the object.
        /// </summary>
        public uint Id { get; set; }

        public override bool Equals(object obj)
        {
            return obj is HIRCObjectBase @base &&
                   Type == @base.Type &&
                   Id == @base.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Id);
        }

        public static bool operator ==(HIRCObjectBase left, HIRCObjectBase right)
        {
            return EqualityComparer<HIRCObjectBase>.Default.Equals(left, right);
        }

        public static bool operator !=(HIRCObjectBase left, HIRCObjectBase right)
        {
            return !(left == right);
        }
    }

    public enum HIRCObjectType : byte
    {
        Unknown,
        Settings,
        Sound,
        EventAction,
        Event,
        Container,
        SwitchContainer,
        ActorMixer,
        AudioBus,
        BlendContainer,
        MusicSegment,
        MusicTrack,
        MusicSwitchContainer,
        MusicPlaylistContainer,
        Attenuation,
        DialogueEvent,
        MotionBus,
        MotionEffect,
        Effect,
        Unknown_0x13,
        AuxiliaryBus
    }
}
