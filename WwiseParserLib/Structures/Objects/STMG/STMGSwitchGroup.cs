namespace WwiseParserLib.Structures.Objects.STMG
{
    public class STMGSwitchGroup : STMGObject
    {
        /// <summary>
        /// The ID of the Game Parameter that the Switch Group is bound to.
        /// <para>Located at: Switch Group Property Editor > Coordinates > (Select)</para>
        /// </summary>
        public uint GameParameterId { get; set; }

        /// <summary>
        /// An unknown value. Appears to always be zero.
        /// </summary>
        public byte Unknown { get; set; }

        /// <summary>
        /// <para>The count of points on the curve.</para>
        /// <para>Determined by: Switch Group Property Editor > Game Parameter > (Editor)</para>
        /// </summary>
        public uint SwitchPointCount { get; set; }

        /// <summary>
        /// <para>Points on the curve.</para>
        /// <para>Located at: Switch Group Property Editor > Game Parameter > (Editor)</para>
        /// </summary>
        public SwitchGroupPoint[] SwitchPoints { get; set; }
    }

    public struct SwitchGroupPoint
    {
        /// <summary>
        /// <para>The x-axis coordinate of the point.</para>
        /// <para>Located at: Switch Group Property Editor > Game Parameter > (Editor)</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// <para>The ID of the object corresponding to the y-axis coordinate of the point.</para>
        /// <para>Located at: Switch Group Property Editor > Game Parameter > (Editor)</para>
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// <para>The shape of the curve following the point. Appears to be always 9 (constant).</para>
        /// </summary>
        public uint FollowingCurveShape { get; set; }
    }
}
