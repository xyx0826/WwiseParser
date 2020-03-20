namespace WwiseParserLib.Structures.Objects.STMG
{
    public class STMGGameParameter : STMGObject
    {
        /// <summary>
        /// <para>The default value of the Game Parameter.</para>
        /// <para>Located at: Game Parameter Property Editor > Range > Default</para>
        /// </summary>
        public float Default { get; set; }

        /// <summary>
        /// <para>The interpolation mode of the Game Parameter.</para>
        /// <para>Located at: Game Parameter Property Editor > Interpolation > Mode</para>
        /// </summary>
        public GameParameterInterpolation Interpolation { get; set; }

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
        public GameParameterBuiltInParameter BoundTo { get; set; }
    }

    public enum GameParameterInterpolation : uint
    {
        None,
        SlewRate,
        FilteringOverTime
    }

    public enum GameParameterBuiltInParameter : byte
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
