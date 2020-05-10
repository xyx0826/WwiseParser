using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    /// <summary>
    /// Represents a Wwise Actor-Mixer Hierarchy.
    /// </summary>
    public class ActorMixerHierarchy
    {
        /// <summary>
        /// All unconnected objects in the hierarchy.
        /// The key is the parent ID.
        /// </summary>
        private ILookup<uint, Actor> _actorGroups;

        /// <summary>
        /// Whether the hierarchy is already loaded.
        /// </summary>
        private bool _loaded;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        private List<Actor> _linkedActors;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        public IReadOnlyCollection<Actor> Actors => _linkedActors;
        
        /// <summary>
        /// Rebuilds the hierarchy with the specified collection of actor objects.
        /// </summary>
        /// <param name="actors">The collection of actors.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the hierarchy is already loaded.</exception>
        public void AddActors(IEnumerable<Actor> actors)
        {
            if (_loaded)
            {
                throw new InvalidOperationException("The hierarchy is already loaded.");
            }

            // Group objects by parent IDs
            _actorGroups = actors.ToLookup(actor => actor.Properties.ParentId, actor => actor);
            GetOrphanedActors();
            foreach (var actor in _linkedActors)
            {
                FindChildren(actor);
            }

            _actorGroups = null;
            _loaded = true;
        }

        /// <summary>
        /// Finds all objects without parent or with an unreachable parent ID.
        /// Presumably all actor objects have parents inside the same bank;
        /// the second case is here for when it's not.
        /// </summary>
        private void GetOrphanedActors()
        {
            _linkedActors = _actorGroups
                .Where(g => g.Key == 0 || !_actorGroups.Contains(g.Key))    // Can actors have parents outside the bank?
                .SelectMany(g => g.AsEnumerable())
                .ToList();
        }

        /// <summary>
        /// Finds children of the specified object in the current hierarchy.
        /// </summary>
        /// <param name="actor">The object to find children for.</param>
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

        /// <summary>
        /// Returns the string representation of the hierarchy.
        /// </summary>
        /// <returns>The string representation of the hierarchy.</returns>
        public string Serialize()
        {
            var sb = new StringBuilder();
            foreach (var actor in _linkedActors)
            {
                // Serialize every top-level object
                SerializeActor(sb, 0, actor);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Serializes the specified object and its children.
        /// </summary>
        /// <param name="sb">The result <see cref="StringBuilder"/>.</param>
        /// <param name="depth">The indentation level.</param>
        /// <param name="actor">The object to serialize.</param>
        private void SerializeActor(StringBuilder sb, int depth, Actor actor)
        {
            // Serialize current object
            sb.AppendLine(actor.Serialize().IndentLines(depth));
            if (actor.ChildCount > 0)
            {
                foreach (var child in actor.Children)
                {
                    // Serialize every child recursively
                    SerializeActor(sb, depth + 4, child);
                }
            }
        }
    }
}
