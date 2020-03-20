namespace WwiseParser.Structures.Objects.STMG
{
    class StateGroup : STMGObjectBase
    {
        /// <summary>
        /// <para>The default transition time, in milliseconds, of the State Group.</para>
        /// <para>Located at: State Group Property Editor > Transitions > Default transition time</para>
        /// </summary>
        public uint DefaultTransitionTime { get; set; }

        /// <summary>
        /// <para>The count of custom transition times of the State Group.</para>
        /// <para>Bi-directional custom transition times are counted twice.</para>
        /// <para>Determined by: State Group Property Editor > Transitions > Custom Transition Time</para>
        /// </summary>
        public uint CustomTransitionTimeCount { get; set; }

        /// <summary>
        /// <para>Custom transition times of the State Group.</para>
        /// <para>Located at: State Group Property Editor > Transitions > Custom Transition Time</para>
        /// </summary>
        public CustomTransitionTime[] CustomTransitionTimes { get; set; }
    }

    struct CustomTransitionTime
    {
        /// <summary>
        /// <para>The ID of the "from" State of the Custom Transition Time.</para>
        /// <para>Is zero if "None" selected.</para>
        /// <para>Located at: State Group Property Editor > Transitions > Custom Transition Time > From</para>
        /// </summary>
        public uint FromStateId { get; set; }

        /// <summary>
        /// <para>The ID of the "to" State of the Custom Transition Time.</para>
        /// <para>Is zero if "None" selected.</para>
        /// <para>Located at: State Group Property Editor > Transitions > Custom Transition Time > To</para>
        /// </summary>
        public uint ToStateId { get; set; }

        /// <summary>
        /// <para>The duration of the custom transition time, in milliseconds.</para>
        /// <para>Located at: State Group Property Editor > Transitions > Custom Transition Time > Time</para>
        /// </summary>
        public uint TransitionTime { get; set; }
    }
}
