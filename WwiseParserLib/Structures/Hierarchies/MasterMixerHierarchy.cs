using System;
using System.Collections.Generic;
using System.Linq;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    /// <summary>
    /// Represents a Wwise Master-Mixer Hierarchy.
    /// </summary>
    public class MasterMixerHierarchy
    {
        /// <summary>
        /// The FNV-1 hash of the Master Audio Bus.
        /// </summary>
        public const uint MasterAudioBusId = 0xe2b7bc37;

        /// <summary>
        /// The FNV-1 hash of the Master Secondary Bus.
        /// </summary>
        public const uint MasterSecondaryBusId = 0x2ffe6ef7;

        /// <summary>
        /// The Master Audio Bus of the hierarchy.
        /// </summary>
        public AudioBus MasterAudioBus { get; private set; }

        /// <summary>
        /// The Master Secondary Bus of the hierarchy.
        /// </summary>
        public AudioBus MasterSecondaryBus { get; private set; }

        /// <summary>
        /// All unconnected buses in the hierarchy.
        /// The key is the parent ID.
        /// </summary>
        private ILookup<uint, AudioBus> _buses;

        /// <summary>
        /// Whether the hierarchy is already loaded.
        /// </summary>
        private bool _loaded;

        /// <summary>
        /// Rebuilds the hierarchy with the specified collection of buses.
        /// </summary>
        /// <param name="buses">The collection of buses.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the hierarchy is already loaded.</exception>
        public void AddBuses(IEnumerable<AudioBus> buses)
        {
            if (_loaded)
            {
                throw new InvalidOperationException("The hierarchy is already loaded.");
            }

            _buses = buses.ToLookup(bus => bus.ParentId, bus => bus);
            MasterAudioBus = _buses[0].Single(bus => bus.Id == MasterAudioBusId);
            MasterSecondaryBus = _buses[0].Single(bus => bus.Id == MasterSecondaryBusId);
            FindChildren(MasterAudioBus);
            FindChildren(MasterSecondaryBus);

            _buses = null;
            _loaded = true;
        }

        /// <summary>
        /// Finds children for the specified Audio Bus in the current hierarchy.
        /// </summary>
        /// <param name="audioBus">The Audio Bus to find children for.</param>
        private void FindChildren(AudioBus audioBus)
        {
            foreach (var bus in _buses[audioBus.Id])
            {
                bus.SetParent(audioBus);
                FindChildren(bus);
            }
        }
    }
}
