﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WwiseParserLib.Structures.Objects.HIRC;
using WwiseParserLib.Structures.Objects.HIRC.Structs;
using WwiseParserLib.Structures.Chunks;
using System.Text;

namespace WwiseParserLib.Parsers
{
    /// <summary>
    /// Parser for HIRC (hierarchy) chunks.
    /// </summary>
    public static class HIRCParser
    {
        /// <summary>
        /// Parses a HIRC chunk.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <param name="noParse">Whether to only extract blobs and not parse fields.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHierarchyChunk Parse(byte[] blob, bool noParse = false)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var hircSection = new SoundBankHierarchyChunk(blob.Length);
                hircSection.ObjectCount = reader.ReadUInt32();
                hircSection.Objects = new HIRCObjectBase[hircSection.ObjectCount];
                var objects = new Dictionary<byte, int>();
                for (var i = 0; i < hircSection.ObjectCount; i++)
                {
                    var objectType = reader.ReadByte();
                    var objectLength = reader.ReadUInt32();
                    var objectBlob = reader.ReadBytes((int)objectLength);

                    HIRCObjectBase hircObject;

                    if (noParse)
                    {
                        hircObject = ParseUnknown(objectType, objectBlob);
                    }
                    else
                    {
                        switch ((HIRCObjectType)objectType)
                        {
                            case HIRCObjectType.Settings:
                                hircObject = ParseSettings(objectBlob);
                                break;

                            case HIRCObjectType.Sound:
                                hircObject = ParseSound(objectBlob);
                                break;

                            case HIRCObjectType.EventAction:
                                hircObject = ParseEventAction(objectBlob);
                                break;

                            case HIRCObjectType.Event:
                                hircObject = ParseEvent(objectBlob);
                                break;

                            case HIRCObjectType.Container:
                                hircObject = ParseContainer(objectBlob);
                                break;

                            case HIRCObjectType.SwitchContainer:
                                hircObject = ParseSwitchContainer(objectBlob);
                                break;

                            case HIRCObjectType.ActorMixer:
                                hircObject = ParseActorMixer(objectBlob);
                                break;

                            case HIRCObjectType.AudioBus:
                                hircObject = ParseAudioBus(objectBlob, false);
                                break;

                            case HIRCObjectType.BlendContainer:
                                hircObject = ParseBlendContainer(objectBlob);
                                break;

                            case HIRCObjectType.MusicSegment:
                                hircObject = ParseMusicSegment(objectBlob);
                                break;

                            case HIRCObjectType.MusicTrack:
                                hircObject = ParseMusicTrack(objectBlob);
                                break;

                            case HIRCObjectType.MusicSwitchContainer:
                                hircObject = ParseMusicSwitchContainer(objectBlob);
                                break;

                            case HIRCObjectType.MusicPlaylistContainer:
                                hircObject = ParseMusicPlaylistContainer(objectBlob);
                                break;

                            case HIRCObjectType.DialogueEvent:
                                hircObject = ParseDialogueEvent(objectBlob);
                                break;

                            case HIRCObjectType.AuxiliaryBus:
                                hircObject = ParseAudioBus(objectBlob, true) as AuxiliaryBus;
                                break;

                            default:
                                hircObject = ParseUnknown(objectType, objectBlob);
                                break;
                        }
                    }

                    hircSection.Objects[i] = hircObject;
                }

