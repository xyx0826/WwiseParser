namespace WwiseParser.Structures.Objects.HIRC.Structs
{
    struct Rtpc
    {
        /// <summary>
        /// <para>The MIDI effect ID of the RTPC when <see cref="IsMidi"/>, otherwise Game Parameter or LFO ID on the x-axis.</para>
        /// <para>Set this property through base class Id.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > X Axis</para>
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// <para>Whether the RTPC is using MIDI.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > X Axis > (Select) > MIDI</para>
        /// </summary>
        public bool IsMidi;

        /// <summary>
        /// <para>Whether the y-axis setting of the RTPC is voice settings or bus volume.</para>
        /// <para>Determined by: Audio Bus Property Editor > RTPC > Y Axis</para>
        /// </summary>
        public bool IsGeneralSettings;

        /// <summary>
        /// <para>The type of the parameter on the y-axis of the RTPC.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > Y Axis</para>
        /// </summary>
        public RtpcParameterType Parameter;

        /// <summary>
        /// An unknown ID.
        /// </summary>
        public uint UnknownId;

        /// <summary>
        /// <para>The selected curve scaling type in the context menu of the curve of the RTPC.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public CurveScalingType CurveScalingType;

        /// <summary>
        /// <para>The count of points on the curve of the RTPC.</para>
        /// <para>Determined by: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public ushort PointCount;

        /// <summary>
        /// <para>Points on the curve of the RTPC.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public RtpcPoint[] Points;
    }

    enum RtpcParameterType : byte
    {
        VoiceVolume = 0x00,
        VoicePitch = 0x02,
        VoiceLowPassFilter = 0x03,
        VoiceHighPassFilter = 0x04,
        BusVolume = 0x05,
        SoundInstanceLimit = 0x11,
        CenterPercentage = 0x18,
        BypassEffect0 = 0x1D,
        BypassEffect1 = 0x1E,
        BypassEffect2 = 0x1F,
        BypassEffect3 = 0x20,
        BypassAllEffects = 0x21,
        HdrThreshold = 0x22,
        HdrReleaseTime = 0x23,
        HdrRatio = 0x24
    }

    enum CurveScalingType : byte
    {
        Linear = 0x00,
        DB = 0x02
    }

    struct RtpcPoint
    {
        /// <summary>
        /// <para>The x-axis coordinate of the RTPC point.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public float X;

        /// <summary>
        /// <para>The y-axis coordinate of the RTPC point.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public float Y;

        /// <summary>
        /// <para>The shape of the curve following the point.</para>
        /// <para>Located at: Audio Bus Property Editor > RTPC > (Editor)</para>
        /// </summary>
        public CurveShape FollowingCurveShape;

        /// <summary>
        /// Three unknown bytes.
        /// </summary>
        public byte[] Unknown;
    }

}
