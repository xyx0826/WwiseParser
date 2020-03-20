using System.IO;
using WwiseParser.Structures.Objects.STMG;
using WwiseParser.Structures.Sections;

namespace WwiseParser.Structures.Parsers.STMG
{
    static class STMGParser
    {
        public static STMGSection ParseSection(byte[] blob)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var stmgSection = new STMGSection((uint)blob.Length);
                stmgSection.VolumeThreshold = reader.ReadSingle();
                stmgSection.MaxVoiceInstances = reader.ReadUInt16();
                stmgSection.StateGroupCount = reader.ReadUInt32();
                stmgSection.StateGroups = new StateGroup[stmgSection.StateGroupCount];
                for (var i = 0; i < stmgSection.StateGroupCount; i++)
                {
                    var stateGroup = new StateGroup();
                    stateGroup.Id = reader.ReadUInt32();
                    stateGroup.DefaultTransitionTime = reader.ReadUInt32();
                    stateGroup.CustomTransitionTimeCount = reader.ReadUInt32();
                    stateGroup.CustomTransitionTimes = new CustomTransitionTime[stateGroup.CustomTransitionTimeCount];
                    for (var j = 0; j < stateGroup.CustomTransitionTimeCount; j++)
                    {
                        CustomTransitionTime customTransitionTime = default;
                        customTransitionTime.FromStateId = reader.ReadUInt32();
                        customTransitionTime.ToStateId = reader.ReadUInt32();
                        customTransitionTime.TransitionTime = reader.ReadUInt32();
                        stateGroup.CustomTransitionTimes[j] = customTransitionTime;
                    }
                    stmgSection.StateGroups[i] = stateGroup;
                }
                stmgSection.ParameterDependentSwitchGroupCount = reader.ReadUInt32();
                stmgSection.ParameterDependentSwitchGroups = new SwitchGroup[stmgSection.ParameterDependentSwitchGroupCount];
                for (var i = 0; i < stmgSection.ParameterDependentSwitchGroupCount; i++)
                {
                    var switchGroup = new SwitchGroup();
                    switchGroup.Id = reader.ReadUInt32();
                    switchGroup.GameParameterId = reader.ReadUInt32();
                    switchGroup.Unknown_08 = reader.ReadByte();
                    switchGroup.SwitchPointCount = reader.ReadUInt32();
                    switchGroup.SwitchPoints = new SwitchPoint[switchGroup.SwitchPointCount];
                    for (var j = 0; j < switchGroup.SwitchPointCount; j++)
                    {
                        SwitchPoint switchPoint = default;
                        switchPoint.X = reader.ReadSingle();
                        switchPoint.Y = reader.ReadUInt32();
                        switchPoint.FollowingCurveShape = reader.ReadUInt32();
                        switchGroup.SwitchPoints[j] = switchPoint;
                    }
                    stmgSection.ParameterDependentSwitchGroups[i] = switchGroup;
                }
                stmgSection.GameParameterCount = reader.ReadUInt32();
                stmgSection.GameParameters = new GameParameter[stmgSection.GameParameterCount];
                for (var i = 0; i < stmgSection.GameParameterCount; i++)
                {
                    var gameParameter = new GameParameter();
                    gameParameter.Id = reader.ReadUInt32();
                    gameParameter.DefaultValue = reader.ReadSingle();
                    gameParameter.InterpolationMode = (GameParameterInterpolationMode)reader.ReadUInt32();
                    gameParameter.InterpolationAttack = reader.ReadSingle();
                    gameParameter.InterpolationRelease = reader.ReadSingle();
                    gameParameter.BoundBuiltInParameter = (GameParameterBuiltInParameter)reader.ReadByte();
                    stmgSection.GameParameters[i] = gameParameter;
                }

                return stmgSection;
            }
        }
    }
}
