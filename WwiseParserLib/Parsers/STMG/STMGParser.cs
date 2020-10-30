using System.IO;
using WwiseParserLib.Structures.Objects.STMG;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Parsers.STMG
{
    public static class STMGParser
    {
        public static STMGSection Parse(byte[] blob)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var stmgSection = new STMGSection(blob.Length);
                stmgSection.VolumeThreshold = reader.ReadSingle();
                stmgSection.MaxVoiceInstances = reader.ReadUInt16();
                stmgSection.StateGroupCount = reader.ReadUInt32();
                stmgSection.StateGroups = new STMGStateGroup[stmgSection.StateGroupCount];
                for (var i = 0; i < stmgSection.StateGroupCount; i++)
                {
                    var stateGroup = new STMGStateGroup();
                    stateGroup.Id = reader.ReadUInt32();
                    stateGroup.DefaultTransitionTime = reader.ReadUInt32();
                    stateGroup.CustomTransitionCount = reader.ReadUInt32();
                    stateGroup.CustomTransitions = new StateGroupCustomTransition[stateGroup.CustomTransitionCount];
                    for (var j = 0; j < stateGroup.CustomTransitionCount; j++)
                    {
                        StateGroupCustomTransition customTransitionTime = default;
                        customTransitionTime.FromStateId = reader.ReadUInt32();
                        customTransitionTime.ToStateId = reader.ReadUInt32();
                        customTransitionTime.TransitionTime = reader.ReadUInt32();
                        stateGroup.CustomTransitions[j] = customTransitionTime;
                    }
                    stmgSection.StateGroups[i] = stateGroup;
                }
                stmgSection.ParameterDependentSwitchGroupCount = reader.ReadUInt32();
                stmgSection.ParameterDependentSwitchGroups = new STMGSwitchGroup[stmgSection.ParameterDependentSwitchGroupCount];
                for (var i = 0; i < stmgSection.ParameterDependentSwitchGroupCount; i++)
                {
                    var switchGroup = new STMGSwitchGroup();
                    switchGroup.Id = reader.ReadUInt32();
                    switchGroup.GameParameterId = reader.ReadUInt32();
                    switchGroup.Unknown = reader.ReadByte();
                    switchGroup.SwitchPointCount = reader.ReadUInt32();
                    switchGroup.SwitchPoints = new SwitchGroupPoint[switchGroup.SwitchPointCount];
                    for (var j = 0; j < switchGroup.SwitchPointCount; j++)
                    {
                        SwitchGroupPoint switchPoint = default;
                        switchPoint.X = reader.ReadSingle();
                        switchPoint.Y = reader.ReadUInt32();
                        switchPoint.FollowingCurveShape = reader.ReadUInt32();
                        switchGroup.SwitchPoints[j] = switchPoint;
                    }
                    stmgSection.ParameterDependentSwitchGroups[i] = switchGroup;
                }
                stmgSection.GameParameterCount = reader.ReadUInt32();
                stmgSection.GameParameters = new STMGGameParameter[stmgSection.GameParameterCount];
                for (var i = 0; i < stmgSection.GameParameterCount; i++)
                {
                    var gameParameter = new STMGGameParameter();
                    gameParameter.Id = reader.ReadUInt32();
                    gameParameter.Default = reader.ReadSingle();
                    gameParameter.Interpolation = (GameParameterInterpolation)reader.ReadUInt32();
                    gameParameter.InterpolationAttack = reader.ReadSingle();
                    gameParameter.InterpolationRelease = reader.ReadSingle();
                    gameParameter.BoundTo = (GameParameterBuiltInParameter)reader.ReadByte();
                    stmgSection.GameParameters[i] = gameParameter;
                }

                return stmgSection;
            }
        }
    }
}