                return hircSection;
            }
        }

        /// <summary>
        /// Parses a HIRC chunk from a Wwise 2013 SoundBank.
        /// Support for Wwise 2013 is incomplete - currently only the Interactive Music Hierarchy is implemented.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <param name="noParse">Whether to only extract blobs and not parse fields.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHierarchyChunk Parse2013(byte[] blob, bool noParse = false)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var hircSection = new SoundBankHierarchyChunk(blob.Length);
                hircSection.ObjectCount = reader.ReadUInt32();
                hircSection.Objects = new HIRCObjectBase[hircSection.ObjectCount];
                var objects = new Dictionary<byte, int>();
                for (var i = 0; i < hircSection.ObjectCount; i++)
                {
                    var objectType = reader.ReadByte();
                    var objectLength = reader.ReadUInt32();
                    var objectBlob = reader.ReadBytes((int)objectLength);

                    HIRCObjectBase hircObject;

                    if (noParse)
                    {
                        hircObject = ParseUnknown(objectType, objectBlob);
                    }
                    else
                    {
                        switch ((HIRCObjectType)objectType)
                        {
                            case HIRCObjectType.MusicSegment:
                                hircObject = ParseMusicSegment2013(objectBlob);
                                break;

                            case HIRCObjectType.MusicTrack:
                                hircObject = ParseMusicTrack2013(objectBlob);
                                break;

                            case HIRCObjectType.MusicSwitchContainer:
                                hircObject = ParseMusicSwitchContainer2013(objectBlob);
                                break;

                            case HIRCObjectType.MusicPlaylistContainer:
                                hircObject = ParseMusicPlaylistContainer2013(objectBlob);
                                break;

                            default:
                                hircObject = ParseUnknown(objectType, objectBlob);
                                break;
                        }
                    }

                    hircSection.Objects[i] = hircObject;
                }

                return hircSection;
            }
        }

        /// <summary>
        /// Parses a HIRC chunk from a Wwise 2019 SoundBank.
        /// Support for Wwise 2019 is incomplete - currently only the Interactive Music Hierarchy is implemented.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <param name="noParse">Whether to only extract blobs and not parse fields.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHierarchyChunk Parse2019(byte[] blob, bool noParse = false)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var hircSection = new SoundBankHierarchyChunk(blob.Length);
                hircSection.ObjectCount = reader.ReadUInt32();
                hircSection.Objects = new HIRCObjectBase[hircSection.ObjectCount];
                var objects = new Dictionary<byte, int>();
                for (var i = 0; i < hircSection.ObjectCount; i++)
                {
                    var objectType = reader.ReadByte();
                    var objectLength = reader.ReadUInt32();
                    var objectBlob = reader.ReadBytes((int)objectLength);

                    HIRCObjectBase hircObject;

                    if (noParse)
                    {
                        hircObject = ParseUnknown(objectType, objectBlob);
                    }
                    else
                    {
                        hircObject = (HIRCObjectType) objectType switch
                        {
                            HIRCObjectType.Event => ParseEvent2019(objectBlob),
                            HIRCObjectType.EventAction => ParseEventAction(objectBlob),
                            HIRCObjectType.MusicSegment => ParseMusicSegment2019(objectBlob),
                            HIRCObjectType.MusicTrack => ParseMusicTrack2019(objectBlob),
                            HIRCObjectType.MusicSwitchContainer => ParseMusicSwitchContainer2019(objectBlob),
                            HIRCObjectType.MusicPlaylistContainer => ParseMusicPlaylistContainer2019(objectBlob),
                            _ => ParseUnknown(objectType, objectBlob)
                        };
                    }

                    hircSection.Objects[i] = hircObject;
                }

                return hircSection;
            }
        }

        /// <summary>
        /// Parses a HIRC chunk from a Wwise 2021 SoundBank.
        /// Support for Wwise 2021 is incomplete - currently only the Interactive Music Hierarchy is implemented.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <param name="noParse">Whether to only extract blobs and not parse fields.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHierarchyChunk Parse2021(byte[] blob, bool noParse = false)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var hircSection = new SoundBankHierarchyChunk(blob.Length);
                hircSection.ObjectCount = reader.ReadUInt32();
                hircSection.Objects = new HIRCObjectBase[hircSection.ObjectCount];
                var objects = new Dictionary<byte, int>();
                for (var i = 0; i < hircSection.ObjectCount; i++)
                {
                    var objectType = reader.ReadByte();
                    var objectLength = reader.ReadUInt32();
                    var objectBlob = reader.ReadBytes((int)objectLength);

                    HIRCObjectBase hircObject;

                    if (noParse)
                    {
                        hircObject = ParseUnknown(objectType, objectBlob);
                    }
                    else
                    {
                        hircObject = (HIRCObjectType)objectType switch
                        {
                            HIRCObjectType.Event => ParseEvent2019(objectBlob),
                            HIRCObjectType.EventAction => ParseEventAction(objectBlob),
                            HIRCObjectType.MusicSegment => ParseMusicSegment2021(objectBlob),
                            HIRCObjectType.MusicTrack => ParseMusicTrack2021(objectBlob),
                            HIRCObjectType.MusicSwitchContainer => ParseMusicSwitchContainer2021(objectBlob),
                            HIRCObjectType.MusicPlaylistContainer => ParseMusicPlaylistContainer2021(objectBlob),
                            _ => ParseUnknown(objectType, objectBlob)
                        };
                    }

                    hircSection.Objects[i] = hircObject;
                }

                return hircSection;
            }
        }

        /// <summary>
        /// Parses a HIRC chunk from a Wwise 2023 SoundBank.
        /// Support for Wwise 2023 is incomplete - currently only the Interactive Music Hierarchy is implemented.
        /// </summary>
        /// <param name="blob">Chunk data to parse, without the leading type magic.</param>
        /// <param name="noParse">Whether to only extract blobs and not parse fields.</param>
        /// <returns>The parsed chunk.</returns>
        public static SoundBankHierarchyChunk Parse2023(byte[] blob, bool noParse = false)
        {
            using (var reader = new BinaryReader(new MemoryStream(blob)))
            {
                var hircSection = new SoundBankHierarchyChunk(blob.Length);
                hircSection.ObjectCount = reader.ReadUInt32();
                hircSection.Objects = new HIRCObjectBase[hircSection.ObjectCount];
                var objects = new Dictionary<byte, int>();
                for (var i = 0; i < hircSection.ObjectCount; i++)
                {
                    var objectType = reader.ReadByte();
                    var objectLength = reader.ReadUInt32();
                    var objectBlob = reader.ReadBytes((int) objectLength);

                    HIRCObjectBase hircObject;

                    if (noParse)
                    {
                        hircObject = ParseUnknown(objectType, objectBlob);
                    }
                    else
                    {
                        hircObject = (HIRCObjectType) objectType switch
                        {
                            HIRCObjectType.Event => ParseEvent2019(objectBlob),
                            HIRCObjectType.EventAction => ParseEventAction(objectBlob),
                            HIRCObjectType.MusicSegment => ParseMusicSegment2023(objectBlob),
                            HIRCObjectType.MusicTrack => ParseMusicTrack2023(objectBlob),
                            HIRCObjectType.MusicSwitchContainer => ParseMusicSwitchContainer2023(objectBlob),
                            HIRCObjectType.MusicPlaylistContainer => ParseMusicPlaylistContainer2023(objectBlob),
                            _ => ParseUnknown(objectType, objectBlob)
                        };
                    }

                    hircSection.Objects[i] = hircObject;
                }

                return hircSection;
            }
        }

        public static AudioBus ParseAudioBus(byte[] data, bool auxiliary)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var audioBus = auxiliary ? new AuxiliaryBus(data.Length) : new AudioBus(data.Length);
                audioBus.Id = reader.ReadUInt32();
                audioBus.ParentId = reader.ReadUInt32();
                audioBus.ParameterCount = reader.ReadByte();
                audioBus.ParameterTypes = new AudioParameterType[audioBus.ParameterCount];
                for (var i = 0; i < audioBus.ParameterCount; i++)
                {
                    audioBus.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
                }
                audioBus.ParameterValues = new float[audioBus.ParameterCount];
                for (var i = 0; i < audioBus.ParameterCount; i++)
                {
                    audioBus.ParameterValues[i] = reader.ReadSingle();
                }
                audioBus.Positioning = (AudioBusPositioningBehavior)reader.ReadByte();
                audioBus.OnPlaybackLimitReached = (AudioBusLimitBehavior)reader.ReadByte();
                audioBus.SoundInstanceLimit = reader.ReadUInt16();
                audioBus.Channel = (AudioBusChannelType)reader.ReadUInt32();
                audioBus.HdrReleaseMode = (AudioBusHdrReleaseMode)reader.ReadByte();
                audioBus.AutoDuckingRecoveryTime = reader.ReadUInt32();
                audioBus.AutoDuckingMaxVolume = reader.ReadSingle();

                audioBus.DuckedBusCount = reader.ReadUInt32();
                audioBus.DuckedBuses = new AudioBusDuckedBus[audioBus.DuckedBusCount];
                for (var i = 0; i < audioBus.DuckedBusCount; i++)
                {
                    AudioBusDuckedBus duckedBus = default;
                    duckedBus.Id = reader.ReadUInt32();
                    duckedBus.Volume = reader.ReadSingle();
                    duckedBus.FadeOut = reader.ReadUInt32();
                    duckedBus.FadeIn = reader.ReadUInt32();
                    duckedBus.CurveShape = (AudioCurveShapeByte)reader.ReadByte();
                    duckedBus.Target = (AudioBusDuckTarget)reader.ReadByte();
                    audioBus.DuckedBuses[i] = duckedBus;
                }

                audioBus.EffectCount = reader.ReadByte();
                if (audioBus.EffectCount > 0)
                {
                    audioBus.BypassedEffects = (AudioBypassedEffects)reader.ReadByte();
                }
                audioBus.Effects = new AudioEffect[audioBus.EffectCount];
                for (var i = 0; i < audioBus.EffectCount; i++)
                {
                    AudioEffect effect = default;
                    effect.Index = reader.ReadByte();
                    effect.Id = reader.ReadUInt32();
                    effect.ShouldUseShareSets = reader.ReadBoolean();
                    effect.IsRendered = reader.ReadBoolean();
                    audioBus.Effects[i] = effect;
                }

                audioBus.UnknownBytes = reader.ReadBytes(6);
                audioBus.RtpcCount = reader.ReadUInt16();
                audioBus.Rtpcs = new AudioRtpc[audioBus.RtpcCount];
                for (var i = 0; i < audioBus.RtpcCount; i++)
                {
                    AudioRtpc rtpc = new AudioRtpc();
                    rtpc.X = reader.ReadUInt32();
                    rtpc.IsMidi = reader.ReadBoolean();
                    rtpc.IsGeneralSettings = reader.ReadBoolean();
                    rtpc.Parameter = (RtpcParameterType)reader.ReadByte();
                    rtpc.UnknownId = reader.ReadUInt32();
                    rtpc.CurveScalingType = (RtpcCurveType)reader.ReadByte();
                    rtpc.PointCount = reader.ReadUInt16();
                    rtpc.Points = new RtpcPoint[rtpc.PointCount];
                    for (var j = 0; j < rtpc.PointCount; j++)
                    {
                        RtpcPoint rtpcPoint = new RtpcPoint();
                        rtpcPoint.X = reader.ReadSingle();
                        rtpcPoint.Y = reader.ReadSingle();
                        rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte)reader.ReadByte();
                        rtpcPoint.Unknown = reader.ReadBytes(3);
                        rtpc.Points[j] = rtpcPoint;
                    }
                    audioBus.Rtpcs[i] = rtpc;
                }

                audioBus.StateGroupCount = reader.ReadUInt32();
                audioBus.StateGroups = new AudioStateGroup[audioBus.StateGroupCount];
                for (var i = 0; i < audioBus.StateGroupCount; i++)
                {
                    AudioStateGroup stateGroup = new AudioStateGroup();
                    stateGroup.Id = reader.ReadUInt32();
                    stateGroup.MusicChangeAt = (MusicKeyPointByte)reader.ReadByte();
                    stateGroup.StateWithSettingsCount = reader.ReadUInt16();
                    stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                    for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                    {
                        AudioStateWithSettings AudioStateWithSettings = default;
                        AudioStateWithSettings.StateId = reader.ReadUInt32();
                        AudioStateWithSettings.SettingsId = reader.ReadUInt32();
                        stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                    }
                    audioBus.StateGroups[i] = stateGroup;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return audioBus;
            }
        }

        public static Sound ParseSound(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var sound = new Sound(data.Length);
                sound.Id = reader.ReadUInt32();
                sound.Unknown_04 = reader.ReadByte();
                sound.Unknown_05 = reader.ReadByte();
                if (sound.Unknown_04 > 1)
                {
                    Console.WriteLine("Encountered sound object with Unknown_04 > 1. Skipping to AudioProperties.");
                    reader.BaseStream.Seek(0x10, SeekOrigin.Current);
                }
                else
                {
                    sound.Conversion = (SoundConversionType)reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    sound.Source = (SoundSource)reader.ReadByte();
                    sound.AudioId = reader.ReadUInt32();
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType)reader.ReadByte();
                }
                sound.Properties = reader.ReadAudioProperties();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return sound;
            }
        }

        public static Container ParseContainer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var container = new Container(data.Length);
                container.Id = reader.ReadUInt32();
                container.Properties = reader.ReadAudioProperties();
                container.LoopCount = reader.ReadUInt16();
                container.Unknown_1 = reader.ReadUInt32();
                container.TransitionDuration = reader.ReadSingle();
                container.Unknown_2 = reader.ReadSingle();
                container.Unknown_3 = reader.ReadSingle();
                container.AvoidLastPlayedCount = reader.ReadUInt16();
                container.Transition = (ContainerTransitionType)reader.ReadByte();
                container.Shuffle = reader.ReadBoolean();
                container.PlayType = (ContainerPlayType)reader.ReadByte();
                container.Behavior = (ContainerSequenceBehavior)reader.ReadByte();
                container.ChildCount = reader.ReadUInt32();
                container.ChildIds = new uint[container.ChildCount];
                for (var i = 0; i < container.ChildCount; i++)
                {
                    container.ChildIds[i] = reader.ReadUInt32();
                }
                container.UnknownParameterCount = reader.ReadUInt16();
                container.UnknownParameters = new ContainerUnknownParameter[container.UnknownParameterCount];
                for (var i = 0; i < container.UnknownParameterCount; i++)
                {
                    ContainerUnknownParameter containerUnknownParameter = default;
                    containerUnknownParameter.Id = reader.ReadUInt32();
                    containerUnknownParameter.Parameter = reader.ReadUInt32();
                    container.UnknownParameters[i] = containerUnknownParameter;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return container;
            }
        }

        public static ActorMixer ParseActorMixer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var actorMixer = new ActorMixer(data.Length);
                actorMixer.Id = reader.ReadUInt32();
                actorMixer.Properties = reader.ReadAudioProperties();
                actorMixer.ChildCount = reader.ReadUInt32();
                actorMixer.ChildIds = new uint[actorMixer.ChildCount];
                for (var i = 0; i < actorMixer.ChildCount; i++)
                {
                    actorMixer.ChildIds[i] = reader.ReadUInt32();
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return actorMixer;
            }
        }

        public static SwitchContainer ParseSwitchContainer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var switchContainer = new SwitchContainer(data.Length);
                switchContainer.Id = reader.ReadUInt32();
                switchContainer.Properties = reader.ReadAudioProperties();
                switchContainer.SwitchType = (SwitchType)reader.ReadByte();
                switchContainer.GroupId = reader.ReadUInt32();
                switchContainer.DefaultSwitchOrStateId = reader.ReadUInt32();
                switchContainer.Mode = (SwitchContainerMode)reader.ReadByte();
                switchContainer.ChildCount = reader.ReadUInt32();
                switchContainer.ChildIds = new uint[switchContainer.ChildCount];
                for (var i = 0; i < switchContainer.ChildCount; i++)
                {
                    switchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                switchContainer.SwitchOrStateCount = reader.ReadUInt32();
                switchContainer.SwitchOrStates = new SwitchOrState[switchContainer.SwitchOrStateCount];
                for (var i = 0; i < switchContainer.SwitchOrStateCount; i++)
                {
                    SwitchOrState switchOrState = default;
                    switchOrState.Id = reader.ReadUInt32();
                    switchOrState.AssignedChildCount = reader.ReadUInt32();
                    switchOrState.AssignedChildIds = new uint[switchOrState.AssignedChildCount];
                    for (var j = 0; j < switchOrState.AssignedChildCount; j++)
                    {
                        switchOrState.AssignedChildIds[j] = reader.ReadUInt32();
                    }
                    switchContainer.SwitchOrStates[i] = switchOrState;
                }
                switchContainer.SwitchChildCount = reader.ReadUInt32();
                switchContainer.SwitchChildren = new SwitchChild[switchContainer.SwitchChildCount];
                for (var i = 0; i < switchContainer.SwitchChildCount; i++)
                {
                    SwitchChild switchChild = default;
                    switchChild.Id = reader.ReadUInt32();
                    switchChild.Behavior = (SwitchPlayBehavior)reader.ReadByte();
                    switchChild.Unknown = reader.ReadByte();
                    switchChild.FadeOut = reader.ReadUInt32();
                    switchChild.FadeIn = reader.ReadUInt32();
                    switchContainer.SwitchChildren[i] = switchChild;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return switchContainer;
            }
        }

        public static BlendContainer ParseBlendContainer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var blendContainer = new BlendContainer(data.Length);
                blendContainer.Id = reader.ReadUInt32();
                blendContainer.Properties = reader.ReadAudioProperties();
                blendContainer.ChildCount = reader.ReadUInt32();
                blendContainer.ChildIds = new uint[blendContainer.ChildCount];
                for (var i = 0; i < blendContainer.ChildCount; i++)
                {
                    blendContainer.ChildIds[i] = reader.ReadUInt32();
                }
                blendContainer.BlendTrackCount = reader.ReadUInt32();
                blendContainer.BlendTracks = new BlendTrack[blendContainer.BlendTrackCount];
                for (var i = 0; i < blendContainer.BlendTrackCount; i++)
                {
                    BlendTrack blendTrack = default;
                    blendTrack.Id = reader.ReadUInt32();
                    blendTrack.RuleCount = reader.ReadUInt16();
                    blendTrack.Rules = new BlendTrackRule[blendTrack.RuleCount];
                    for (var j = 0; j < blendTrack.RuleCount; j++)
                    {
                        BlendTrackRule blendRule = default;
                        blendRule.X = reader.ReadUInt32();
                        blendRule.XType = (BlendTrackXAxisType)reader.ReadByte();
                        blendRule.Unknown_05 = reader.ReadByte();
                        blendRule.YType = (BlendTrackYAxisType)reader.ReadByte();
                        blendRule.UnknownId = reader.ReadUInt32();
                        blendRule.Unknown_0B = reader.ReadByte();
                        blendRule.PointCount = reader.ReadUInt16();
                        blendRule.Points = new MusicCurvePoint[blendRule.PointCount];
                        for (var k = 0; k < blendRule.PointCount; k++)
                        {
                            MusicCurvePoint point = default;
                            point.X = reader.ReadSingle();
                            point.Y = reader.ReadSingle();
                            point.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                            blendRule.Points[k] = point;
                        }
                        blendTrack.Rules[j] = blendRule;
                    }
                    blendTrack.CrossfadeId = reader.ReadUInt32();
                    blendTrack.Unknown = reader.ReadByte();
                    blendTrack.ChildCount = reader.ReadUInt32();
                    blendTrack.Children = new BlendTrackChild[blendTrack.ChildCount];
                    for (var j = 0; j < blendTrack.ChildCount; j++)
                    {
                        BlendTrackChild blendTrackChild = default;
                        blendTrackChild.Id = reader.ReadUInt32();
                        blendTrackChild.CrossfadePointCount = reader.ReadUInt32();
                        blendTrackChild.CrossfadePoints = new MusicCurvePoint[blendTrackChild.CrossfadePointCount];
                        for (var k = 0; k < blendTrackChild.CrossfadePointCount; k++)
                        {
                            MusicCurvePoint crossfadePoint = default;
                            crossfadePoint.X = reader.ReadSingle();
                            crossfadePoint.Y = reader.ReadSingle();
                            crossfadePoint.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                            blendTrackChild.CrossfadePoints[k] = crossfadePoint;
                        }
                        blendTrack.Children[j] = blendTrackChild;
                    }
                    blendContainer.BlendTracks[i] = blendTrack;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return blendContainer;
            }
        }

        public static MusicSegment ParseMusicSegment2013(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSegment = new MusicSegment(data.Length);
                musicSegment.Id = reader.ReadUInt32();
                musicSegment.Properties = reader.ReadAudioProperties2013();
                musicSegment.ChildCount = reader.ReadUInt32();
                musicSegment.ChildIds = new uint[musicSegment.ChildCount];
                for (var i = 0; i < musicSegment.ChildCount; i++)
                {
                    musicSegment.ChildIds[i] = reader.ReadUInt32();
                }
                musicSegment.GridPeriodTime = reader.ReadDouble();
                musicSegment.GridOffsetTime = reader.ReadDouble();
                musicSegment.Tempo = reader.ReadSingle();
                musicSegment.TimeSignatureUpper = reader.ReadByte();
                musicSegment.TimeSignatureLower = reader.ReadByte();
                musicSegment.Unknown = reader.ReadByte();
                musicSegment.StingerCount = reader.ReadUInt32();
                musicSegment.Stingers = new MusicStinger[musicSegment.StingerCount];
                for (var i = 0; i < musicSegment.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSegment.Stingers[i] = stinger;
                }
                musicSegment.EndTrimOffset = reader.ReadDouble();
                musicSegment.MusicCueCount = reader.ReadUInt32();
                musicSegment.MusicCues = new MusicCue[musicSegment.MusicCueCount];
                for (var i = 0; i < musicSegment.MusicCueCount; i++)
                {
                    MusicCue musicCue = default;
                    musicCue.Id = reader.ReadUInt32();
                    musicCue.Time = reader.ReadDouble();
                    musicCue.CustomNameLength = reader.ReadUInt32();
                    if (musicCue.CustomNameLength > 0)
                    {
                        musicCue.CustomName = String.Concat(reader.ReadChars((int)musicCue.CustomNameLength - 1));
                        reader.BaseStream.Position++;   // Skip null byte
                    }
                    musicSegment.MusicCues[i] = musicCue;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSegment;
            }
        }

        public static MusicSegment ParseMusicSegment(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSegment = new MusicSegment(data.Length);
                musicSegment.Id = reader.ReadUInt32();
                musicSegment.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSegment.Properties = reader.ReadAudioProperties();
                musicSegment.ChildCount = reader.ReadUInt32();
                musicSegment.ChildIds = new uint[musicSegment.ChildCount];
                for (var i = 0; i < musicSegment.ChildCount; i++)
                {
                    musicSegment.ChildIds[i] = reader.ReadUInt32();
                }
                musicSegment.GridPeriodTime = reader.ReadDouble();
                musicSegment.GridOffsetTime = reader.ReadDouble();
                musicSegment.Tempo = reader.ReadSingle();
                musicSegment.TimeSignatureUpper = reader.ReadByte();
                musicSegment.TimeSignatureLower = reader.ReadByte();
                musicSegment.Unknown = reader.ReadByte();
                musicSegment.StingerCount = reader.ReadUInt32();
                musicSegment.Stingers = new MusicStinger[musicSegment.StingerCount];
                for (var i = 0; i < musicSegment.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSegment.Stingers[i] = stinger;
                }
                musicSegment.EndTrimOffset = reader.ReadDouble();
                musicSegment.MusicCueCount = reader.ReadUInt32();
                musicSegment.MusicCues = new MusicCue[musicSegment.MusicCueCount];
                for (var i = 0; i < musicSegment.MusicCueCount; i++)
                {
                    MusicCue musicCue = default;
                    musicCue.Id = reader.ReadUInt32();
                    musicCue.Time = reader.ReadDouble();
                    musicCue.CustomNameLength = reader.ReadUInt32();
                    if (musicCue.CustomNameLength > 0)
                    {
                        musicCue.CustomName = String.Concat(reader.ReadChars((int)musicCue.CustomNameLength - 1));
                        reader.BaseStream.Position++;   // Skip null byte
                    }
                    musicSegment.MusicCues[i] = musicCue;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSegment;
            }
        }

        public static MusicSegment ParseMusicSegment2019(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSegment = new MusicSegment(data.Length);
                musicSegment.Id = reader.ReadUInt32();
                musicSegment.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSegment.Properties = reader.ReadAudioProperties2019();
                musicSegment.ChildCount = reader.ReadUInt32();
                musicSegment.ChildIds = new uint[musicSegment.ChildCount];
                for (var i = 0; i < musicSegment.ChildCount; i++)
                {
                    musicSegment.ChildIds[i] = reader.ReadUInt32();
                }
                musicSegment.GridPeriodTime = reader.ReadDouble();
                musicSegment.GridOffsetTime = reader.ReadDouble();
                musicSegment.Tempo = reader.ReadSingle();
                musicSegment.TimeSignatureUpper = reader.ReadByte();
                musicSegment.TimeSignatureLower = reader.ReadByte();
                musicSegment.Unknown = reader.ReadByte();
                musicSegment.StingerCount = reader.ReadUInt32();
                musicSegment.Stingers = new MusicStinger[musicSegment.StingerCount];
                for (var i = 0; i < musicSegment.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSegment.Stingers[i] = stinger;
                }
                musicSegment.EndTrimOffset = reader.ReadDouble();
                musicSegment.MusicCueCount = reader.ReadUInt32();
                musicSegment.MusicCues = new MusicCue[musicSegment.MusicCueCount];
                for (var i = 0; i < musicSegment.MusicCueCount; i++)
                {
                    MusicCue musicCue = default;
                    musicCue.Id = reader.ReadUInt32();
                    musicCue.Time = reader.ReadDouble();
                    var customName = new StringBuilder();
                    // musicCue.CustomNameLength = reader.ReadUInt32();
                    //if (musicCue.CustomNameLength > 0)
                    //{
                    //    musicCue.CustomName = String.Concat(reader.ReadChars((int)musicCue.CustomNameLength - 1));
                    //    reader.BaseStream.Position++;   // Skip null byte
                    //}
                    // Wwise 2021
                    var c = reader.ReadByte();
                    while (c != 0)
                    {
                        customName.Append(Convert.ToChar(c));
                        c = reader.ReadByte();
                    }
                    if (customName.Length > 0)
                    {
                        musicCue.CustomName = customName.ToString();
                    }
                    musicSegment.MusicCues[i] = musicCue;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSegment;
            }
        }

        public static MusicSegment ParseMusicSegment2021(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSegment = new MusicSegment(data.Length);
                musicSegment.Id = reader.ReadUInt32();
                musicSegment.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSegment.Properties = reader.ReadAudioProperties2021();
                musicSegment.ChildCount = reader.ReadUInt32();
                musicSegment.ChildIds = new uint[musicSegment.ChildCount];
                for (var i = 0; i < musicSegment.ChildCount; i++)
                {
                    musicSegment.ChildIds[i] = reader.ReadUInt32();
                }
                musicSegment.GridPeriodTime = reader.ReadDouble();
                musicSegment.GridOffsetTime = reader.ReadDouble();
                musicSegment.Tempo = reader.ReadSingle();
                musicSegment.TimeSignatureUpper = reader.ReadByte();
                musicSegment.TimeSignatureLower = reader.ReadByte();
                musicSegment.Unknown = reader.ReadByte();
                musicSegment.StingerCount = reader.ReadUInt32();
                musicSegment.Stingers = new MusicStinger[musicSegment.StingerCount];
                for (var i = 0; i < musicSegment.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSegment.Stingers[i] = stinger;
                }
                musicSegment.EndTrimOffset = reader.ReadDouble();
                musicSegment.MusicCueCount = reader.ReadUInt32();
                musicSegment.MusicCues = new MusicCue[musicSegment.MusicCueCount];
                for (var i = 0; i < musicSegment.MusicCueCount; i++)
                {
                    MusicCue musicCue = default;
                    musicCue.Id = reader.ReadUInt32();
                    musicCue.Time = reader.ReadDouble();
                    var customName = new StringBuilder();
                    var c = reader.ReadByte();
                    while (c != 0)
                    {
                        customName.Append(Convert.ToChar(c));
                        c = reader.ReadByte();
                    }
                    if (customName.Length > 0)
                    {
                        musicCue.CustomName = customName.ToString();
                    }
                    musicSegment.MusicCues[i] = musicCue;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSegment;
            }
        }

        public static MusicSegment ParseMusicSegment2023(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSegment = new MusicSegment(data.Length);
                musicSegment.Id = reader.ReadUInt32();
                musicSegment.MidiBehavior = (MusicMidiBehavior) reader.ReadByte();
                musicSegment.Properties = reader.ReadAudioProperties2023();
                musicSegment.ChildCount = reader.ReadUInt32();
                musicSegment.ChildIds = new uint[musicSegment.ChildCount];
                for (var i = 0; i < musicSegment.ChildCount; i++)
                {
                    musicSegment.ChildIds[i] = reader.ReadUInt32();
                }
                musicSegment.GridPeriodTime = reader.ReadDouble();
                musicSegment.GridOffsetTime = reader.ReadDouble();
                musicSegment.Tempo = reader.ReadSingle();
                musicSegment.TimeSignatureUpper = reader.ReadByte();
                musicSegment.TimeSignatureLower = reader.ReadByte();
                musicSegment.Unknown = reader.ReadByte();
                musicSegment.StingerCount = reader.ReadUInt32();
                musicSegment.Stingers = new MusicStinger[musicSegment.StingerCount];
                for (var i = 0; i < musicSegment.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt) reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSegment.Stingers[i] = stinger;
                }
                musicSegment.EndTrimOffset = reader.ReadDouble();
                musicSegment.MusicCueCount = reader.ReadUInt32();
                musicSegment.MusicCues = new MusicCue[musicSegment.MusicCueCount];
                for (var i = 0; i < musicSegment.MusicCueCount; i++)
                {
                    MusicCue musicCue = default;
                    musicCue.Id = reader.ReadUInt32();
                    musicCue.Time = reader.ReadDouble();
                    var customName = new StringBuilder();
                    var c = reader.ReadByte();
                    while (c != 0)
                    {
                        customName.Append(Convert.ToChar(c));
                        c = reader.ReadByte();
                    }
                    if (customName.Length > 0)
                    {
                        musicCue.CustomName = customName.ToString();
                    }
                    musicSegment.MusicCues[i] = musicCue;
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSegment;
            }
        }

        public static MusicSwitchContainer ParseMusicSwitchContainer2013(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSwitchContainer = new MusicSwitchContainer(data.Length);
                musicSwitchContainer.Id = reader.ReadUInt32();
                musicSwitchContainer.Properties = reader.ReadAudioProperties2013();
                musicSwitchContainer.ChildCount = reader.ReadUInt32();
                musicSwitchContainer.ChildIds = new uint[musicSwitchContainer.ChildCount];
                for (var i = 0; i < musicSwitchContainer.ChildCount; i++)
                {
                    musicSwitchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GridPeriodTime = reader.ReadDouble();
                musicSwitchContainer.GridOffsetTime = reader.ReadDouble();
                musicSwitchContainer.Tempo = reader.ReadSingle();
                musicSwitchContainer.TimeSignatureUpper = reader.ReadByte();
                musicSwitchContainer.TimeSignatureLower = reader.ReadByte();
                musicSwitchContainer.Unknown_1 = reader.ReadByte();
                musicSwitchContainer.StingerCount = reader.ReadUInt32();
                musicSwitchContainer.Stingers = new MusicStinger[musicSwitchContainer.StingerCount];
                for (var i = 0; i < musicSwitchContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSwitchContainer.Stingers[i] = stinger;
                }
                musicSwitchContainer.TransitionCount = reader.ReadUInt32();
                musicSwitchContainer.Transitions = new MusicTransition[musicSwitchContainer.TransitionCount];
                for (var i = 0; i < musicSwitchContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadByte() == 0xFF;
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt16();
                    transition.PlayPreEntry = reader.ReadByte() == 0xFF;
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicSwitchContainer.Transitions[i] = transition;
                }
                musicSwitchContainer.ContinueOnSwitchChange = reader.ReadBoolean();
                musicSwitchContainer.GroupCount = reader.ReadUInt32();
                musicSwitchContainer.GroupIds = new uint[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GroupTypes = new bool[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupTypes[i] = reader.ReadBoolean();
                }
                musicSwitchContainer.PathSectionLength = reader.ReadUInt32();
                musicSwitchContainer.UseWeighted = reader.ReadBoolean();
                musicSwitchContainer.Paths = reader.ReadPaths(musicSwitchContainer.PathSectionLength, musicSwitchContainer.ChildIds);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSwitchContainer;
            }
        }

        public static MusicSwitchContainer ParseMusicSwitchContainer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSwitchContainer = new MusicSwitchContainer(data.Length);
                musicSwitchContainer.Id = reader.ReadUInt32();
                musicSwitchContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSwitchContainer.Properties = reader.ReadAudioProperties();
                musicSwitchContainer.ChildCount = reader.ReadUInt32();
                musicSwitchContainer.ChildIds = new uint[musicSwitchContainer.ChildCount];
                for (var i = 0; i < musicSwitchContainer.ChildCount; i++)
                {
                    musicSwitchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GridPeriodTime = reader.ReadDouble();
                musicSwitchContainer.GridOffsetTime = reader.ReadDouble();
                musicSwitchContainer.Tempo = reader.ReadSingle();
                musicSwitchContainer.TimeSignatureUpper = reader.ReadByte();
                musicSwitchContainer.TimeSignatureLower = reader.ReadByte();
                musicSwitchContainer.Unknown_1 = reader.ReadByte();
                musicSwitchContainer.StingerCount = reader.ReadUInt32();
                musicSwitchContainer.Stingers = new MusicStinger[musicSwitchContainer.StingerCount];
                for (var i = 0; i < musicSwitchContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSwitchContainer.Stingers[i] = stinger;
                }
                musicSwitchContainer.TransitionCount = reader.ReadUInt32();
                musicSwitchContainer.Transitions = new MusicTransition[musicSwitchContainer.TransitionCount];
                for (var i = 0; i < musicSwitchContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadByte() == 0xFF;
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt16();
                    transition.PlayPreEntry = reader.ReadByte() == 0xFF;
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicSwitchContainer.Transitions[i] = transition;
                }
                musicSwitchContainer.ContinueOnSwitchChange = reader.ReadBoolean();
                musicSwitchContainer.GroupCount = reader.ReadUInt32();
                musicSwitchContainer.GroupIds = new uint[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GroupTypes = new bool[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupTypes[i] = reader.ReadBoolean();
                }
                musicSwitchContainer.PathSectionLength = reader.ReadUInt32();
                musicSwitchContainer.UseWeighted = reader.ReadBoolean();
                musicSwitchContainer.Paths = reader.ReadPaths(musicSwitchContainer.PathSectionLength, musicSwitchContainer.ChildIds);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSwitchContainer;
            }
        }

        public static MusicSwitchContainer ParseMusicSwitchContainer2019(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSwitchContainer = new MusicSwitchContainer(data.Length);
                musicSwitchContainer.Id = reader.ReadUInt32();
                musicSwitchContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSwitchContainer.Properties = reader.ReadAudioProperties2019();
                musicSwitchContainer.ChildCount = reader.ReadUInt32();
                musicSwitchContainer.ChildIds = new uint[musicSwitchContainer.ChildCount];
                for (var i = 0; i < musicSwitchContainer.ChildCount; i++)
                {
                    musicSwitchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GridPeriodTime = reader.ReadDouble();
                musicSwitchContainer.GridOffsetTime = reader.ReadDouble();
                musicSwitchContainer.Tempo = reader.ReadSingle();
                musicSwitchContainer.TimeSignatureUpper = reader.ReadByte();
                musicSwitchContainer.TimeSignatureLower = reader.ReadByte();
                musicSwitchContainer.Unknown_1 = reader.ReadByte();
                musicSwitchContainer.StingerCount = reader.ReadUInt32();
                musicSwitchContainer.Stingers = new MusicStinger[musicSwitchContainer.StingerCount];
                for (var i = 0; i < musicSwitchContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSwitchContainer.Stingers[i] = stinger;
                }
                musicSwitchContainer.TransitionCount = reader.ReadUInt32();
                musicSwitchContainer.Transitions = new MusicTransition[musicSwitchContainer.TransitionCount];
                for (var i = 0; i < musicSwitchContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicSwitchContainer.Transitions[i] = transition;
                }
                musicSwitchContainer.ContinueOnSwitchChange = reader.ReadBoolean();
                musicSwitchContainer.GroupCount = reader.ReadUInt32();
                musicSwitchContainer.GroupIds = new uint[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GroupTypes = new bool[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupTypes[i] = reader.ReadBoolean();
                }
                musicSwitchContainer.PathSectionLength = reader.ReadUInt32();
                musicSwitchContainer.UseWeighted = reader.ReadBoolean();
                musicSwitchContainer.Paths = reader.ReadPaths(musicSwitchContainer.PathSectionLength, musicSwitchContainer.ChildIds);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSwitchContainer;
            }
        }

        public static MusicSwitchContainer ParseMusicSwitchContainer2021(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSwitchContainer = new MusicSwitchContainer(data.Length);
                musicSwitchContainer.Id = reader.ReadUInt32();
                musicSwitchContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicSwitchContainer.Properties = reader.ReadAudioProperties2021();
                musicSwitchContainer.ChildCount = reader.ReadUInt32();
                musicSwitchContainer.ChildIds = new uint[musicSwitchContainer.ChildCount];
                for (var i = 0; i < musicSwitchContainer.ChildCount; i++)
                {
                    musicSwitchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GridPeriodTime = reader.ReadDouble();
                musicSwitchContainer.GridOffsetTime = reader.ReadDouble();
                musicSwitchContainer.Tempo = reader.ReadSingle();
                musicSwitchContainer.TimeSignatureUpper = reader.ReadByte();
                musicSwitchContainer.TimeSignatureLower = reader.ReadByte();
                musicSwitchContainer.Unknown_1 = reader.ReadByte();
                musicSwitchContainer.StingerCount = reader.ReadUInt32();
                musicSwitchContainer.Stingers = new MusicStinger[musicSwitchContainer.StingerCount];
                for (var i = 0; i < musicSwitchContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSwitchContainer.Stingers[i] = stinger;
                }
                musicSwitchContainer.TransitionCount = reader.ReadUInt32();
                musicSwitchContainer.Transitions = new MusicTransition[musicSwitchContainer.TransitionCount];
                for (var i = 0; i < musicSwitchContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicSwitchContainer.Transitions[i] = transition;
                }
                musicSwitchContainer.ContinueOnSwitchChange = reader.ReadBoolean();
                musicSwitchContainer.GroupCount = reader.ReadUInt32();
                musicSwitchContainer.GroupIds = new uint[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GroupTypes = new bool[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupTypes[i] = reader.ReadBoolean();
                }
                musicSwitchContainer.PathSectionLength = reader.ReadUInt32();
                musicSwitchContainer.UseWeighted = reader.ReadBoolean();
                musicSwitchContainer.Paths = reader.ReadPaths(musicSwitchContainer.PathSectionLength, musicSwitchContainer.ChildIds);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSwitchContainer;
            }
        }

        public static MusicSwitchContainer ParseMusicSwitchContainer2023(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicSwitchContainer = new MusicSwitchContainer(data.Length);
                musicSwitchContainer.Id = reader.ReadUInt32();
                musicSwitchContainer.MidiBehavior = (MusicMidiBehavior) reader.ReadByte();
                musicSwitchContainer.Properties = reader.ReadAudioProperties2023();
                musicSwitchContainer.ChildCount = reader.ReadUInt32();
                musicSwitchContainer.ChildIds = new uint[musicSwitchContainer.ChildCount];
                for (var i = 0; i < musicSwitchContainer.ChildCount; i++)
                {
                    musicSwitchContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GridPeriodTime = reader.ReadDouble();
                musicSwitchContainer.GridOffsetTime = reader.ReadDouble();
                musicSwitchContainer.Tempo = reader.ReadSingle();
                musicSwitchContainer.TimeSignatureUpper = reader.ReadByte();
                musicSwitchContainer.TimeSignatureLower = reader.ReadByte();
                musicSwitchContainer.Unknown_1 = reader.ReadByte();
                musicSwitchContainer.StingerCount = reader.ReadUInt32();
                musicSwitchContainer.Stingers = new MusicStinger[musicSwitchContainer.StingerCount];
                for (var i = 0; i < musicSwitchContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt) reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() == 1;
                    musicSwitchContainer.Stingers[i] = stinger;
                }
                musicSwitchContainer.TransitionCount = reader.ReadUInt32();
                musicSwitchContainer.Transitions = new MusicTransition[musicSwitchContainer.TransitionCount];
                for (var i = 0; i < musicSwitchContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte) reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget) reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicSwitchContainer.Transitions[i] = transition;
                }
                musicSwitchContainer.ContinueOnSwitchChange = reader.ReadBoolean();
                musicSwitchContainer.GroupCount = reader.ReadUInt32();
                musicSwitchContainer.GroupIds = new uint[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupIds[i] = reader.ReadUInt32();
                }
                musicSwitchContainer.GroupTypes = new bool[musicSwitchContainer.GroupCount];
                for (var i = 0; i < musicSwitchContainer.GroupCount; i++)
                {
                    musicSwitchContainer.GroupTypes[i] = reader.ReadBoolean();
                }
                musicSwitchContainer.PathSectionLength = reader.ReadUInt32();
                musicSwitchContainer.UseWeighted = reader.ReadBoolean();
                musicSwitchContainer.Paths = reader.ReadPaths(musicSwitchContainer.PathSectionLength, musicSwitchContainer.ChildIds);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicSwitchContainer;
            }
        }

        public static MusicTrack ParseMusicTrack2013(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicTrack = new MusicTrack(data.Length);
                musicTrack.Id = reader.ReadUInt32();
                musicTrack.SoundCount = reader.ReadUInt32();
                musicTrack.Sounds = new Sound[musicTrack.SoundCount];
                for (var i = 0; i < musicTrack.SoundCount; i++)
                {
                    var sound = new Sound(0);
                    sound.Unknown_04 = reader.ReadByte();
                    sound.Unknown_05 = reader.ReadByte();
                    sound.Conversion = (SoundConversionType)reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    var soundSource = reader.ReadUInt32();
                    if (soundSource == 0)
                    {
                        sound.Source = SoundSource.Embedded;
                    }
                    else if (soundSource == 1)
                    {
                        sound.Source = SoundSource.Streamed;
                    }
                    else if (soundSource == 2)
                    {
                        sound.Source = SoundSource.StreamedZeroLatency;
                    }
                    else
                    {
                        throw new InvalidDataException("Unexpected sound source value.");
                    }
                    sound.AudioId = reader.ReadUInt32();
                    reader.BaseStream.Position += 4;    // embedded SoundBank ID or WEM ID
                    if (sound.Source == SoundSource.Embedded)
                    {
                        reader.BaseStream.Position += 4;    // byteOffset
                        sound.AudioLength = reader.ReadUInt32();
                    }
                    // else zeroLatency: uint unk, uint prefetchLengthBits
                    // else: four bytes (length)? and one byte unk
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType)reader.ReadByte();
                    musicTrack.Sounds[i] = sound;
                }
                musicTrack.TimeParameterCount = reader.ReadUInt32();
                musicTrack.TimeParameters = new MusicTrackTimeParameter[musicTrack.TimeParameterCount];
                for (var i = 0; i < musicTrack.TimeParameterCount; i++)
                {
                    MusicTrackTimeParameter timeParameter = default;
                    timeParameter.SubTrackIndex = reader.ReadUInt32();
                    timeParameter.AudioId = reader.ReadUInt32();
                    timeParameter.BeginOffset = reader.ReadDouble();
                    timeParameter.BeginTrimOffset = reader.ReadDouble();
                    timeParameter.EndTrimOffset = reader.ReadDouble();
                    timeParameter.EndOffset = reader.ReadDouble();
                    musicTrack.TimeParameters[i] = timeParameter;
                }
                musicTrack.SubTrackCount = reader.ReadUInt32();
                if (musicTrack.SoundCount > 0)
                {
                    // FIXME: workaround - when MusicTrack have zero sounds, one of the uints is too much before AudioProperties
                    musicTrack.CurveCount = reader.ReadUInt32();
                }
                musicTrack.Curves = new MusicTrackCurve[musicTrack.CurveCount];
                for (var i = 0; i < musicTrack.CurveCount; i++)
                {
                    MusicTrackCurve curve = default;
                    curve.TimeParameterIndex = reader.ReadUInt32();
                    curve.Type = (MusicFadeCurveType)reader.ReadUInt32();   // TODO: different
                    curve.PointCount = reader.ReadUInt32();
                    curve.Points = new MusicCurvePoint[curve.PointCount];
                    for (var j = 0; j < curve.PointCount; j++)
                    {
                        MusicCurvePoint fadePoint = default;
                        fadePoint.X = reader.ReadSingle();
                        fadePoint.Y = reader.ReadSingle();
                        fadePoint.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        curve.Points[j] = fadePoint;
                    }
                    musicTrack.Curves[i] = curve;
                }
                musicTrack.Properties = reader.ReadAudioProperties2013();
                musicTrack.TrackType = (MusicTrackType)reader.ReadUInt32();
                if (musicTrack.TrackType == MusicTrackType.Switch)
                {
                    // shouldn't happen
                    MusicSwitchParameters switchParameters = default;
                    switchParameters.Unknown = reader.ReadByte();
                    switchParameters.GroupId = reader.ReadUInt32();
                    switchParameters.DefaultSwitchOrStateId = reader.ReadUInt32();
                    switchParameters.SubTrackCount = reader.ReadUInt32();
                    switchParameters.AssociatedSwitchOrStateIds = new uint[switchParameters.SubTrackCount];
                    for (var i = 0; i < switchParameters.SubTrackCount; i++)
                    {
                        switchParameters.AssociatedSwitchOrStateIds[i] = reader.ReadUInt32();
                    }
                    switchParameters.FadeOutDuration = reader.ReadUInt32();
                    switchParameters.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeOutOffset = reader.ReadInt32();
                    switchParameters.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    switchParameters.ExitSourceAtCueId = reader.ReadUInt32();
                    switchParameters.FadeInDuration = reader.ReadUInt32();
                    switchParameters.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeInOffset = reader.ReadInt32();
                    musicTrack.SwitchParameters = switchParameters;
                }
                musicTrack.LookAheadTime = reader.ReadUInt32();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicTrack;
            }
        }

        public static MusicTrack ParseMusicTrack(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicTrack = new MusicTrack(data.Length);
                musicTrack.Id = reader.ReadUInt32();
                musicTrack.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicTrack.SoundCount = reader.ReadUInt32();
                musicTrack.Sounds = new Sound[musicTrack.SoundCount];
                for (var i = 0; i < musicTrack.SoundCount; i++)
                {
                    var sound = new Sound(0);
                    sound.Unknown_04 = reader.ReadByte();
                    sound.Unknown_05 = reader.ReadByte();
                    sound.Conversion = (SoundConversionType)reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    sound.Source = (SoundSource)reader.ReadByte();
                    sound.AudioId = reader.ReadUInt32();
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType)reader.ReadByte();
                    musicTrack.Sounds[i] = sound;
                }
                musicTrack.TimeParameterCount = reader.ReadUInt32();
                musicTrack.TimeParameters = new MusicTrackTimeParameter[musicTrack.TimeParameterCount];
                for (var i = 0; i < musicTrack.TimeParameterCount; i++)
                {
                    MusicTrackTimeParameter timeParameter = default;
                    timeParameter.SubTrackIndex = reader.ReadUInt32();
                    timeParameter.AudioId = reader.ReadUInt32();
                    timeParameter.BeginOffset = reader.ReadDouble();
                    timeParameter.BeginTrimOffset = reader.ReadDouble();
                    timeParameter.EndTrimOffset = reader.ReadDouble();
                    timeParameter.EndOffset = reader.ReadDouble();
                    musicTrack.TimeParameters[i] = timeParameter;
                }
                musicTrack.SubTrackCount = reader.ReadUInt32();
                if (musicTrack.SoundCount > 0)
                {
                    // FIXME: workaround - when MusicTrack have zero sounds, one of the uints is too much before AudioProperties
                    musicTrack.CurveCount = reader.ReadUInt32();
                }
                musicTrack.Curves = new MusicTrackCurve[musicTrack.CurveCount];
                for (var i = 0; i < musicTrack.CurveCount; i++)
                {
                    MusicTrackCurve curve = default;
                    curve.TimeParameterIndex = reader.ReadUInt32();
                    curve.Type = (MusicFadeCurveType)reader.ReadUInt32();
                    curve.PointCount = reader.ReadUInt32();
                    curve.Points = new MusicCurvePoint[curve.PointCount];
                    for (var j = 0; j < curve.PointCount; j++)
                    {
                        MusicCurvePoint fadePoint = default;
                        fadePoint.X = reader.ReadSingle();
                        fadePoint.Y = reader.ReadSingle();
                        fadePoint.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        curve.Points[j] = fadePoint;
                    }
                    musicTrack.Curves[i] = curve;
                }
                musicTrack.Properties = reader.ReadAudioProperties();
                musicTrack.TrackType = (MusicTrackType)reader.ReadByte();
                if (musicTrack.TrackType == MusicTrackType.Switch)
                {
                    MusicSwitchParameters switchParameters = default;
                    switchParameters.Unknown = reader.ReadByte();
                    switchParameters.GroupId = reader.ReadUInt32();
                    switchParameters.DefaultSwitchOrStateId = reader.ReadUInt32();
                    switchParameters.SubTrackCount = reader.ReadUInt32();
                    switchParameters.AssociatedSwitchOrStateIds = new uint[switchParameters.SubTrackCount];
                    for (var i = 0; i < switchParameters.SubTrackCount; i++)
                    {
                        switchParameters.AssociatedSwitchOrStateIds[i] = reader.ReadUInt32();
                    }
                    switchParameters.FadeOutDuration = reader.ReadUInt32();
                    switchParameters.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeOutOffset = reader.ReadInt32();
                    switchParameters.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    switchParameters.ExitSourceAtCueId = reader.ReadUInt32();
                    switchParameters.FadeInDuration = reader.ReadUInt32();
                    switchParameters.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeInOffset = reader.ReadInt32();
                    musicTrack.SwitchParameters = switchParameters;
                }
                musicTrack.LookAheadTime = reader.ReadUInt32();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicTrack;
            }
        }

        public static MusicTrack ParseMusicTrack2019(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicTrack = new MusicTrack(data.Length);
                musicTrack.Id = reader.ReadUInt32();
                musicTrack.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicTrack.SoundCount = reader.ReadUInt32();
                musicTrack.Sounds = new Sound[musicTrack.SoundCount];
                for (var i = 0; i < musicTrack.SoundCount; i++)
                {
                    var sound = new Sound(0);
                    sound.Unknown_04 = reader.ReadByte();
                    sound.Unknown_05 = reader.ReadByte();
                    sound.Conversion = (SoundConversionType)reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    sound.Source = (SoundSource)reader.ReadByte();
                    sound.AudioId = reader.ReadUInt32();
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType)reader.ReadByte();
                    musicTrack.Sounds[i] = sound;
                }
                musicTrack.TimeParameterCount = reader.ReadUInt32();
                musicTrack.TimeParameters = new MusicTrackTimeParameter[musicTrack.TimeParameterCount];
                for (var i = 0; i < musicTrack.TimeParameterCount; i++)
                {
                    MusicTrackTimeParameter timeParameter = default;
                    timeParameter.SubTrackIndex = reader.ReadUInt32();
                    timeParameter.AudioId = reader.ReadUInt32();
                    timeParameter.EventId = reader.ReadUInt32();
                    timeParameter.BeginOffset = reader.ReadDouble();
                    timeParameter.BeginTrimOffset = reader.ReadDouble();
                    timeParameter.EndTrimOffset = reader.ReadDouble();
                    timeParameter.EndOffset = reader.ReadDouble();
                    musicTrack.TimeParameters[i] = timeParameter;
                }
                if (musicTrack.TimeParameterCount > 0)
                {
                    musicTrack.SubTrackCount = reader.ReadUInt32();
                }
                musicTrack.CurveCount = reader.ReadUInt32();
                musicTrack.Curves = new MusicTrackCurve[musicTrack.CurveCount];
                for (var i = 0; i < musicTrack.CurveCount; i++)
                {
                    MusicTrackCurve curve = default;
                    curve.TimeParameterIndex = reader.ReadUInt32();
                    curve.Type = (MusicFadeCurveType)reader.ReadUInt32();
                    curve.PointCount = reader.ReadUInt32();
                    curve.Points = new MusicCurvePoint[curve.PointCount];
                    for (var j = 0; j < curve.PointCount; j++)
                    {
                        MusicCurvePoint fadePoint = default;
                        fadePoint.X = reader.ReadSingle();
                        fadePoint.Y = reader.ReadSingle();
                        fadePoint.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        curve.Points[j] = fadePoint;
                    }
                    musicTrack.Curves[i] = curve;
                }
                musicTrack.Properties = reader.ReadAudioProperties2019();
                musicTrack.TrackType = (MusicTrackType)reader.ReadByte();
                if (musicTrack.TrackType == MusicTrackType.Switch)
                {
                    MusicSwitchParameters switchParameters = default;
                    switchParameters.Unknown = reader.ReadByte();
                    switchParameters.GroupId = reader.ReadUInt32();
                    switchParameters.DefaultSwitchOrStateId = reader.ReadUInt32();
                    switchParameters.SubTrackCount = reader.ReadUInt32();
                    switchParameters.AssociatedSwitchOrStateIds = new uint[switchParameters.SubTrackCount];
                    for (var i = 0; i < switchParameters.SubTrackCount; i++)
                    {
                        switchParameters.AssociatedSwitchOrStateIds[i] = reader.ReadUInt32();
                    }
                    switchParameters.FadeOutDuration = reader.ReadUInt32();
                    switchParameters.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeOutOffset = reader.ReadInt32();
                    switchParameters.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    switchParameters.ExitSourceAtCueId = reader.ReadUInt32();
                    switchParameters.FadeInDuration = reader.ReadUInt32();
                    switchParameters.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeInOffset = reader.ReadInt32();
                    musicTrack.SwitchParameters = switchParameters;
                }
                musicTrack.LookAheadTime = reader.ReadUInt32();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicTrack;
            }
        }

        public static MusicTrack ParseMusicTrack2021(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicTrack = new MusicTrack(data.Length);
                musicTrack.Id = reader.ReadUInt32();
                musicTrack.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicTrack.SoundCount = reader.ReadUInt32();
                musicTrack.Sounds = new Sound[musicTrack.SoundCount];
                for (var i = 0; i < musicTrack.SoundCount; i++)
                {
                    var sound = new Sound(0);
                    sound.Unknown_04 = reader.ReadByte();
                    sound.Unknown_05 = reader.ReadByte();
                    sound.Conversion = (SoundConversionType)reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    sound.Source = (SoundSource)reader.ReadByte();
                    sound.AudioId = reader.ReadUInt32();
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType)reader.ReadByte();
                    musicTrack.Sounds[i] = sound;
                }
                musicTrack.TimeParameterCount = reader.ReadUInt32();
                musicTrack.TimeParameters = new MusicTrackTimeParameter[musicTrack.TimeParameterCount];
                for (var i = 0; i < musicTrack.TimeParameterCount; i++)
                {
                    MusicTrackTimeParameter timeParameter = default;
                    timeParameter.SubTrackIndex = reader.ReadUInt32();
                    timeParameter.AudioId = reader.ReadUInt32();
                    timeParameter.EventId = reader.ReadUInt32();
                    timeParameter.BeginOffset = reader.ReadDouble();
                    timeParameter.BeginTrimOffset = reader.ReadDouble();
                    timeParameter.EndTrimOffset = reader.ReadDouble();
                    timeParameter.EndOffset = reader.ReadDouble();
                    musicTrack.TimeParameters[i] = timeParameter;
                }
                if (musicTrack.TimeParameterCount > 0)
                {
                    musicTrack.SubTrackCount = reader.ReadUInt32();
                }
                musicTrack.CurveCount = reader.ReadUInt32();
                musicTrack.Curves = new MusicTrackCurve[musicTrack.CurveCount];
                for (var i = 0; i < musicTrack.CurveCount; i++)
                {
                    MusicTrackCurve curve = default;
                    curve.TimeParameterIndex = reader.ReadUInt32();
                    curve.Type = (MusicFadeCurveType)reader.ReadUInt32();
                    curve.PointCount = reader.ReadUInt32();
                    curve.Points = new MusicCurvePoint[curve.PointCount];
                    for (var j = 0; j < curve.PointCount; j++)
                    {
                        MusicCurvePoint fadePoint = default;
                        fadePoint.X = reader.ReadSingle();
                        fadePoint.Y = reader.ReadSingle();
                        fadePoint.FollowingCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        curve.Points[j] = fadePoint;
                    }
                    musicTrack.Curves[i] = curve;
                }
                musicTrack.Properties = reader.ReadAudioProperties2021();
                musicTrack.TrackType = (MusicTrackType)reader.ReadByte();
                if (musicTrack.TrackType == MusicTrackType.Switch)
                {
                    MusicSwitchParameters switchParameters = default;
                    switchParameters.Unknown = reader.ReadByte();
                    switchParameters.GroupId = reader.ReadUInt32();
                    switchParameters.DefaultSwitchOrStateId = reader.ReadUInt32();
                    switchParameters.SubTrackCount = reader.ReadUInt32();
                    switchParameters.AssociatedSwitchOrStateIds = new uint[switchParameters.SubTrackCount];
                    for (var i = 0; i < switchParameters.SubTrackCount; i++)
                    {
                        switchParameters.AssociatedSwitchOrStateIds[i] = reader.ReadUInt32();
                    }
                    switchParameters.FadeOutDuration = reader.ReadUInt32();
                    switchParameters.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeOutOffset = reader.ReadInt32();
                    switchParameters.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    switchParameters.ExitSourceAtCueId = reader.ReadUInt32();
                    switchParameters.FadeInDuration = reader.ReadUInt32();
                    switchParameters.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    switchParameters.FadeInOffset = reader.ReadInt32();
                    musicTrack.SwitchParameters = switchParameters;
                }
                musicTrack.LookAheadTime = reader.ReadUInt32();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicTrack;
            }
        }

        public static MusicTrack ParseMusicTrack2023(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicTrack = new MusicTrack(data.Length);
                musicTrack.Id = reader.ReadUInt32();
                musicTrack.MidiBehavior = (MusicMidiBehavior) reader.ReadByte();
                musicTrack.SoundCount = reader.ReadUInt32();
                musicTrack.Sounds = new Sound[musicTrack.SoundCount];
                for (var i = 0; i < musicTrack.SoundCount; i++)
                {
                    var sound = new Sound(0);
                    sound.Unknown_04 = reader.ReadByte();
                    sound.Unknown_05 = reader.ReadByte();
                    sound.Conversion = (SoundConversionType) reader.ReadByte();
                    sound.Unknown_07 = reader.ReadByte();
                    sound.Source = (SoundSource) reader.ReadByte();
                    sound.AudioId = reader.ReadUInt32();
                    sound.AudioLength = reader.ReadUInt32();
                    sound.AudioType = (SoundType) reader.ReadByte();
                    musicTrack.Sounds[i] = sound;
                }
                musicTrack.TimeParameterCount = reader.ReadUInt32();
                musicTrack.TimeParameters = new MusicTrackTimeParameter[musicTrack.TimeParameterCount];
                for (var i = 0; i < musicTrack.TimeParameterCount; i++)
                {
                    MusicTrackTimeParameter timeParameter = default;
                    timeParameter.SubTrackIndex = reader.ReadUInt32();
                    timeParameter.AudioId = reader.ReadUInt32();
                    timeParameter.EventId = reader.ReadUInt32();
                    timeParameter.BeginOffset = reader.ReadDouble();
                    timeParameter.BeginTrimOffset = reader.ReadDouble();
                    timeParameter.EndTrimOffset = reader.ReadDouble();
                    timeParameter.EndOffset = reader.ReadDouble();
                    musicTrack.TimeParameters[i] = timeParameter;
                }
                if (musicTrack.TimeParameterCount > 0)
                {
                    musicTrack.SubTrackCount = reader.ReadUInt32();
                }
                musicTrack.CurveCount = reader.ReadUInt32();
                musicTrack.Curves = new MusicTrackCurve[musicTrack.CurveCount];
                for (var i = 0; i < musicTrack.CurveCount; i++)
                {
                    MusicTrackCurve curve = default;
                    curve.TimeParameterIndex = reader.ReadUInt32();
                    curve.Type = (MusicFadeCurveType) reader.ReadUInt32();
                    curve.PointCount = reader.ReadUInt32();
                    curve.Points = new MusicCurvePoint[curve.PointCount];
                    for (var j = 0; j < curve.PointCount; j++)
                    {
                        MusicCurvePoint fadePoint = default;
                        fadePoint.X = reader.ReadSingle();
                        fadePoint.Y = reader.ReadSingle();
                        fadePoint.FollowingCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                        curve.Points[j] = fadePoint;
                    }
                    musicTrack.Curves[i] = curve;
                }
                musicTrack.Properties = reader.ReadAudioProperties2023();
                musicTrack.TrackType = (MusicTrackType) reader.ReadByte();
                if (musicTrack.TrackType == MusicTrackType.Switch)
                {
                    MusicSwitchParameters switchParameters = default;
                    switchParameters.Unknown = reader.ReadByte();
                    switchParameters.GroupId = reader.ReadUInt32();
                    switchParameters.DefaultSwitchOrStateId = reader.ReadUInt32();
                    switchParameters.SubTrackCount = reader.ReadUInt32();
                    switchParameters.AssociatedSwitchOrStateIds = new uint[switchParameters.SubTrackCount];
                    for (var i = 0; i < switchParameters.SubTrackCount; i++)
                    {
                        switchParameters.AssociatedSwitchOrStateIds[i] = reader.ReadUInt32();
                    }
                    switchParameters.FadeOutDuration = reader.ReadUInt32();
                    switchParameters.FadeOutCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    switchParameters.FadeOutOffset = reader.ReadInt32();
                    switchParameters.ExitSourceAt = (MusicKeyPointByte) reader.ReadUInt32();
                    switchParameters.ExitSourceAtCueId = reader.ReadUInt32();
                    switchParameters.FadeInDuration = reader.ReadUInt32();
                    switchParameters.FadeInCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    switchParameters.FadeInOffset = reader.ReadInt32();
                    musicTrack.SwitchParameters = switchParameters;
                }
                musicTrack.LookAheadTime = reader.ReadUInt32();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicTrack;
            }
        }

        public static Event ParseEvent(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var @event = new Event(data.Length);
                @event.Id = reader.ReadUInt32();
                @event.ActionCount = reader.ReadUInt32();
                @event.ActionIds = new uint[@event.ActionCount];
                for (var i = 0; i < @event.ActionCount; i++)
                {
                    @event.ActionIds[i] = reader.ReadUInt32();
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return @event;
            }
        }

        public static Event ParseEvent2019(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var @event = new Event(data.Length);
                @event.Id = reader.ReadUInt32();
                @event.ActionCount = reader.ReadByte();
                @event.ActionIds = new uint[@event.ActionCount];
                for (var i = 0; i < @event.ActionCount; i++)
                {
                    @event.ActionIds[i] = reader.ReadUInt32();
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return @event;
            }
        }

        public static EventAction ParseEventAction(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var eventAction = new EventAction(data.Length);
                eventAction.Id = reader.ReadUInt32();
                eventAction.Scope = (EventActionScope)reader.ReadByte();
                eventAction.ActionType = (EventActionType)reader.ReadByte();
                eventAction.ObjectId = reader.ReadUInt32();
                eventAction.ObjectId2 = reader.ReadByte();
                eventAction.ParameterCount = reader.ReadByte();
                eventAction.ParameterTypes = new AudioParameterType[eventAction.ParameterCount];
                for (var i = 0; i < eventAction.ParameterCount; i++)
                {
                    eventAction.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
                }
                eventAction.ParameterValues = new int[eventAction.ParameterCount];
                for (var i = 0; i < eventAction.ParameterCount; i++)
                {
                    eventAction.ParameterValues[i] = reader.ReadInt32();
                }
                eventAction.ParameterPairCount = reader.ReadByte();
                eventAction.ParameterPairTypes = new byte[eventAction.ParameterPairCount];
                for (var i = 0; i < eventAction.ParameterPairCount; i++)
                {
                    eventAction.ParameterPairTypes[i] = reader.ReadByte();
                }
                eventAction.ParameterPairValues = new AudioParameterPair[eventAction.ParameterPairCount];
                for (var i = 0; i < eventAction.ParameterPairCount; i++)
                {
                    AudioParameterPair audioParameterPair = default;
                    audioParameterPair.Parameter_1 = reader.ReadSingle();
                    audioParameterPair.Parameter_2 = reader.ReadSingle();
                    eventAction.ParameterPairValues[i] = audioParameterPair;
                }


                // Read custom settings
                if (eventAction.ActionType.HasSettings())
                {
                    EventActionSettings settings = null;
                    switch (eventAction.ActionType)
                    {
                        case EventActionType.Stop:
                            var stop = new EventActionStopSettings();
                            stop.FadeOutCurve = (AudioCurveShapeByte) reader.ReadByte();
                            stop.Flag = reader.ReadByte();
                            reader.ReadEventActionExceptions(stop);
                            settings = stop;
                            break;
                        case EventActionType.Pause:
                            var pause = new EventActionPauseSettings();
                            pause.FadeOutCurve = (AudioCurveShapeByte) reader.ReadByte();
                            pause.Flag = reader.ReadByte();
                            reader.ReadEventActionExceptions(pause);
                            settings = pause;
                            break;
                        case EventActionType.Resume:
                            var resume = new EventActionResumeSettings();
                            resume.FadeInCurve = (AudioCurveShapeByte) reader.ReadByte();
                            resume.Flag = reader.ReadByte();
                            reader.ReadEventActionExceptions(resume);
                            settings = resume;
                            break;
                        case EventActionType.Play:
                            var play = new EventActionPlaySettings();
                            play.FadeInCurve = (AudioCurveShapeByte) reader.ReadByte();
                            play.TargetSoundBankId = reader.ReadUInt32();
                            settings = play;
                            break;
                        case EventActionType.Seek:
                            var seek = new EventActionSeekSettings();
                            seek.SeekType = (EventActionSeekType) reader.ReadByte();
                            seek.Seek = reader.ReadSingle();
                            seek.Unknown_0 = reader.ReadUInt32();
                            seek.Unknown_1 = reader.ReadUInt32();
                            seek.SeekToNearestMarker = reader.ReadBoolean();
                            settings = seek;
                            break;
                    }
                    eventAction.Settings = settings;
                }

                // FIXME: we don't fully understand Play EventAction, will trip the assert
                //Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return eventAction;
            }
        }

        public static Settings ParseSettings(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var settings = new Settings(data.Length);
                settings.Id = reader.ReadUInt32();
                settings.ParameterCount = reader.ReadByte();
                settings.ParameterTypes = new AudioParameterType[settings.ParameterCount];
                for (var i = 0; i < settings.ParameterCount; i++)
                {
                    settings.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
                }
                settings.ParameterValues = new float[settings.ParameterCount];
                for (var i = 0; i < settings.ParameterCount; i++)
                {
                    settings.ParameterValues[i] = reader.ReadSingle();
                }

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return settings;
            }
        }

        public static Unknown ParseUnknown(byte type, byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var unknown = new Unknown((HIRCObjectType)type, data.Length);
                unknown.Id = reader.ReadUInt32();
                unknown.Blob = reader.ReadBytes(data.Length - 4);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return unknown;
            }
        }

        public static MusicPlaylistContainer ParseMusicPlaylistContainer2013(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicPlaylistContainer = new MusicPlaylistContainer(data.Length);
                musicPlaylistContainer.Id = reader.ReadUInt32();
                musicPlaylistContainer.Properties = reader.ReadAudioProperties2013();
                musicPlaylistContainer.ChildCount = reader.ReadUInt32();
                musicPlaylistContainer.ChildIds = new uint[musicPlaylistContainer.ChildCount];
                for (var i = 0; i < musicPlaylistContainer.ChildCount; i++)
                {
                    musicPlaylistContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicPlaylistContainer.GridPeriodTime = reader.ReadDouble();
                musicPlaylistContainer.GridOffsetTime = reader.ReadDouble();
                musicPlaylistContainer.Tempo = reader.ReadSingle();
                musicPlaylistContainer.TimeSignatureUpper = reader.ReadByte();
                musicPlaylistContainer.TimeSignatureLower = reader.ReadByte();
                musicPlaylistContainer.Unknown_1 = reader.ReadByte();
                musicPlaylistContainer.StingerCount = reader.ReadUInt32();
                musicPlaylistContainer.Stingers = new MusicStinger[musicPlaylistContainer.StingerCount];
                for (var i = 0; i < musicPlaylistContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadBoolean();
                    musicPlaylistContainer.Stingers[i] = stinger;
                }
                musicPlaylistContainer.TransitionCount = reader.ReadUInt32();
                musicPlaylistContainer.Transitions = new MusicTransition[musicPlaylistContainer.TransitionCount];
                for (var i = 0; i < musicPlaylistContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadByte() == 0xFF;
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt16();
                    transition.PlayPreEntry = reader.ReadByte() == 0xFF;
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicPlaylistContainer.Transitions[i] = transition;
                }
                musicPlaylistContainer.PlaylistElementCount = reader.ReadUInt32();
                musicPlaylistContainer.Playlist = reader.ReadPlaylist2013();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicPlaylistContainer;
            }
        }

        public static MusicPlaylistContainer ParseMusicPlaylistContainer(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicPlaylistContainer = new MusicPlaylistContainer(data.Length);
                musicPlaylistContainer.Id = reader.ReadUInt32();
                musicPlaylistContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicPlaylistContainer.Properties = reader.ReadAudioProperties();
                musicPlaylistContainer.ChildCount = reader.ReadUInt32();
                musicPlaylistContainer.ChildIds = new uint[musicPlaylistContainer.ChildCount];
                for (var i = 0; i < musicPlaylistContainer.ChildCount; i++)
                {
                    musicPlaylistContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicPlaylistContainer.GridPeriodTime = reader.ReadDouble();
                musicPlaylistContainer.GridOffsetTime = reader.ReadDouble();
                musicPlaylistContainer.Tempo = reader.ReadSingle();
                musicPlaylistContainer.TimeSignatureUpper = reader.ReadByte();
                musicPlaylistContainer.TimeSignatureLower = reader.ReadByte();
                musicPlaylistContainer.Unknown_1 = reader.ReadByte();
                musicPlaylistContainer.StingerCount = reader.ReadUInt32();
                musicPlaylistContainer.Stingers = new MusicStinger[musicPlaylistContainer.StingerCount];
                for (var i = 0; i < musicPlaylistContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadBoolean();
                    musicPlaylistContainer.Stingers[i] = stinger;
                }
                musicPlaylistContainer.TransitionCount = reader.ReadUInt32();
                musicPlaylistContainer.Transitions = new MusicTransition[musicPlaylistContainer.TransitionCount];
                for (var i = 0; i < musicPlaylistContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadByte() == 0xFF;
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt16();
                    transition.PlayPreEntry = reader.ReadByte() == 0xFF;
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadByte() == 0xFF;
                        transition.PlayTransitionPostExit = reader.ReadByte() == 0xFF;
                    }
                    musicPlaylistContainer.Transitions[i] = transition;
                }
                musicPlaylistContainer.PlaylistElementCount = reader.ReadUInt32();
                musicPlaylistContainer.Playlist = reader.ReadPlaylist();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicPlaylistContainer;
            }
        }
        
        public static MusicPlaylistContainer ParseMusicPlaylistContainer2019(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicPlaylistContainer = new MusicPlaylistContainer(data.Length);
                musicPlaylistContainer.Id = reader.ReadUInt32();
                musicPlaylistContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicPlaylistContainer.Properties = reader.ReadAudioProperties2019();
                musicPlaylistContainer.ChildCount = reader.ReadUInt32();
                musicPlaylistContainer.ChildIds = new uint[musicPlaylistContainer.ChildCount];
                for (var i = 0; i < musicPlaylistContainer.ChildCount; i++)
                {
                    musicPlaylistContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicPlaylistContainer.GridPeriodTime = reader.ReadDouble();
                musicPlaylistContainer.GridOffsetTime = reader.ReadDouble();
                musicPlaylistContainer.Tempo = reader.ReadSingle();
                musicPlaylistContainer.TimeSignatureUpper = reader.ReadByte();
                musicPlaylistContainer.TimeSignatureLower = reader.ReadByte();
                musicPlaylistContainer.Unknown_1 = reader.ReadByte();
                musicPlaylistContainer.StingerCount = reader.ReadUInt32();
                musicPlaylistContainer.Stingers = new MusicStinger[musicPlaylistContainer.StingerCount];
                for (var i = 0; i < musicPlaylistContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadBoolean();
                    musicPlaylistContainer.Stingers[i] = stinger;
                }
                musicPlaylistContainer.TransitionCount = reader.ReadUInt32();
                musicPlaylistContainer.Transitions = new MusicTransition[musicPlaylistContainer.TransitionCount];
                for (var i = 0; i < musicPlaylistContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    // eJumpToType, eEntryType
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadBoolean();
                        transition.PlayTransitionPostExit = reader.ReadBoolean();
                    }
                    musicPlaylistContainer.Transitions[i] = transition;
                }
                musicPlaylistContainer.PlaylistElementCount = reader.ReadUInt32();
                musicPlaylistContainer.Playlist = reader.ReadPlaylist();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicPlaylistContainer;
            }
        }

        public static MusicPlaylistContainer ParseMusicPlaylistContainer2021(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicPlaylistContainer = new MusicPlaylistContainer(data.Length);
                musicPlaylistContainer.Id = reader.ReadUInt32();
                musicPlaylistContainer.MidiBehavior = (MusicMidiBehavior)reader.ReadByte();
                musicPlaylistContainer.Properties = reader.ReadAudioProperties2021();
                musicPlaylistContainer.ChildCount = reader.ReadUInt32();
                musicPlaylistContainer.ChildIds = new uint[musicPlaylistContainer.ChildCount];
                for (var i = 0; i < musicPlaylistContainer.ChildCount; i++)
                {
                    musicPlaylistContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicPlaylistContainer.GridPeriodTime = reader.ReadDouble();
                musicPlaylistContainer.GridOffsetTime = reader.ReadDouble();
                musicPlaylistContainer.Tempo = reader.ReadSingle();
                musicPlaylistContainer.TimeSignatureUpper = reader.ReadByte();
                musicPlaylistContainer.TimeSignatureLower = reader.ReadByte();
                musicPlaylistContainer.Unknown_1 = reader.ReadByte();
                musicPlaylistContainer.StingerCount = reader.ReadUInt32();
                musicPlaylistContainer.Stingers = new MusicStinger[musicPlaylistContainer.StingerCount];
                for (var i = 0; i < musicPlaylistContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt)reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() > 0;
                    musicPlaylistContainer.Stingers[i] = stinger;
                }
                musicPlaylistContainer.TransitionCount = reader.ReadUInt32();
                musicPlaylistContainer.Transitions = new MusicTransition[musicPlaylistContainer.TransitionCount];
                for (var i = 0; i < musicPlaylistContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte)reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    // eJumpToType, eEntryType
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget)reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt)reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadBoolean();
                        transition.PlayTransitionPostExit = reader.ReadBoolean();
                    }
                    musicPlaylistContainer.Transitions[i] = transition;
                }
                musicPlaylistContainer.PlaylistElementCount = reader.ReadUInt32();
                musicPlaylistContainer.Playlist = reader.ReadPlaylist();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicPlaylistContainer;
            }
        }

        public static MusicPlaylistContainer ParseMusicPlaylistContainer2023(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var musicPlaylistContainer = new MusicPlaylistContainer(data.Length);
                musicPlaylistContainer.Id = reader.ReadUInt32();
                musicPlaylistContainer.MidiBehavior = (MusicMidiBehavior) reader.ReadByte();
                musicPlaylistContainer.Properties = reader.ReadAudioProperties2023();
                musicPlaylistContainer.ChildCount = reader.ReadUInt32();
                musicPlaylistContainer.ChildIds = new uint[musicPlaylistContainer.ChildCount];
                for (var i = 0; i < musicPlaylistContainer.ChildCount; i++)
                {
                    musicPlaylistContainer.ChildIds[i] = reader.ReadUInt32();
                }
                musicPlaylistContainer.GridPeriodTime = reader.ReadDouble();
                musicPlaylistContainer.GridOffsetTime = reader.ReadDouble();
                musicPlaylistContainer.Tempo = reader.ReadSingle();
                musicPlaylistContainer.TimeSignatureUpper = reader.ReadByte();
                musicPlaylistContainer.TimeSignatureLower = reader.ReadByte();
                musicPlaylistContainer.Unknown_1 = reader.ReadByte();
                musicPlaylistContainer.StingerCount = reader.ReadUInt32();
                musicPlaylistContainer.Stingers = new MusicStinger[musicPlaylistContainer.StingerCount];
                for (var i = 0; i < musicPlaylistContainer.StingerCount; i++)
                {
                    MusicStinger stinger = default;
                    stinger.TriggerId = reader.ReadUInt32();
                    stinger.SegmentId = reader.ReadUInt32();
                    stinger.PlayAt = (MusicKeyPointUInt) reader.ReadUInt32();
                    stinger.CueId = reader.ReadUInt32();
                    stinger.DoNotRepeatIn = reader.ReadUInt32();
                    stinger.AllowPlayingInNextSegment = reader.ReadUInt32() > 0;
                    musicPlaylistContainer.Stingers[i] = stinger;
                }
                musicPlaylistContainer.TransitionCount = reader.ReadUInt32();
                musicPlaylistContainer.Transitions = new MusicTransition[musicPlaylistContainer.TransitionCount];
                for (var i = 0; i < musicPlaylistContainer.TransitionCount; i++)
                {
                    MusicTransition transition = default;
                    transition.SourceIdCount = reader.ReadUInt32();
                    transition.SourceIds = new uint[transition.SourceIdCount];
                    for (var j = 0; j < transition.SourceIdCount; j++)
                    {
                        transition.SourceIds[j] = reader.ReadUInt32();
                    }
                    transition.DestinationIdCount = reader.ReadUInt32();
                    transition.DestinationIds = new uint[transition.DestinationIdCount];
                    for (var j = 0; j < transition.DestinationIdCount; j++)
                    {
                        transition.DestinationIds[j] = reader.ReadUInt32();
                    }
                    transition.FadeOutDuration = reader.ReadUInt32();
                    transition.FadeOutCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    transition.FadeOutOffset = reader.ReadInt32();
                    transition.ExitSourceAt = (MusicKeyPointByte) reader.ReadUInt32();
                    transition.ExitSourceAtCueId = reader.ReadUInt32();
                    transition.PlayPostExit = reader.ReadBoolean();
                    transition.FadeInDuration = reader.ReadUInt32();
                    transition.FadeInCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                    transition.FadeInOffset = reader.ReadInt32();
                    transition.CustomCueFilterId = reader.ReadUInt32();
                    transition.JumpToPlaylistItemId = reader.ReadUInt32();
                    // eJumpToType, eEntryType
                    transition.DestinationSyncTo = (MusicTransitionSyncTarget) reader.ReadUInt32();
                    transition.PlayPreEntry = reader.ReadBoolean();
                    transition.MatchSourceCueName = reader.ReadBoolean();
                    transition.UseTransitionSegment = reader.ReadBoolean();
                    if (transition.UseTransitionSegment)
                    {
                        transition.TransitionSegmentId = reader.ReadUInt32();
                        transition.TransitionFadeInDuration = reader.ReadUInt32();
                        transition.TransitionFadeInCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                        transition.TransitionFadeInOffset = reader.ReadInt32();
                        transition.TransitionFadeOutDuration = reader.ReadUInt32();
                        transition.TransitionFadeOutCurveShape = (AudioCurveShapeUInt) reader.ReadUInt32();
                        transition.TransitionFadeOutOffset = reader.ReadInt32();
                        transition.PlayTransitionPreEntry = reader.ReadBoolean();
                        transition.PlayTransitionPostExit = reader.ReadBoolean();
                    }
                    musicPlaylistContainer.Transitions[i] = transition;
                }
                musicPlaylistContainer.PlaylistElementCount = reader.ReadUInt32();
                musicPlaylistContainer.Playlist = reader.ReadPlaylist();

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return musicPlaylistContainer;
            }
        }

        public static DialogueEvent ParseDialogueEvent(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                var dialogueEvent = new DialogueEvent(data.Length);
                dialogueEvent.Id = reader.ReadUInt32();
                dialogueEvent.Probability = reader.ReadByte();
                dialogueEvent.ArgumentCount = reader.ReadUInt32();
                dialogueEvent.ArgumentIds = new uint[dialogueEvent.ArgumentCount];
                for (var i = 0; i < dialogueEvent.ArgumentCount; i++)
                {
                    dialogueEvent.ArgumentIds[i] = reader.ReadUInt32();
                }
                dialogueEvent.Unknown_1 = reader.ReadByte();
                dialogueEvent.Unknown_2 = reader.ReadByte();
                dialogueEvent.Unknown_3 = reader.ReadByte();
                dialogueEvent.PathSectionLength = reader.ReadUInt32();
                dialogueEvent.UseWeighted = reader.ReadBoolean();
                dialogueEvent.Paths = reader.ReadPaths(dialogueEvent.PathSectionLength, null);

                Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Length);
                return dialogueEvent;
            }
        }

        private static AudioProperties ReadAudioProperties2013(this BinaryReader reader)
        {
            var audioProperties = new AudioProperties();
            audioProperties.OverrideEffects = reader.ReadBoolean();
            audioProperties.EffectCount = reader.ReadByte();
            if (audioProperties.EffectCount > 0)
            {
                audioProperties.BypassedEffects = (AudioBypassedEffects)reader.ReadByte();
            }
            audioProperties.Effects = new AudioEffect[audioProperties.EffectCount];
            for (var i = 0; i < audioProperties.EffectCount; i++)
            {
                AudioEffect effect = default;
                effect.Index = reader.ReadByte();
                effect.Id = reader.ReadUInt32();
                effect.ShouldUseShareSets = reader.ReadBoolean();
                effect.IsRendered = reader.ReadBoolean();
                audioProperties.Effects[i] = effect;
            }
            audioProperties.OutputBusId = reader.ReadUInt32();
            audioProperties.ParentId = reader.ReadUInt32();
            var overridePlaybackPriority = reader.ReadBoolean() ? AudioPlaybackBehavior.OverridePlaybackPriority : 0;
            var offsetPriority = reader.ReadBoolean() ? AudioPlaybackBehavior.OffsetPlaybackPriority : 0;
            audioProperties.PlaybackBehavior = overridePlaybackPriority | offsetPriority;
            audioProperties.ParameterCount = reader.ReadByte();
            audioProperties.ParameterTypes = new AudioParameterType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                audioProperties.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
            }
            audioProperties.ParameterValues = new ValueType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var parameterType = audioProperties.ParameterTypes[i].ToString();
                if (parameterType.EndsWith("_UInt"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadUInt32();
                }
                else if (parameterType.EndsWith("_Int"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadInt32();
                }
                else
                {
                    audioProperties.ParameterValues[i] = reader.ReadSingle();
                }
            }
            audioProperties.ParameterPairCount = reader.ReadByte();
            audioProperties.ParameterPairTypes = new byte[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                audioProperties.ParameterPairTypes[i] = reader.ReadByte();
            }
            audioProperties.ParameterPairValues = new AudioParameterPair[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                AudioParameterPair audioParameterPair = default;
                audioParameterPair.Parameter_1 = reader.ReadSingle();
                audioParameterPair.Parameter_2 = reader.ReadSingle();
                audioProperties.ParameterPairValues[i] = audioParameterPair;
            }

            // Read Positioning settings: merge booleans into a single flag
            AudioPositioningBehavior overridePositioning, twoDimensional, enablePanner, enableSpatialization, updateAtEachFrame, userDefinedShouldLoop, ignoreListenerOrientation;
            twoDimensional = enablePanner = enableSpatialization = updateAtEachFrame = userDefinedShouldLoop = ignoreListenerOrientation = 0;
            overridePositioning = reader.ReadBoolean() ? AudioPositioningBehavior.OverrideParent : 0;
            if (overridePositioning > 0)
            {
                twoDimensional = reader.ReadBoolean() ? AudioPositioningBehavior.TwoDimensional : 0;
                if (twoDimensional > 0)
                {
                    reader.BaseStream.Position++;
                    enablePanner = reader.ReadBoolean() ? AudioPositioningBehavior.Enable2dPanner : 0;
                }
                else
                {
                    reader.BaseStream.Position++;
                    audioProperties.IsGameDefined = reader.ReadUInt32() == 1;
                    audioProperties.AttenuationId = reader.ReadUInt32();
                    enableSpatialization = reader.ReadBoolean() ? AudioPositioningBehavior.EnableSpatialization : 0;
                    if (audioProperties.IsGameDefined)
                    {
                        updateAtEachFrame = reader.ReadBoolean() ? AudioPositioningBehavior.UpdateAtEachFrame : 0;
                    }
                    else
                    {
                        audioProperties.UserDefinedPlaySettings = (AudioUserDefinedPositioningBehavior)reader.ReadByte();
                        reader.BaseStream.Position += 3;
                        userDefinedShouldLoop = reader.ReadBoolean() ? AudioPositioningBehavior.UserDefinedShouldLoop : 0;
                        audioProperties.TransitionTime = reader.ReadUInt32();
                        ignoreListenerOrientation = reader.ReadBoolean() ? 0 : AudioPositioningBehavior.IgnoreListenerOrientation;
                        audioProperties.ControlPointKeyCount = reader.ReadUInt32();
                        audioProperties.ControlPointKeys = new AudioControlPointKey[audioProperties.ControlPointKeyCount];
                        for (var i = 0; i < audioProperties.ControlPointKeyCount; i++)
                        {
                            AudioControlPointKey controlPointKey = default;
                            controlPointKey.X = reader.ReadSingle();
                            controlPointKey.Z = reader.ReadSingle();
                            controlPointKey.Y = reader.ReadSingle();
                            controlPointKey.Timestamp = reader.ReadUInt32();
                            audioProperties.ControlPointKeys[i] = controlPointKey;
                        }
                        audioProperties.RandomRangeCount = reader.ReadUInt32();
                        audioProperties.RandomRanges = new AudioPathRandomRange[audioProperties.RandomRangeCount];
                        for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                        {
                            AudioPathRandomRange randomRange = default;
                            randomRange.LeftRight = reader.ReadSingle();
                            randomRange.UpDown = reader.ReadSingle();
                            audioProperties.RandomRanges[i] = randomRange;
                        }
                    }
                }
            }
            audioProperties.Positioning |= 
                overridePositioning
                | twoDimensional
                | enablePanner
                | enableSpatialization
                | updateAtEachFrame
                | userDefinedShouldLoop
                | ignoreListenerOrientation;

            // Read Aux Sends settings: merge booleans into a single flag
            AudioAuxSendsBehavior overrideGameDefined, useGameDefinedAuxSends, overrideUserDefined, overrideAuxSends;
            overrideGameDefined = reader.ReadBoolean() ? AudioAuxSendsBehavior.OverrideGameDefined : 0;
            useGameDefinedAuxSends = reader.ReadBoolean() ? AudioAuxSendsBehavior.UseGameDefinedAuxSends : 0;
            overrideUserDefined = reader.ReadBoolean() ? AudioAuxSendsBehavior.OverrideUserDefined : 0;
            overrideAuxSends = reader.ReadBoolean() ? AudioAuxSendsBehavior.OverrideAuxSends : 0;
            audioProperties.AuxSendsBehavior |=
                overrideGameDefined
                | useGameDefinedAuxSends
                | overrideUserDefined
                | overrideAuxSends;

            if (audioProperties.AuxSendsBehavior.HasFlag(AudioAuxSendsBehavior.OverrideAuxSends))
            {
                audioProperties.AuxiliarySendBusIds = new uint[4];
                audioProperties.AuxiliarySendBusIds[0] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[1] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[2] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[3] = reader.ReadUInt32();
            }

            reader.BaseStream.Position++;   // 1
            AudioLimitBehavior discardNewestInstance, useVirtual, limitGlobally, overrideParentPlaybackLimit, overrideParentVirtualVoice;
            discardNewestInstance = reader.ReadBoolean() ? AudioLimitBehavior.DiscardNewest : 0;
            useVirtual = reader.ReadBoolean() ? AudioLimitBehavior.UseVirtual : 0;
            var soundInstanceLimit = reader.ReadUInt16();
            limitGlobally = reader.ReadBoolean() ? AudioLimitBehavior.LimitGlobally : 0;
            audioProperties.VirtualVoiceBehavior = (AudioVirtualVoiceBehavior)reader.ReadByte();
            overrideParentPlaybackLimit = reader.ReadBoolean() ? AudioLimitBehavior.OverrideParentPlaybackLimit : 0;
            overrideParentVirtualVoice = reader.ReadBoolean() ? AudioLimitBehavior.OverrideParentVirtualVoice : 0;
            audioProperties.LimitBehavior |=
                discardNewestInstance
                | useVirtual
                | limitGlobally
                | overrideParentPlaybackLimit
                | overrideParentVirtualVoice;

            AudioHdrSettings overrideEnvelopeTracking, overrideLoudnessNormalization, enableLoudnessNormalization, enableEnvelope;
            overrideEnvelopeTracking = reader.ReadBoolean() ? AudioHdrSettings.OverrideEnvelopeTracking : 0;
            overrideLoudnessNormalization = reader.ReadBoolean() ? AudioHdrSettings.EnableLoudnessNormalization : 0;
            enableLoudnessNormalization = reader.ReadBoolean() ? AudioHdrSettings.EnableLoudnessNormalization : 0;
            enableEnvelope = reader.ReadBoolean() ? AudioHdrSettings.EnableEnvelope : 0;
            audioProperties.HdrSettings |=
                overrideEnvelopeTracking
                | overrideLoudnessNormalization
                | enableLoudnessNormalization
                | enableEnvelope;

            audioProperties.StateGroupCount = reader.ReadUInt32();
            audioProperties.StateGroups = new AudioStateGroup[audioProperties.StateGroupCount];
            for (var i = 0; i < audioProperties.StateGroupCount; i++)
            {
                var stateGroup = new AudioStateGroup();
                stateGroup.Id = reader.ReadUInt32();
                stateGroup.MusicChangeAt = (MusicKeyPointByte)reader.ReadByte();
                stateGroup.StateWithSettingsCount = reader.ReadUInt16();
                stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                {
                    AudioStateWithSettings AudioStateWithSettings = default;
                    AudioStateWithSettings.StateId = reader.ReadUInt32();
                    AudioStateWithSettings.SettingsId = reader.ReadUInt32();
                    stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                }
                audioProperties.StateGroups[i] = stateGroup;
            }
            audioProperties.RtpcCount = reader.ReadUInt16();
            audioProperties.Rtpcs = new AudioRtpc[audioProperties.RtpcCount];
            for (var i = 0; i < audioProperties.RtpcCount; i++)
            {
                var rtpc = new AudioRtpc();
                rtpc.X = reader.ReadUInt32();
                rtpc.Parameter = (RtpcParameterType)reader.ReadUInt32();
                rtpc.UnknownId = reader.ReadUInt32();
                rtpc.CurveScalingType = (RtpcCurveType)reader.ReadByte();
                rtpc.PointCount = reader.ReadUInt16();
                rtpc.Points = new RtpcPoint[rtpc.PointCount];
                for (var j = 0; j < rtpc.PointCount; j++)
                {
                    RtpcPoint rtpcPoint = new RtpcPoint();
                    rtpcPoint.X = reader.ReadSingle();
                    rtpcPoint.Y = reader.ReadSingle();
                    rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte)reader.ReadUInt32();
                    rtpc.Points[j] = rtpcPoint;
                }
                audioProperties.Rtpcs[i] = rtpc;
            }
            return audioProperties;
        }

        private static AudioProperties ReadAudioProperties(this BinaryReader reader)
        {
            var audioProperties = new AudioProperties();
            audioProperties.OverrideEffects = reader.ReadBoolean();
            audioProperties.EffectCount = reader.ReadByte();
            if (audioProperties.EffectCount > 0)
            {
                audioProperties.BypassedEffects = (AudioBypassedEffects)reader.ReadByte();
            }
            audioProperties.Effects = new AudioEffect[audioProperties.EffectCount];
            for (var i = 0; i < audioProperties.EffectCount; i++)
            {
                AudioEffect effect = default;
                effect.Index = reader.ReadByte();
                effect.Id = reader.ReadUInt32();
                effect.ShouldUseShareSets = reader.ReadBoolean();
                effect.IsRendered = reader.ReadBoolean();
                audioProperties.Effects[i] = effect;
            }
            audioProperties.Unknown_1 = reader.ReadByte();
            audioProperties.OutputBusId = reader.ReadUInt32();
            audioProperties.ParentId = reader.ReadUInt32();
            audioProperties.PlaybackBehavior = (AudioPlaybackBehavior)reader.ReadByte();
            audioProperties.ParameterCount = reader.ReadByte();
            audioProperties.ParameterTypes = new AudioParameterType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                audioProperties.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
            }
            audioProperties.ParameterValues = new ValueType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var parameterType = audioProperties.ParameterTypes[i].ToString();
                if (parameterType.EndsWith("_UInt"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadUInt32();
                }
                else if (parameterType.EndsWith("_Int"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadInt32();
                }
                else
                {
                    audioProperties.ParameterValues[i] = reader.ReadSingle();
                }
            }
            audioProperties.ParameterPairCount = reader.ReadByte();
            audioProperties.ParameterPairTypes = new byte[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                audioProperties.ParameterPairTypes[i] = reader.ReadByte();
            }
            audioProperties.ParameterPairValues = new AudioParameterPair[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                AudioParameterPair audioParameterPair = default;
                audioParameterPair.Parameter_1 = reader.ReadSingle();
                audioParameterPair.Parameter_2 = reader.ReadSingle();
                audioProperties.ParameterPairValues[i] = audioParameterPair;
            }
            audioProperties.Positioning = (AudioPositioningBehavior)reader.ReadByte();
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.ThreeDimensional))
            {
                audioProperties.IsGameDefined = reader.ReadBoolean();
                audioProperties.AttenuationId = reader.ReadUInt32();
                if (!audioProperties.IsGameDefined)
                {
                    audioProperties.UserDefinedPlaySettings = (AudioUserDefinedPositioningBehavior)reader.ReadByte();
                    audioProperties.TransitionTime = reader.ReadUInt32();
                    audioProperties.ControlPointKeyCount = reader.ReadUInt32();
                    audioProperties.ControlPointKeys = new AudioControlPointKey[audioProperties.ControlPointKeyCount];
                    for (var i = 0; i < audioProperties.ControlPointKeyCount; i++)
                    {
                        AudioControlPointKey controlPointKey = default;
                        controlPointKey.X = reader.ReadSingle();
                        controlPointKey.Z = reader.ReadSingle();
                        controlPointKey.Y = reader.ReadSingle();
                        controlPointKey.Timestamp = reader.ReadUInt32();
                        audioProperties.ControlPointKeys[i] = controlPointKey;
                    }
                    audioProperties.RandomRangeCount = reader.ReadUInt32();
                    audioProperties.RandomRangeUnknowns = new AudioPathRandomUnknown[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomUnknown randomRangeUnknown = default;
                        randomRangeUnknown.Unknown_0 = reader.ReadUInt32();
                        randomRangeUnknown.Unknown_4 = reader.ReadUInt32();
                        audioProperties.RandomRangeUnknowns[i] = randomRangeUnknown;
                    }
                    audioProperties.RandomRanges = new AudioPathRandomRange[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomRange randomRange = default;
                        randomRange.LeftRight = reader.ReadSingle();
                        randomRange.FrontBack = reader.ReadSingle();
                        randomRange.UpDown = reader.ReadSingle();
                        audioProperties.RandomRanges[i] = randomRange;
                    }
                }
            }
            audioProperties.AuxSendsBehavior = (AudioAuxSendsBehavior)reader.ReadByte();
            if (audioProperties.AuxSendsBehavior.HasFlag(AudioAuxSendsBehavior.OverrideAuxSends))
            {
                audioProperties.AuxiliarySendBusIds = new uint[4];
                audioProperties.AuxiliarySendBusIds[0] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[1] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[2] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[3] = reader.ReadUInt32();
            }
            audioProperties.LimitBehavior = (AudioLimitBehavior)reader.ReadByte();
            audioProperties.VirtualVoiceReturnBehavior = (AudioVirtualVoiceReturnBehavior)reader.ReadByte();
            audioProperties.LimitSoundInstancesTo = reader.ReadUInt16();
            audioProperties.VirtualVoiceBehavior = (AudioVirtualVoiceBehavior)reader.ReadByte();
            audioProperties.HdrSettings = (AudioHdrSettings)reader.ReadByte();
            audioProperties.StateGroupCount = reader.ReadUInt32();
            audioProperties.StateGroups = new AudioStateGroup[audioProperties.StateGroupCount];
            for (var i = 0; i < audioProperties.StateGroupCount; i++)
            {
                var stateGroup = new AudioStateGroup();
                stateGroup.Id = reader.ReadUInt32();
                stateGroup.MusicChangeAt = (MusicKeyPointByte)reader.ReadByte();
                stateGroup.StateWithSettingsCount = reader.ReadUInt16();
                stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                {
                    AudioStateWithSettings AudioStateWithSettings = default;
                    AudioStateWithSettings.StateId = reader.ReadUInt32();
                    AudioStateWithSettings.SettingsId = reader.ReadUInt32();
                    stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                }
                audioProperties.StateGroups[i] = stateGroup;
            }
            audioProperties.RtpcCount = reader.ReadUInt16();
            audioProperties.Rtpcs = new AudioRtpc[audioProperties.RtpcCount];
            for (var i = 0; i < audioProperties.RtpcCount; i++)
            {
                var rtpc = new AudioRtpc();
                rtpc.X = reader.ReadUInt32();
                rtpc.IsMidi = reader.ReadBoolean();
                rtpc.IsGeneralSettings = reader.ReadBoolean();
                rtpc.Parameter = (RtpcParameterType)reader.ReadByte();
                rtpc.UnknownId = reader.ReadUInt32();
                rtpc.CurveScalingType = (RtpcCurveType)reader.ReadByte();
                rtpc.PointCount = reader.ReadUInt16();
                rtpc.Points = new RtpcPoint[rtpc.PointCount];
                for (var j = 0; j < rtpc.PointCount; j++)
                {
                    RtpcPoint rtpcPoint = new RtpcPoint();
                    rtpcPoint.X = reader.ReadSingle();
                    rtpcPoint.Y = reader.ReadSingle();
                    rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte)reader.ReadByte();
                    rtpcPoint.Unknown = reader.ReadBytes(3);
                    rtpc.Points[j] = rtpcPoint;
                }
                audioProperties.Rtpcs[i] = rtpc;
            }
            return audioProperties;
        }

        private static AudioProperties ReadAudioProperties2019(this BinaryReader reader)
        {
            var audioProperties = new AudioProperties();
            audioProperties.OverrideEffects = reader.ReadBoolean();
            audioProperties.EffectCount = reader.ReadByte();
            if (audioProperties.EffectCount > 0)
            {
                audioProperties.BypassedEffects = (AudioBypassedEffects)reader.ReadByte();
            }
            audioProperties.Effects = new AudioEffect[audioProperties.EffectCount];
            for (var i = 0; i < audioProperties.EffectCount; i++)
            {
                AudioEffect effect = default;
                effect.Index = reader.ReadByte();
                effect.Id = reader.ReadUInt32();
                effect.ShouldUseShareSets = reader.ReadBoolean();
                effect.IsRendered = reader.ReadBoolean();
                audioProperties.Effects[i] = effect;
            }
            audioProperties.Unknown_1 = reader.ReadByte();
            reader.BaseStream.Position += 2;
            audioProperties.OutputBusId = reader.ReadUInt32();
            audioProperties.ParentId = reader.ReadUInt32();
            audioProperties.PlaybackBehavior = (AudioPlaybackBehavior)reader.ReadByte();
            audioProperties.ParameterCount = reader.ReadByte();
            audioProperties.ParameterTypes = new AudioParameterType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                audioProperties.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
            }
            audioProperties.ParameterValues = new ValueType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var parameterType = audioProperties.ParameterTypes[i].ToString();
                if (parameterType.EndsWith("_UInt"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadUInt32();
                }
                else if (parameterType.EndsWith("_Int"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadInt32();
                }
                else
                {
                    audioProperties.ParameterValues[i] = reader.ReadSingle();
                }
            }
            audioProperties.ParameterPairCount = reader.ReadByte();
            audioProperties.ParameterPairTypes = new byte[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                audioProperties.ParameterPairTypes[i] = reader.ReadByte();
            }
            audioProperties.ParameterPairValues = new AudioParameterPair[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                AudioParameterPair audioParameterPair = default;
                audioParameterPair.Parameter_1 = reader.ReadSingle();
                audioParameterPair.Parameter_2 = reader.ReadSingle();
                audioProperties.ParameterPairValues[i] = audioParameterPair;
            }
            audioProperties.Positioning = (AudioPositioningBehavior)reader.ReadByte();
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.TwoDimensional))
            {
                //// ListenerRelativeRoutingBehavior
                //00000001 3DSpatializationPosition
                //00000010 3DSpatializationPositionAndOrientation
                //00001000 EnableAttenuation
                //00010000 HoldEmitterPositionAndOrientation
                //00100000 HoldListenerOrientation
                //01000000 ShouldLoop
                //10000000 EnableDiffraction
                reader.BaseStream.Position++;
            }
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UpdateAtEachFrame)
                || audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UserDefinedShouldLoop))
            {
                // 3DPositionListenerWithAutomation or 3DPositionEmitterWithAutomation
                audioProperties.IsGameDefined = reader.ReadBoolean();
                audioProperties.AttenuationId = reader.ReadUInt32();
                if (!audioProperties.IsGameDefined)
                {
                    audioProperties.UserDefinedPlaySettings = (AudioUserDefinedPositioningBehavior)reader.ReadByte();
                    audioProperties.TransitionTime = reader.ReadUInt32();
                    audioProperties.ControlPointKeyCount = reader.ReadUInt32();
                    audioProperties.ControlPointKeys = new AudioControlPointKey[audioProperties.ControlPointKeyCount];
                    for (var i = 0; i < audioProperties.ControlPointKeyCount; i++)
                    {
                        AudioControlPointKey controlPointKey = default;
                        controlPointKey.X = reader.ReadSingle();
                        controlPointKey.Z = reader.ReadSingle();
                        controlPointKey.Y = reader.ReadSingle();
                        controlPointKey.Timestamp = reader.ReadUInt32();
                        audioProperties.ControlPointKeys[i] = controlPointKey;
                    }
                    audioProperties.RandomRangeCount = reader.ReadUInt32();
                    audioProperties.RandomRangeUnknowns = new AudioPathRandomUnknown[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomUnknown randomRangeUnknown = default;
                        randomRangeUnknown.Unknown_0 = reader.ReadUInt32();
                        randomRangeUnknown.Unknown_4 = reader.ReadUInt32();
                        audioProperties.RandomRangeUnknowns[i] = randomRangeUnknown;
                    }
                    audioProperties.RandomRanges = new AudioPathRandomRange[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomRange randomRange = default;
                        randomRange.LeftRight = reader.ReadSingle();
                        randomRange.FrontBack = reader.ReadSingle();
                        randomRange.UpDown = reader.ReadSingle();
                        audioProperties.RandomRanges[i] = randomRange;
                    }
                }
            }
            audioProperties.AuxSendsBehavior = (AudioAuxSendsBehavior)reader.ReadByte();
            if (audioProperties.AuxSendsBehavior.HasFlag(AudioAuxSendsBehavior.OverrideAuxSends))
            {
                audioProperties.AuxiliarySendBusIds = new uint[4];
                audioProperties.AuxiliarySendBusIds[0] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[1] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[2] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[3] = reader.ReadUInt32();
            }
            // reader.BaseStream.Position += 4;    // Unknown uint 0, only exists in 2019.2.0.7216 (not in D2BL)
            audioProperties.LimitBehavior = (AudioLimitBehavior)reader.ReadByte();
            audioProperties.VirtualVoiceReturnBehavior = (AudioVirtualVoiceReturnBehavior)reader.ReadByte();
            audioProperties.LimitSoundInstancesTo = reader.ReadUInt16();
            audioProperties.VirtualVoiceBehavior = (AudioVirtualVoiceBehavior)reader.ReadByte();
            audioProperties.HdrSettings = (AudioHdrSettings)reader.ReadByte();
            var statePropertyCount = reader.ReadByte();
            for (int i = 0; i < statePropertyCount; i++)
            {
                reader.BaseStream.Position += 3;    // 3 unknown bytes
            }
            audioProperties.StateGroupCount = reader.ReadByte();
            audioProperties.StateGroups = new AudioStateGroup[audioProperties.StateGroupCount];
            for (var i = 0; i < audioProperties.StateGroupCount; i++)
            {
                var stateGroup = new AudioStateGroup();
                stateGroup.Id = reader.ReadUInt32();
                stateGroup.MusicChangeAt = (MusicKeyPointByte)reader.ReadByte();
                stateGroup.StateWithSettingsCount = reader.ReadByte();
                stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                {
                    AudioStateWithSettings AudioStateWithSettings = default;
                    AudioStateWithSettings.StateId = reader.ReadUInt32();
                    AudioStateWithSettings.SettingsId = reader.ReadUInt32();
                    stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                }
                audioProperties.StateGroups[i] = stateGroup;
            }
            audioProperties.RtpcCount = reader.ReadUInt16();
            audioProperties.Rtpcs = new AudioRtpc[audioProperties.RtpcCount];
            for (var i = 0; i < audioProperties.RtpcCount; i++)
            {
                var rtpc = new AudioRtpc();
                rtpc.X = reader.ReadUInt32();
                rtpc.IsMidi = reader.ReadBoolean();
                rtpc.IsGeneralSettings = reader.ReadBoolean();
                rtpc.Parameter = (RtpcParameterType)reader.ReadByte();
                rtpc.UnknownId = reader.ReadUInt32();
                rtpc.CurveScalingType = (RtpcCurveType)reader.ReadByte();
                rtpc.PointCount = reader.ReadUInt16();
                rtpc.Points = new RtpcPoint[rtpc.PointCount];
                for (var j = 0; j < rtpc.PointCount; j++)
                {
                    RtpcPoint rtpcPoint = new RtpcPoint();
                    rtpcPoint.X = reader.ReadSingle();
                    rtpcPoint.Y = reader.ReadSingle();
                    rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte)reader.ReadByte();
                    rtpcPoint.Unknown = reader.ReadBytes(3);
                    rtpc.Points[j] = rtpcPoint;
                }
                audioProperties.Rtpcs[i] = rtpc;
            }
            return audioProperties;
        }

        private static AudioProperties ReadAudioProperties2021(this BinaryReader reader)
        {
            var audioProperties = new AudioProperties();
            audioProperties.OverrideEffects = reader.ReadBoolean();
            audioProperties.EffectCount = reader.ReadByte();
            if (audioProperties.EffectCount > 0)
            {
                audioProperties.BypassedEffects = (AudioBypassedEffects)reader.ReadByte();
            }
            audioProperties.Effects = new AudioEffect[audioProperties.EffectCount];
            for (var i = 0; i < audioProperties.EffectCount; i++)
            {
                AudioEffect effect = default;
                effect.Index = reader.ReadByte();
                effect.Id = reader.ReadUInt32();
                effect.ShouldUseShareSets = reader.ReadBoolean();
                effect.IsRendered = reader.ReadBoolean();
                audioProperties.Effects[i] = effect;
            }
            audioProperties.Unknown_1 = reader.ReadByte();
            // Wwise 2021: u8 uNumFx
            // Wwise 2021: u8 bOverrideAttachmentParams
            reader.BaseStream.Position += 2;
            audioProperties.OutputBusId = reader.ReadUInt32();
            audioProperties.ParentId = reader.ReadUInt32();
            audioProperties.PlaybackBehavior = (AudioPlaybackBehavior)reader.ReadByte();
            audioProperties.ParameterCount = reader.ReadByte();
            audioProperties.ParameterTypes = new AudioParameterType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                audioProperties.ParameterTypes[i] = (AudioParameterType)reader.ReadByte();
            }
            audioProperties.ParameterValues = new ValueType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var parameterType = audioProperties.ParameterTypes[i].ToString();
                if (parameterType.EndsWith("_UInt"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadUInt32();
                }
                else if (parameterType.EndsWith("_Int"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadInt32();
                }
                else
                {
                    audioProperties.ParameterValues[i] = reader.ReadSingle();
                }
            }
            audioProperties.ParameterPairCount = reader.ReadByte();
            audioProperties.ParameterPairTypes = new byte[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                audioProperties.ParameterPairTypes[i] = reader.ReadByte();
            }
            audioProperties.ParameterPairValues = new AudioParameterPair[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                AudioParameterPair audioParameterPair = default;
                audioParameterPair.Parameter_1 = reader.ReadSingle();
                audioParameterPair.Parameter_2 = reader.ReadSingle();
                audioProperties.ParameterPairValues[i] = audioParameterPair;
            }
            audioProperties.Positioning = (AudioPositioningBehavior)reader.ReadByte();
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.TwoDimensional))
            {
                //// ListenerRelativeRoutingBehavior
                //00000001 3DSpatializationPosition
                //00000010 3DSpatializationPositionAndOrientation
                //00001000 EnableAttenuation
                //00010000 HoldEmitterPositionAndOrientation
                //00100000 HoldListenerOrientation
                //01000000 ShouldLoop
                //10000000 EnableDiffraction
                reader.BaseStream.Position++;
            }
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UpdateAtEachFrame)
                || audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UserDefinedShouldLoop))
            {
                // 3DPositionListenerWithAutomation or 3DPositionEmitterWithAutomation
                audioProperties.IsGameDefined = reader.ReadBoolean();
                audioProperties.AttenuationId = reader.ReadUInt32();
                if (!audioProperties.IsGameDefined)
                {
                    audioProperties.UserDefinedPlaySettings = (AudioUserDefinedPositioningBehavior)reader.ReadByte();
                    audioProperties.TransitionTime = reader.ReadUInt32();
                    audioProperties.ControlPointKeyCount = reader.ReadUInt32();
                    audioProperties.ControlPointKeys = new AudioControlPointKey[audioProperties.ControlPointKeyCount];
                    for (var i = 0; i < audioProperties.ControlPointKeyCount; i++)
                    {
                        AudioControlPointKey controlPointKey = default;
                        controlPointKey.X = reader.ReadSingle();
                        controlPointKey.Z = reader.ReadSingle();
                        controlPointKey.Y = reader.ReadSingle();
                        controlPointKey.Timestamp = reader.ReadUInt32();
                        audioProperties.ControlPointKeys[i] = controlPointKey;
                    }
                    audioProperties.RandomRangeCount = reader.ReadUInt32();
                    audioProperties.RandomRangeUnknowns = new AudioPathRandomUnknown[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomUnknown randomRangeUnknown = default;
                        randomRangeUnknown.Unknown_0 = reader.ReadUInt32();
                        randomRangeUnknown.Unknown_4 = reader.ReadUInt32();
                        audioProperties.RandomRangeUnknowns[i] = randomRangeUnknown;
                    }
                    audioProperties.RandomRanges = new AudioPathRandomRange[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomRange randomRange = default;
                        randomRange.LeftRight = reader.ReadSingle();
                        randomRange.FrontBack = reader.ReadSingle();
                        randomRange.UpDown = reader.ReadSingle();
                        audioProperties.RandomRanges[i] = randomRange;
                    }
                }
            }
            audioProperties.AuxSendsBehavior = (AudioAuxSendsBehavior)reader.ReadByte();
            if (audioProperties.AuxSendsBehavior.HasFlag(AudioAuxSendsBehavior.OverrideAuxSends))
            {
                audioProperties.AuxiliarySendBusIds = new uint[4];
                audioProperties.AuxiliarySendBusIds[0] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[1] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[2] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[3] = reader.ReadUInt32();
            }
            reader.BaseStream.Position += 4;    // reflectionsAuxBus
            audioProperties.LimitBehavior = (AudioLimitBehavior)reader.ReadByte();
            audioProperties.VirtualVoiceReturnBehavior = (AudioVirtualVoiceReturnBehavior)reader.ReadByte();
            audioProperties.LimitSoundInstancesTo = reader.ReadUInt16();
            audioProperties.VirtualVoiceBehavior = (AudioVirtualVoiceBehavior)reader.ReadByte();
            audioProperties.HdrSettings = (AudioHdrSettings)reader.ReadByte();
            var statePropertyCount = reader.ReadByte();
            for (int i = 0; i < statePropertyCount; i++)
            {
                reader.BaseStream.Position += 3;    // 3 unknown bytes
            }
            audioProperties.StateGroupCount = reader.ReadByte();
            audioProperties.StateGroups = new AudioStateGroup[audioProperties.StateGroupCount];
            for (var i = 0; i < audioProperties.StateGroupCount; i++)
            {
                var stateGroup = new AudioStateGroup();
                stateGroup.Id = reader.ReadUInt32();
                stateGroup.MusicChangeAt = (MusicKeyPointByte)reader.ReadByte();
                stateGroup.StateWithSettingsCount = reader.ReadByte();
                stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                {
                    AudioStateWithSettings AudioStateWithSettings = default;
                    AudioStateWithSettings.StateId = reader.ReadUInt32();
                    AudioStateWithSettings.SettingsId = reader.ReadUInt32();
                    stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                }
                audioProperties.StateGroups[i] = stateGroup;
            }
            audioProperties.RtpcCount = reader.ReadUInt16();
            audioProperties.Rtpcs = new AudioRtpc[audioProperties.RtpcCount];
            for (var i = 0; i < audioProperties.RtpcCount; i++)
            {
                var rtpc = new AudioRtpc();
                rtpc.X = reader.ReadUInt32();
                rtpc.IsMidi = reader.ReadBoolean();
                rtpc.IsGeneralSettings = reader.ReadBoolean();
                rtpc.Parameter = (RtpcParameterType)reader.ReadByte();
                rtpc.UnknownId = reader.ReadUInt32();
                rtpc.CurveScalingType = (RtpcCurveType)reader.ReadByte();
                rtpc.PointCount = reader.ReadUInt16();
                rtpc.Points = new RtpcPoint[rtpc.PointCount];
                for (var j = 0; j < rtpc.PointCount; j++)
                {
                    RtpcPoint rtpcPoint = new RtpcPoint();
                    rtpcPoint.X = reader.ReadSingle();
                    rtpcPoint.Y = reader.ReadSingle();
                    rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte)reader.ReadByte();
                    rtpcPoint.Unknown = reader.ReadBytes(3);
                    rtpc.Points[j] = rtpcPoint;
                }
                audioProperties.Rtpcs[i] = rtpc;
            }
            return audioProperties;
        }

        private static AudioProperties ReadAudioProperties2023(this BinaryReader reader)
        {
            var audioParameterTypeMapV150 = new Dictionary<byte, AudioParameterType>()
            {
                [0x00] = AudioParameterType.VoiceVolume,
                [0x01] = AudioParameterType.VoicePitch,
                [0x02] = AudioParameterType.VoiceLowPass,
                [0x03] = AudioParameterType.VoiceHighPass,
                [0x04] = AudioParameterType.BusVolume,
                [0x05] = AudioParameterType.MakeUpGain,
                [0x06] = AudioParameterType.PlaybackPriority,
                [0x07] = AudioParameterType.MuteRatio,
                [0x08] = AudioParameterType.OverrideAuxBus0Volume,
                [0x09] = AudioParameterType.OverrideAuxBus1Volume,
                [0x0A] = AudioParameterType.OverrideAuxBus2Volume,
                [0x0B] = AudioParameterType.OverrideAuxBus3Volume,
                [0x0C] = AudioParameterType.GameDefinedAuxSendVolume,
                [0x0D] = AudioParameterType.OverrideBusVolume,
                [0x0E] = AudioParameterType.OverrideBusHighPassFilter,
                [0x0F] = AudioParameterType.OverrideBusLowPassFilter,
                [0x1B] = AudioParameterType.HdrThreshold,
                [0x1C] = AudioParameterType.HdrRatio,
                [0x1D] = AudioParameterType.HdrReleaseTime,
                [0x1E] = AudioParameterType.HdrEnvelopeActiveRange,
                [0x1F] = AudioParameterType.MidiTransposition_Int,
                [0x20] = AudioParameterType.MidiVelocityOffset_Int,
                [0x21] = AudioParameterType.PlaybackSpeed,
                [0x22] = AudioParameterType.InitialDelay,
                [0x23] = AudioParameterType.PositioningPannerX,
                [0x24] = AudioParameterType.PositioningPannerY,
                [0x29] = AudioParameterType.PositioningCenterPercentage,
                [0x3A] = AudioParameterType.LoopTime_UInt,
                [0x3B] = AudioParameterType.Probability,
                [0x3D] = AudioParameterType.HdrOutputGameParam,
                [0x3E] = AudioParameterType.HdrOutputGameParamMin,
                [0x3F] = AudioParameterType.HdrOutputGameParamMax,
                [0x4D] = AudioParameterType.MidiFiltersKeyRangeMin,
                [0x4E] = AudioParameterType.MidiFiltersKeyRangeMax,
                [0x4F] = AudioParameterType.MidiFiltersVelocityRangeMin,
                [0x50] = AudioParameterType.MidiFiltersVelocityRangeMax,
            };

            var audioProperties = new AudioProperties();
            audioProperties.OverrideEffects = reader.ReadBoolean();
            audioProperties.EffectCount = reader.ReadByte();
            if (audioProperties.EffectCount > 0)
            {
                audioProperties.BypassedEffects = (AudioBypassedEffects) reader.ReadByte();
            }
            audioProperties.Effects = new AudioEffect[audioProperties.EffectCount];
            for (var i = 0; i < audioProperties.EffectCount; i++)
            {
                AudioEffect effect = default;
                effect.Index = reader.ReadByte();
                effect.Id = reader.ReadUInt32();
                effect.ShouldUseShareSets = reader.ReadBoolean();
                effect.IsRendered = reader.ReadBoolean();
                audioProperties.Effects[i] = effect;
            }
            // Wwise 2021: u8 uNumFx
            // Wwise 2021: u8 bOverrideAttachmentParams
            reader.BaseStream.Position += 2;
            audioProperties.OutputBusId = reader.ReadUInt32();
            audioProperties.ParentId = reader.ReadUInt32();
            audioProperties.PlaybackBehavior = (AudioPlaybackBehavior) reader.ReadByte();
            audioProperties.ParameterCount = reader.ReadByte();
            audioProperties.ParameterTypes = new AudioParameterType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var rawParameterType = reader.ReadByte();
                if (audioParameterTypeMapV150.TryGetValue(rawParameterType, out var parameterType))
                {
                    audioProperties.ParameterTypes[i] = parameterType;
                }
                else
                {
                    audioProperties.ParameterTypes[i] = (AudioParameterType) rawParameterType;
                }
            }
            audioProperties.ParameterValues = new ValueType[audioProperties.ParameterCount];
            for (var i = 0; i < audioProperties.ParameterCount; i++)
            {
                var parameterType = audioProperties.ParameterTypes[i].ToString();
                if (parameterType.EndsWith("_UInt"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadUInt32();
                }
                else if (parameterType.EndsWith("_Int"))
                {
                    audioProperties.ParameterValues[i] = reader.ReadInt32();
                }
                else
                {
                    audioProperties.ParameterValues[i] = reader.ReadSingle();
                }
            }
            audioProperties.ParameterPairCount = reader.ReadByte();
            audioProperties.ParameterPairTypes = new byte[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                audioProperties.ParameterPairTypes[i] = reader.ReadByte();
            }
            audioProperties.ParameterPairValues = new AudioParameterPair[audioProperties.ParameterPairCount];
            for (var i = 0; i < audioProperties.ParameterPairCount; i++)
            {
                AudioParameterPair audioParameterPair = default;
                audioParameterPair.Parameter_1 = reader.ReadSingle();
                audioParameterPair.Parameter_2 = reader.ReadSingle();
                audioProperties.ParameterPairValues[i] = audioParameterPair;
            }
            audioProperties.Positioning = (AudioPositioningBehavior) reader.ReadByte();
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.TwoDimensional))
            {
                //// ListenerRelativeRoutingBehavior
                //00000001 3DSpatializationPosition
                //00000010 3DSpatializationPositionAndOrientation
                //00001000 EnableAttenuation
                //00010000 HoldEmitterPositionAndOrientation
                //00100000 HoldListenerOrientation
                //01000000 ShouldLoop
                //10000000 EnableDiffraction
                reader.BaseStream.Position++;
            }
            if (audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UpdateAtEachFrame)
                || audioProperties.Positioning.HasFlag(AudioPositioningBehavior.UserDefinedShouldLoop))
            {
                // 3DPositionListenerWithAutomation or 3DPositionEmitterWithAutomation
                audioProperties.IsGameDefined = reader.ReadBoolean();
                audioProperties.AttenuationId = reader.ReadUInt32();
                if (!audioProperties.IsGameDefined)
                {
                    audioProperties.UserDefinedPlaySettings = (AudioUserDefinedPositioningBehavior) reader.ReadByte();
                    audioProperties.TransitionTime = reader.ReadUInt32();
                    audioProperties.ControlPointKeyCount = reader.ReadUInt32();
                    audioProperties.ControlPointKeys = new AudioControlPointKey[audioProperties.ControlPointKeyCount];
                    for (var i = 0; i < audioProperties.ControlPointKeyCount; i++)
                    {
                        AudioControlPointKey controlPointKey = default;
                        controlPointKey.X = reader.ReadSingle();
                        controlPointKey.Z = reader.ReadSingle();
                        controlPointKey.Y = reader.ReadSingle();
                        controlPointKey.Timestamp = reader.ReadUInt32();
                        audioProperties.ControlPointKeys[i] = controlPointKey;
                    }
                    audioProperties.RandomRangeCount = reader.ReadUInt32();
                    audioProperties.RandomRangeUnknowns = new AudioPathRandomUnknown[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomUnknown randomRangeUnknown = default;
                        randomRangeUnknown.Unknown_0 = reader.ReadUInt32();
                        randomRangeUnknown.Unknown_4 = reader.ReadUInt32();
                        audioProperties.RandomRangeUnknowns[i] = randomRangeUnknown;
                    }
                    audioProperties.RandomRanges = new AudioPathRandomRange[audioProperties.RandomRangeCount];
                    for (var i = 0; i < audioProperties.RandomRangeCount; i++)
                    {
                        AudioPathRandomRange randomRange = default;
                        randomRange.LeftRight = reader.ReadSingle();
                        randomRange.FrontBack = reader.ReadSingle();
                        randomRange.UpDown = reader.ReadSingle();
                        audioProperties.RandomRanges[i] = randomRange;
                    }
                }
            }
            audioProperties.AuxSendsBehavior = (AudioAuxSendsBehavior) reader.ReadByte();
            if (audioProperties.AuxSendsBehavior.HasFlag(AudioAuxSendsBehavior.OverrideAuxSends))
            {
                audioProperties.AuxiliarySendBusIds = new uint[4];
                audioProperties.AuxiliarySendBusIds[0] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[1] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[2] = reader.ReadUInt32();
                audioProperties.AuxiliarySendBusIds[3] = reader.ReadUInt32();
            }
            reader.BaseStream.Position += 4;    // reflectionsAuxBus
            audioProperties.LimitBehavior = (AudioLimitBehavior) reader.ReadByte();
            audioProperties.VirtualVoiceReturnBehavior = (AudioVirtualVoiceReturnBehavior) reader.ReadByte();
            audioProperties.LimitSoundInstancesTo = reader.ReadUInt16();
            audioProperties.VirtualVoiceBehavior = (AudioVirtualVoiceBehavior) reader.ReadByte();
            audioProperties.HdrSettings = (AudioHdrSettings) reader.ReadByte();
            var statePropertyCount = reader.ReadByte();
            for (int i = 0; i < statePropertyCount; i++)
            {
                reader.BaseStream.Position += 3;    // 3 unknown bytes
            }
            audioProperties.StateGroupCount = reader.ReadByte();
            audioProperties.StateGroups = new AudioStateGroup[audioProperties.StateGroupCount];
            for (var i = 0; i < audioProperties.StateGroupCount; i++)
            {
                var stateGroup = new AudioStateGroup();
                stateGroup.Id = reader.ReadUInt32();
                stateGroup.MusicChangeAt = (MusicKeyPointByte) reader.ReadByte();
                stateGroup.StateWithSettingsCount = reader.ReadByte();
                stateGroup.StatesWithSettings = new AudioStateWithSettings[stateGroup.StateWithSettingsCount];
                for (var j = 0; j < stateGroup.StateWithSettingsCount; j++)
                {
                    AudioStateWithSettings AudioStateWithSettings = default;
                    AudioStateWithSettings.StateId = reader.ReadUInt32();
                    var stateSettingsCount = reader.ReadUInt16();
                    // Omit entire PropBundle with u16 types and u32 values
                    reader.BaseStream.Position += stateSettingsCount * (2 + 4);
                    stateGroup.StatesWithSettings[j] = AudioStateWithSettings;
                }
                audioProperties.StateGroups[i] = stateGroup;
            }
            audioProperties.RtpcCount = reader.ReadUInt16();
            audioProperties.Rtpcs = new AudioRtpc[audioProperties.RtpcCount];
            for (var i = 0; i < audioProperties.RtpcCount; i++)
            {
                var rtpc = new AudioRtpc();
                rtpc.X = reader.ReadUInt32();
                rtpc.IsMidi = reader.ReadBoolean();
                rtpc.IsGeneralSettings = reader.ReadBoolean();
                rtpc.Parameter = (RtpcParameterType) reader.ReadByte();
                rtpc.UnknownId = reader.ReadUInt32();
                rtpc.CurveScalingType = (RtpcCurveType) reader.ReadByte();
                rtpc.PointCount = reader.ReadUInt16();
                rtpc.Points = new RtpcPoint[rtpc.PointCount];
                for (var j = 0; j < rtpc.PointCount; j++)
                {
                    RtpcPoint rtpcPoint = new RtpcPoint();
                    rtpcPoint.X = reader.ReadSingle();
                    rtpcPoint.Y = reader.ReadSingle();
                    rtpcPoint.FollowingCurveShape = (AudioCurveShapeByte) reader.ReadByte();
                    rtpcPoint.Unknown = reader.ReadBytes(3);
                    rtpc.Points[j] = rtpcPoint;
                }
                audioProperties.Rtpcs[i] = rtpc;
            }
            return audioProperties;
        }

        private static AudioPathNode ReadPaths(this BinaryReader reader, uint pathSectionLength, uint[] audioIds)
        {
            // Assert section integrity
            if (pathSectionLength % 0x0C > 0)
            {
                throw new ArgumentException("PathSectionLength should be exactly multiples of 0x0C.");
            }

            // Read section bytes
            var sectionCount = (int)pathSectionLength / 0x0C;
            var sections = new List<byte[]>(sectionCount);
            while (sectionCount > 0)
            {
                sections.Add(reader.ReadBytes(0x0C));
                sectionCount--;
            }

            // Read root node (index 0)
            return ReadPathElement(sections, audioIds, 0) as AudioPathNode;
        }

        private static AudioPathElement ReadPathElement(List<byte[]> sections, uint[] audioIds, uint childrenStartAt)
        {
            var elementIsEndpoint = false;
            var hasAudioIds = audioIds != null;
            var section = sections[(int)childrenStartAt];

            var fromStateOrSwitchId = BitConverter.ToUInt32(section, 0);
            var audioId = BitConverter.ToUInt32(section, 4);                // either childId...
            var childrenStartAtIndex = BitConverter.ToUInt16(section, 4);   // or childrenStartAtIndex
            var childCount = BitConverter.ToUInt16(section, 6);             // with childCount
            var weight = BitConverter.ToUInt16(section, 8);
            var probability = BitConverter.ToUInt16(section, 10);

            // Figure out whether the element is a node or an endpoint
            // FIXME: workaround - some audio IDs don't appear in the audioIds list
            // in fact, doesn't show up in the HIRC list, at all!
            // try to assume it's not an endpoint, before using it as an endpoint
            if (childrenStartAtIndex < sections.Count          // Children index not out of bound (still possible to be a child)
                && childrenStartAtIndex > childrenStartAt           // Children's children start at somewhere down the road
                && childCount < sections.Count - childrenStartAt)   // Children's children count not out of bound
            {
                // element is a node
                elementIsEndpoint = false;
            }
            //else if (hasAudioIds && audioIds.Contains(audioId))
            //{
            //    // element is an endpoint
            //    elementIsEndpoint = true;
            //}
            else
            {
                // element is an endpoint
                elementIsEndpoint = true;
            }

            if (elementIsEndpoint)
            {
                // childId is an audio object, reached the end
                var endpoint = new MusicPathEndpoint();
                endpoint.FromStateOrSwitchId = fromStateOrSwitchId;
                endpoint.AudioId = audioId;
                endpoint.Weight = weight;
                endpoint.Probability = probability;
                return endpoint;
            }
            else
            {
                // childId is not an ID, reached a node
                var node = new AudioPathNode();
                node.FromStateOrSwitchId = fromStateOrSwitchId;
                node.ChildrenStartAtIndex = childrenStartAtIndex;
                node.ChildCount = childCount;
                node.Children = new AudioPathElement[node.ChildCount];
                for (uint i = 0; i < node.ChildCount; i++)
                {
                    node.Children[i] = ReadPathElement(sections, audioIds, node.ChildrenStartAtIndex + i);
                }
                node.Weight = weight;
                node.Probability = probability;
                return node;
            }
        }
        
        private static MusicPlaylistElement ReadPlaylist2013(this BinaryReader reader)
        {
            var element = reader.ReadPlaylistElement2013();
            element.Children = new MusicPlaylistElement[element.ChildCount];
            for (var i = 0; i < element.ChildCount; i++)
            {
                element.Children[i] = reader.ReadPlaylist2013();
            }
            return element;
        }

        private static MusicPlaylistElement ReadPlaylist(this BinaryReader reader)
        {
            var element = reader.ReadPlaylistElement();
            element.Children = new MusicPlaylistElement[element.ChildCount];
            for (var i = 0; i < element.ChildCount; i++)
            {
                element.Children[i] = reader.ReadPlaylist();
            }
            return element;
        }

        private static MusicPlaylistElement ReadPlaylistElement2013(this BinaryReader reader)
        {
            MusicPlaylistElement musicPlaylistElement = new MusicPlaylistElement();
            musicPlaylistElement.SegmentId = reader.ReadUInt32();
            musicPlaylistElement.UnknownId = reader.ReadUInt32();
            musicPlaylistElement.ChildCount = reader.ReadUInt32();
            musicPlaylistElement.Type = (MusicPlaylistElementType)reader.ReadUInt32();
            musicPlaylistElement.LoopCount = reader.ReadUInt16();
            musicPlaylistElement.Weight = reader.ReadUInt32();
            musicPlaylistElement.AvoidRepeatCount = reader.ReadUInt16();
            musicPlaylistElement.IsGroup = reader.ReadBoolean();
            musicPlaylistElement.IsShuffle = reader.ReadBoolean();

            return musicPlaylistElement;
        }

        private static MusicPlaylistElement ReadPlaylistElement(this BinaryReader reader)
        {
            MusicPlaylistElement musicPlaylistElement = new MusicPlaylistElement();
            musicPlaylistElement.SegmentId = reader.ReadUInt32();
            musicPlaylistElement.UnknownId = reader.ReadUInt32();
            musicPlaylistElement.ChildCount = reader.ReadUInt32();
            musicPlaylistElement.Type = (MusicPlaylistElementType)reader.ReadUInt32();
            musicPlaylistElement.LoopCount = reader.ReadUInt16();
            musicPlaylistElement.RandomizerLoopCountMin = reader.ReadInt16();
            musicPlaylistElement.RandomizerLoopCountMax = reader.ReadInt16();
            musicPlaylistElement.Weight = reader.ReadUInt32();
            musicPlaylistElement.AvoidRepeatCount = reader.ReadUInt16();
            musicPlaylistElement.IsGroup = reader.ReadBoolean();
            musicPlaylistElement.IsShuffle = reader.ReadBoolean();

            return musicPlaylistElement;
        }

        private static void ReadEventActionExceptions(
            this BinaryReader reader, EventActionSettingsWithExceptions settings)
        {
            settings.ExceptionCount = reader.ReadByte();
            settings.Exceptions = new EventActionException[settings.ExceptionCount];
            for (var i = 0; i < settings.ExceptionCount; i++)
            {
                EventActionException exception = new EventActionException();
                exception.Id = reader.ReadUInt32();
                exception.Unknown = reader.ReadByte();
                settings.Exceptions[i] = exception;
            }
        }
    }
}
