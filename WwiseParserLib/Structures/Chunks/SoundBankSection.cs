namespace WwiseParserLib.Structures.Chunks
{
    /// <summary>
    /// Base class representing a Wwise SoundBank section.
    /// </summary>
    public abstract class SoundBankSection
    {
        private SoundBankSection() { }

        protected SoundBankSection(SoundBankSectionName name, uint length)
        {
            Name = name;
            Length = length;
        }

        /// <summary>
        /// The name of the section.
        /// </summary>
        public SoundBankSectionName Name { get; private set; }

        /// <summary>
        /// The length of the section, in bytes, excluding the name and the length.
        /// </summary>
        public uint Length { get; private set; }
    }

    public enum SoundBankSectionName : uint
    {
        /// <summary>
        /// A section that contains basic SoundBank metadata. Stands for "bank header."
        /// </summary>
        BKHD = 0x44484B42,

        /// <summary>
        /// A section that contains references to embedded Wwise Encoded Media. Stands for "data index."
        /// </summary>
        DIDX = 0x58444944,

        /// <summary>
        /// A section that contains embedded Wwise Encoded Media. Stands for "data."
        /// </summary>
        DATA = 0x41544144,

        /// <summary>
        /// A section that contains environment information. Stands for "environments."
        /// </summary>
        ENVS = 0x53564E45,

        /// <summary>
        /// An unknown section. Possibly stands for "effects production."
        /// </summary>
        FXPR = 0x52505846,

        /// <summary>
        /// A section that contains Wwise objects.
        /// </summary>
        HIRC = 0x43524948,

        /// <summary>
        /// A section that contains references to other SoundBanks and their names.
        /// </summary>
        STID = 0x44495453,

        /// <summary>
        /// A section that contains Game Syncs. Only exists in Init.bnk SoundBank.
        /// </summary>
        STMG = 0x474D5453,

        /// <summary>
        /// A section that contains platform information. Stands for "platform."
        /// </summary>
        PLAT = 0x54414C50,

        /// <summary>
        /// An unknown section. Possibly stands for "initialization."
        /// </summary>
        INIT = 0x54494E49
    }
}
