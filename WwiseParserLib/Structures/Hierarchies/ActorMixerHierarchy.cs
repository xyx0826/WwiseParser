using System;
using System.Collections.Generic;
using System.Linq;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    public class ActorMixerHierarchy
    {
        private ILookup<uint, Actor> _actorGroups;

        private bool _loaded;

        private List<Actor> _linkedActors;

        public IReadOnlyCollection<Actor> Actors => _linkedActors;

        public void AddActors(IEnumerable<Actor> actors)
        {
            if (_loaded)
            {
                throw new InvalidOperationException("The hierarchy is already loaded.");
            }

            _actorGroups = actors.ToLookup(actor => actor.Properties.ParentId, actor => actor);
            GetOrphanedActors();
            foreach (var actor in _linkedActors)
            {
                FindChildren(actor);
            }

            _actorGroups = null;
            _loaded = true;
        }

        private void GetOrphanedActors()
        {
            _linkedActors = _actorGroups
                .Where(g => g.Key == 0 || !_actorGroups.Contains(g.Key))    // Can actors have parents outside the bank?
                .SelectMany(g => g.AsEnumerable())
                .ToList();
        }

        private void FindChildren(Actor actor)
        {
            if (_actorGroups.Contains(actor.Id))
            {
                foreach (var child in _actorGroups[actor.Id])
                {
                    child.SetParent(actor);
                    FindChildren(child);
                }
            }
        }
    }
}
