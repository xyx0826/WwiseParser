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
        /// <summary>
        /// All parsed sections of the current SoundBank.
        /// </summary>
        protected SoundBankSection[] _parsedSections;

        protected SoundBank()
        {
            // Assume array will contain all sections
            var sectionCount = Enum.GetValues(typeof(SoundBankSectionName)).Length;
            _parsedSections = new SoundBankSection[sectionCount];
        }

        /// <summary>
        /// Reads the binary data of the specified section.
        /// </summary>
        /// <param name="name">The name of the section to read.</param>
        /// <returns>The data of the section.</returns>
        public abstract byte[] ReadSection(SoundBankSectionName name);

        /// <summary>
        /// Parses the specified section.
        /// </summary>
        /// <param name="name">The name of the section to parse.</param>
        /// <returns>The parsed section, or <see cref="null"/> if one does not exist.</returns>
        /// <exception cref="NotImplementedException">
        /// Thrown when a section other than <see cref="SoundBankSectionName.BKHD"/>,
        /// <see cref="SoundBankSectionName.HIRC"/>, <see cref="SoundBankSectionName.STMG"/>
        /// is specified and exists, because their parsers are not implemented yet.</exception>
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

        /// <summary>
        /// Gets the parsed specified section from the current SoundBank.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <returns>The parsed specified section, or <see cref="null"/> if one does not exist.</returns>
        /// <exception cref="NotImplementedException">
        /// Thrown when an unsupported section name is specified.
        /// See <see cref="ParseSection(SoundBankSectionName)"/>.</exception>
        public SoundBankSection GetSection(SoundBankSectionName name)
        {
            // Is it already parsed?
            // The index of the section in all sections
            var sectionIdx = Array.IndexOf(
                Enum.GetValues(typeof(SoundBankSectionName)), name);
            var section = _parsedSections[sectionIdx];
            if (section == null)
            {
                if ((section = ParseSection(name)) == null)
                {
                    // Section does not exist
                    return null;
                }
                else
                {
                    // Save parsed section and return
                    _parsedSections[sectionIdx] = section;
                    return section;
                }
            }
            else
            {
                // Return already parsed section
                return section;
            }
        }

        /// <summary>
        /// Creates a Master-Mixer Hierarchy from the current SoundBank.
        /// </summary>
        /// <returns>The parsed and rebuilt hierarchy.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a HIRC section does not exist.</exception>
        public MasterMixerHierarchy CreateMasterMixerHierarchy()
        {
            var hircSection = GetSection(SoundBankSectionName.HIRC);
            if (hircSection == null)
            {
                throw new InvalidOperationException(
                    "The SoundBank does not have a HIRC section.");
            }

            var hier = new MasterMixerHierarchy();
            var buses = (hircSection as HIRCSection).Objects
                .Where(o => o is AudioBus)
                .Select(o => o as AudioBus);
            hier.AddBuses(buses);
            return hier;
        }

        /// <summary>
        /// Creates an Actor-Mixer hierarchy from the current SoundBank.
        /// </summary>
        /// <returns>The parsed and rebuilt hierarchy.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a HIRC section does not exist.</exception>
        public ActorMixerHierarchy CreateActorMixerHierarchy()
        {
            var hircSection = GetSection(SoundBankSectionName.HIRC);
            if (hircSection == null)
            {
                throw new InvalidOperationException(
                    "The SoundBank does not have a HIRC section.");
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
