﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    /// <summary>
    /// A Wwise Actor-Mixer Hierarchy.
    /// </summary>
    public class ActorMixerHierarchy
    {
        /// <summary>
        /// All unconnected objects in the hierarchy.
        /// The key is the parent ID; the values are objects sharing the parent.
        /// </summary>
        private ILookup<uint, SoundObject> _subtrees;

        /// <summary>
        /// Whether the hierarchy is already loaded.
        /// </summary>
        private bool _loaded;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        private IList<SoundObject> _hierarchy;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        public IReadOnlyList<SoundObject> Hierarchy
        {
            get
            {
                if (_hierarchy is List<SoundObject> l)
                {
                    return l.AsReadOnly();
                }
                else
                {
                    return _hierarchy.ToList().AsReadOnly();
                }
            }
        }
        
        /// <summary>
        /// Rebuilds the hierarchy with the specified collection of Sound Objects.
        /// </summary>
        /// <param name="objs">The collection of Sound Objects.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the hierarchy is already loaded.</exception>
        public void LoadSoundObjects(IEnumerable<SoundObject> objs)
        {
            if (_loaded)
            {
                throw new InvalidOperationException("The hierarchy is already loaded.");
            }

            // Group objects by parent IDs
            _subtrees = objs.ToLookup(o => o.Properties.ParentId, o => o);
            _hierarchy = GetTopLevelSoundObjects(_subtrees);
            foreach (var soundObject in _hierarchy)
            {
                LinkChildren(soundObject);
            }

            _subtrees = null;
            _loaded = true;
        }

        /// <summary>
        /// Recursively links children of the specified Sound Object.
        /// </summary>
        /// <param name="o">The Sound Object to link children for.</param>
        private void LinkChildren(SoundObject o)
        {
            if (_subtrees.Contains(o.Id))
            {
                foreach (var child in _subtrees[o.Id])
                {
                    child.SetParent(o);
                    LinkChildren(child);
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
            foreach (var soundObject in _hierarchy)
            {
                // Serialize every top-level object
                SerializeActor(sb, 0, soundObject);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns all top-level Sound Objects or those without a reachable parent.
        /// </summary>
        /// <param name="subtrees">A lookup of parent IDs to child Sound Objects of those IDs.</param>
        /// <returns>A list of all top-level Sound Objects or those without a reachable parent.</returns>
        private static IList<SoundObject> GetTopLevelSoundObjects(ILookup<uint, SoundObject> subtrees)
        {
            if (subtrees == null)
            {
                throw new ArgumentException("The specified subtree lookup can't be null.");
            }

            return subtrees
                .Where(g => g.Key == 0 || !subtrees.Contains(g.Key))    // Top-level or orphan
                .SelectMany(g => g.AsEnumerable())
                .ToList();
        }

        /// <summary>
        /// Serializes the specified object and its children.
        /// </summary>
        /// <param name="sb">The result <see cref="StringBuilder"/>.</param>
        /// <param name="depth">The indentation level.</param>
        /// <param name="o">The object to serialize.</param>
        private static void SerializeActor(StringBuilder sb, int depth, SoundObject o)
        {
            // Serialize current object
            sb.AppendLine(o.Serialize().IndentLines(depth));
            if (o.ChildCount > 0)
            {
                foreach (var child in o.Children)
                {
                    // Serialize every child recursively
                    SerializeActor(sb, depth + 4, child);
                }
            }
        }
    }
}
