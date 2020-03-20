namespace WwiseParser.Structures.Objects.HIRC
{
    internal class HIRCObjectBase : ObjectBase
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
        public HIRCObjectType Type { get; set; }

        /// <summary>
        /// The length of the object, in bytes, excluding the type and the length.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// The ID of the object.
        /// </summary>
        public uint Id { get; set; }
    }

    enum HIRCObjectType : byte
    {
        //Settings = 0x01,
        Sound = 0x02,
        EventAction = 0x03,
        Event = 0x04,
        Container = 0x05,
        SwitchContainer = 0x06,
        ActorMixer = 0x07,
        AudioBus = 0x08,
        BlendContainer = 0x09,
        MusicSegment = 0x0A,
        MusicTrack = 0x0B,
        MusicSwitchContainer = 0x0C
    }
}
