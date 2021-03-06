﻿using WwiseParser.Structures.Objects.STMG;

namespace WwiseParser.Structures.Sections
{
    class STMGSection : SectionBase
    {
        public STMGSection(uint length)
        {
            Name = SectionName.STMG;
            Length = length;
        }

        /// <summary>
        /// <para>The volume threshold of the Wwise project.</para>
        /// <para>Located at: Project Settings > Volume Threshold</para>
        /// </summary>
        public float VolumeThreshold { get; set; }

        /// <summary>
        /// <para>The maximum count of voice instances of the Wwise project.</para>
        /// <para>Located at: Project Settings > Max Voice Instances</para>
        /// </summary>
        public ushort MaxVoiceInstances { get; set; }

        /// <summary>
        /// <para>The count of State Groups of the Wwise project.</para>
        /// <para>Determined by: Project Explorer > Game Syncs > States</para>
        /// </summary>
        public uint StateGroupCount { get; set; }

        /// <summary>
        /// <para>State Groups of the Wwise project.</para>
        /// <para>Located at: Project Explorer > Game Syncs > States</para>
        /// </summary>
        public StateGroup[] StateGroups { get; set; }

        /// <summary>
        /// <para>The count of Switch Groups of the Wwise project.</para>
        /// <para>Only groups with the following settings enabled are included:</para>
        /// <para>Switch Group Property Editor > Game Parameter > Use Game Parameter</para>
        /// <para>Determined by: Project Explorer > Game Syncs > Switches</para>
        /// </summary>
        public uint ParameterDependentSwitchGroupCount { get; set; }

        /// <summary>
        /// <para>Switch Groups of the Wwise project.</para>
        /// <para>Only groups with the following settings enabled are included:</para>
        /// <para>Switch Group Property Editor > Game Parameter > Use Game Parameter</para>
        /// <para>Located at: Project Explorer > Game Syncs > Switches</para>
        /// </summary>
        public SwitchGroup[] ParameterDependentSwitchGroups { get; set; }

        /// <summary>
        /// <para>The count of Game Parameters of the Wwise project.</para>
        /// <para>Determined by: Project Explorer > Game Syncs > Game Parameters</para>
        /// </summary>
        public uint GameParameterCount { get; set; }

        /// <summary>
        /// <para>Game Parameters of the Wwise project.</para>
        /// <para>Located at: Project Explorer > Game Syncs > Game Parameters</para>
        /// </summary>
        public GameParameter[] GameParameters { get; set; }
    }
}
