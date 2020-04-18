using System;
using System.Linq;
using WwiseParserLib.Parsers.BKHD;
using WwiseParserLib.Parsers.HIRC;
using WwiseParserLib.Parsers.STMG;
using WwiseParserLib.Structures.Sections;

namespace WwiseParserLib.Structures.SoundBanks
{
    public abstract class SoundBank
    {
        public SoundBank()
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
            var section = _parsedSections.SingleOrDefault(x => x.Name == name);
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
    }
}
