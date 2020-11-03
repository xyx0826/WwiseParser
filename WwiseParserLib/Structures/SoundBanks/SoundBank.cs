using System;
using System.Linq;
using WwiseParserLib.Parsers;
using WwiseParserLib.Structures.Hierarchies;
using WwiseParserLib.Structures.Objects.HIRC;
using WwiseParserLib.Structures.Chunks;

namespace WwiseParserLib.Structures.SoundBanks
{
    public abstract class SoundBank
    {
        /// <summary>
        /// All parsed chunks of the current SoundBank.
        /// </summary>
        protected SoundBankChunk[] _parsedChunks;

        protected SoundBank()
        {
            // Assume array will contain all chunks
            var chunkCount = Enum.GetValues(typeof(SoundBankChunkType)).Length;
            _parsedChunks = new SoundBankChunk[chunkCount];
        }

        /// <summary>
        /// Reads the binary data of the specified chunk.
        /// </summary>
        /// <param name="name">The name of the chunk to read.</param>
        /// <returns>The data of the chunk.</returns>
        public abstract byte[] ReadChunkBlob(SoundBankChunkType name);

        /// <summary>
        /// Parses the specified chunk.
        /// </summary>
        /// <param name="name">The name of the chunk to parse.</param>
        /// <param name="noParse">Whether to not parse HIRC objects. If true,
        /// all objects will be <see cref="Unknown"/>.</param>
        /// <returns>The parsed chunk, or null if the specified chunk is unsupported or does not exist.</returns>
        public SoundBankChunk ParseChunk(SoundBankChunkType name, bool noParse = false)
        {
            var blob = ReadChunkBlob(name);
            if (blob == null)
            {
                return null;
            }

            switch (name)
            {
                case SoundBankChunkType.BKHD:
                    return BKHDParser.Parse(blob);

                case SoundBankChunkType.HIRC:
                    return HIRCParser.Parse(blob, noParse);

                case SoundBankChunkType.STMG:
                    return STMGParser.Parse(blob);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the parsed specified chunk from the current SoundBank.
        /// </summary>
        /// <param name="name">The name of the chunk.</param>
        /// <param name="noParse">Whether to not parse HIRC objects. If true,
        /// all objects will be <see cref="Unknown"/>.</param>
        /// <returns>The parsed specified chunk, or null if one does not exist.</returns>
        /// <exception cref="NotImplementedException">
        /// Thrown when an unsupported chunk name is specified.
        /// See <see cref="ParseChunk(SoundBankChunkType)"/>.</exception>
        public SoundBankChunk GetChunk(SoundBankChunkType name, bool noParse = false)
        {
            // Is it already parsed?
            // The index of the chunk in all chunks
            var chunkAt = Array.IndexOf(
                Enum.GetValues(typeof(SoundBankChunkType)), name);
            var chunk = _parsedChunks[chunkAt];
            if (chunk == null)
            {
                // Chunk not already parsed, try parsing it now
                if ((chunk = ParseChunk(name, noParse)) == null)
                {
                    // Chunk does not exist
                    return null;
                }
                else
                {
                    // Save parsed chunk and return
                    _parsedChunks[chunkAt] = chunk;
                    return chunk;
                }
            }
            else
            {
                // Return already parsed chunk
                return chunk;
            }
        }

        /// <summary>
        /// Creates a Master-Mixer Hierarchy from the current SoundBank.
        /// </summary>
        /// <returns>The parsed and rebuilt hierarchy, or null
        /// if the current SoundBank doesn't have a HIRC chunk.</returns>
        public MasterMixerHierarchy CreateMasterMixerHierarchy()
        {
            var hirc = GetChunk(SoundBankChunkType.HIRC);
            if (hirc == null)
            {
                return null;
            }

            var hier = new MasterMixerHierarchy();
            var buses = (hirc as SoundBankHierarchyChunk).Objects
                .Where(o => o is AudioBus)
                .Select(o => o as AudioBus);
            hier.AddBuses(buses);
            return hier;
        }

        /// <summary>
        /// Creates an Actor-Mixer hierarchy from the current SoundBank.
        /// </summary>
        /// <returns>The parsed and rebuilt hierarchy, or null
        /// if the current SoundBank doesn't have a HIRC chunk.</returns>
        public ActorMixerHierarchy CreateActorMixerHierarchy()
        {
            var hirc = GetChunk(SoundBankChunkType.HIRC);
            if (hirc == null)
            {
                return null;
            }

            var hier = new ActorMixerHierarchy();
            var actors = (hirc as SoundBankHierarchyChunk).Objects
                .Where(o => o is SoundObject)
                .Select(o => o as SoundObject);
            hier.LoadSoundObjects(actors);
            return hier;
        }

        /// <summary>
        /// Creates an Interactive Music Hierarchy from the current SoundBank.
        /// </summary>
        /// <returns>The parsed and rebuilt hierarchy, or null
        /// if the current SoundBank doesn't have a HIRC chunk.</returns>
        public InteractiveMusicHierarchy CreateInteractiveMusicHierarchy()
        {
            var hirc = GetChunk(SoundBankChunkType.HIRC);
            if (hirc == null)
            {
                return null;
            }

            var hier = new InteractiveMusicHierarchy();
            var musicObjs = (hirc as SoundBankHierarchyChunk).Objects
                .Where(o => o is MusicObject)
                .Select(o => o as MusicObject);
            hier.LoadMusicObjects(musicObjs);
            return hier;
        }
    }
}
