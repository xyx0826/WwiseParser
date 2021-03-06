﻿using System;
using WwiseParser.Structures.Objects.HIRC.Structs;

namespace WwiseParser.Structures.Objects.HIRC
{
    class SwitchContainer : HIRCObjectBase
    {
        public SwitchContainer(int length) : base(HIRCObjectType.SwitchContainer, (uint)length)
        {

        }

        /// <summary>
        /// Additional properties of the Switch Container.
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// <para>The type of group used by the Switch Container.</para>
        /// <para>Determined by: Switch Container Property Editor > Switch > Group</para>
        /// </summary>
        public SwitchType SwitchType { get; set; }

        /// <summary>
        /// <para>The ID of the group used by the switch container.</para>
        /// <para>Located at: Switch Container Property Editor > Switch > Group</para>
        /// </summary>
        public uint GroupId { get; set; }

        /// <summary>
        /// <para>The default switch/state used by the switch container.</para>
        /// <para>Located at: Switch Container Property Editor > Switch > Default Switch/State</para>
        /// </summary>
        public uint DefaultSwitchOrStateId { get; set; }

        /// <summary>
        /// <para>The play mode of the Switch Container.</para>
        /// <para>Located at: Switch Container Property Editor > Play Mode</para>
        /// </summary>
        public PlayMode Mode { get; set; }

        /// <summary>
        /// <para>The count of children of the Switch Container that are assigned to switches or states.</para>
        /// <para>Determined by: Contents Editor > Assigned Objects</para>
        /// </summary>
        public uint AssignedChildCount { get; set; }

        /// <summary>
        /// <para>IDs of children of the Switch Container that are assigned to switches or states.</para>
        /// <para>Located at: Contents Editor > Assigned Objects</para>
        /// </summary>
        public uint[] AssignedChildIds { get; set; }

        /// <summary>
        /// <para>The count of switches or states of the Switch Group or State Group used by the Switch Container.</para>
        /// </summary>
        public uint SwitchOrStateCount { get; set; }

        /// <summary>
        /// <para>IDs of switches or states of the Switch Group or State Group used by the Switch Container.</para>
        /// </summary>
        public SwitchOrState[] SwitchOrStates { get; set; }

        /// <summary>
        /// <para>The count of children of the Switch Container.</para>
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// <para>Children of the Switch Container.</para>
        /// </summary>
        public SwitchChild[] Children { get; set; }
    }

    enum SwitchType : byte
    {
        NoneOrSwitchGroup,
        StateGroup
    }

    enum PlayMode : byte
    {
        Step,
        Continuous
    }

    struct SwitchOrState
    {
        public uint Id { get; set; }

        public uint AssignedChildCount { get; set; }

        public uint[] AssignedChildIds { get; set; }
    }

    struct SwitchChild
    {
        public uint Id { get; set; }

        public PlayBehavior Behavior { get; set; }

        public byte Unknown { get; set; }

        public uint FadeOut { get; set; }

        public uint FadeIn { get; set; }
    }

    [Flags]
    enum PlayBehavior : byte
    {
        FirstOnly              = 0b_0001,
        ContinueAcrossSwitches = 0b_0010
    }
}
