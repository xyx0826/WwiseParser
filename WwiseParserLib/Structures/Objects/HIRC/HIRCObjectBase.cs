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
