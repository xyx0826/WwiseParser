namespace WwiseParser.Structures.Objects.STMG
{
    class GameParameter : STMGObjectBase
    {
        /// <summary>
        /// <para>The default value of the Game Parameter.</para>
        /// <para>Located at: Game Parameter Property Editor > Range > Default</para>
        /// </summary>
        public float DefaultValue { get; set; }

        /// <summary>
        /// <para>The interpolation mode of the Game Parameter.</para>
        /// <para>Located at: Game Parameter Property Editor > Interpolation > Mode</para>
        /// </summary>
        public GameParameterInterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// <para>The interpolation attack of the Game Parameter, in units/seconds.</para>
        /// <para>Located at: Game Parameter Property Editor > Interpolation > Attack</para>
        /// </summary>
        public float InterpolationAttack { get; set; }

        /// <summary>
        /// <para>The interpolation release of the Game Parameter, in units/seconds.</para>
        /// <para>Located at: Game Parameter Property Editor > Interpolation > Release</para>
        /// </summary>
        public float InterpolationRelease { get; set; }

        /// <summary>
        /// <para>The built-in parameter that the Game Parameter is bound to.</para>
        /// <para>Located at: Game Parameter Property Editor > Bind to Built-In Parameter</para>
        /// </summary>
        public GameParameterBuiltInParameter BoundBuiltInParameter { get; set; }
    }

    enum GameParameterInterpolationMode : uint
    {
        None,
        SlewRate,
        FilteringOverTime
    }

    enum GameParameterBuiltInParameter : byte
    {
        None,
        Distance,
        Azimuth,
        Elevation,
        ObjectToListenerAngle,
        Obstruction,
        Occlusion
    }
}
