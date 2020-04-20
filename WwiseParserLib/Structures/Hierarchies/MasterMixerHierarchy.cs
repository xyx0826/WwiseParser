using System;
using System.Collections.Generic;
using System.Linq;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    public class MasterMixerHierarchy
    {
        public const uint MasterAudioBusId = 0xe2b7bc37;

        public const uint MasterSecondaryBusId = 0x2ffe6ef7;

        public AudioBus MasterAudioBus { get; private set; }

        public AudioBus MasterSecondaryBus { get; private set; }

        private ILookup<uint, AudioBus> _buses;

        private bool _loaded;

        /// <summary>
        /// Adds the specified buses to the hierarchy. Only works once.
        /// </summary>
        /// <param name="buses">Buses to add.</param>
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
