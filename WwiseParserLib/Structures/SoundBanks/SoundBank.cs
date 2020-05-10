using System;
using System.Linq;
using WwiseParserLib.Parsers.BKHD;
using WwiseParserLib.Parsers.HIRC;
using WwiseParserLib.Parsers.STMG;
using WwiseParserLib.Structures.Hierarchies;
using WwiseParserLib.Structures.Objects.HIRC;
using WwiseParserLib.Structures.Sections;

namespace WwiseParserLib.Structures.SoundBanks
{
    public abstract class SoundBank
    {
        protected SoundBank()
        {
            // Set section array length to maximum possible
            var sectionCount = Enum.GetValues(typeof(SoundBankSectionName)).Length;
            _parsedSections = new SoundBankSection[sectionCount];
        }

        protected SoundBankSection[] _parsedSections;

        // read binary data of a section
        public abstract byte[] ReadSection(SoundBankSectionName name);

        // try to read and parse a section, null if not exists
        public SoundBankSection ParseSection(SoundBankSectionName name)
        {
            var blob = ReadSection(name);
            if (blob == null)
            {
                return null;
            }

            switch (name)
            {
                case SoundBankSectionName.BKHD:
                    return BKHDParser.Parse(blob);

                case SoundBankSectionName.HIRC:
                    return HIRCParser.Parse(blob);

                case SoundBankSectionName.STMG:
                    return STMGParser.Parse(blob);

                default:
                    throw new NotImplementedException();
            }
        }

        // try to get a parsed section, null if not exists
        public SoundBankSection GetSection(SoundBankSectionName name)
        {
            var section = _parsedSections
                .SingleOrDefault(x => x != null && x.Name == name);
            if (section == null)
            {
                if ((section = ParseSection(name)) == null)
                {
                    // Section does not exist
                    return null;
                }
                else
                {
                    // Save section and return
                    for (var i = 0; i < _parsedSections.Length; i++)
                    {
                        _parsedSections[i] ??= section;
                    }
                    return section;
                }
            }
            else
            {
                // Return saved section
                return section;
            }
        }

        public MasterMixerHierarchy CreateMasterMixerHierarchy()
        {
            var hircSection = GetSection(SoundBankSectionName.HIRC);
            if (hircSection == null)
            {
                throw new InvalidOperationException(
                    "The SoundBank does not have a HIRC section, or is not yet parsed.");
            }

            var hier = new MasterMixerHierarchy();
            var buses = (hircSection as HIRCSection).Objects
                .Where(o => o is AudioBus)
                .Select(o => o as AudioBus);
            hier.AddBuses(buses);
            return hier;
        }

        public ActorMixerHierarchy CreateActorMixerHierarchy()
        {
            var hircSection = GetSection(SoundBankSectionName.HIRC);
            if (hircSection == null)
            {
                throw new InvalidOperationException(
                    "The SoundBank does not have a HIRC section, or is not yet parsed.");
            }

            var hier = new ActorMixerHierarchy();
            var actors = (hircSection as HIRCSection).Objects
                .Where(o => o is Actor)
                .Select(o => o as Actor);
            hier.AddActors(actors);
            return hier;
        }
    }
}
