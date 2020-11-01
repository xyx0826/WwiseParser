using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Structures.Hierarchies
{
    /// <summary>
    /// A Wwise Interactive Music Hierarchy.
    /// </summary>
    public class InteractiveMusicHierarchy
    {
        /// <summary>
        /// All unconnected objects in the hierarchy.
        /// The key is the parent ID; the values are objects sharing the parent.
        /// </summary>
        private ILookup<uint, MusicObject> _subtrees;

        /// <summary>
        /// Whether the hierarchy is already loaded.
        /// </summary>
        private bool _loaded;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        private IList<MusicObject> _hierarchy;

        /// <summary>
        /// All connected objects in the hierarchy.
        /// </summary>
        public IReadOnlyList<MusicObject> Hierarchy
        {
            get
            {
                if (_hierarchy is List<MusicObject> l)
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
        /// Rebuilds the hierarchy with the specified collection of Music Objects.
        /// </summary>
        /// <param name="objs">The collection of Music Objects.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the hierarchy is already loaded.</exception>
        public void LoadMusicObjects(IEnumerable<MusicObject> objs)
        {
            if (_loaded)
            {
                throw new InvalidOperationException("The hierarchy is already loaded.");
            }

            // Group objects by parent IDs
            _subtrees = objs.ToLookup(o => o.Properties.ParentId, o => o);
            _hierarchy = GetTopLevelMusicObjects(_subtrees);
            foreach (var MusicObject in _hierarchy)
            {
                LinkChildren(MusicObject);
            }

            _subtrees = null;
            _loaded = true;
        }

        /// <summary>
        /// Recursively links children of the specified Music Object.
        /// </summary>
        /// <param name="o">The Music Object to link children for.</param>
        private void LinkChildren(MusicObject o)
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
            foreach (var MusicObject in _hierarchy)
            {
                // Serialize every top-level object
                SerializeActor(sb, 0, MusicObject);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns all top-level Music Objects or those without a reachable parent.
        /// </summary>
        /// <param name="subtrees">A lookup of parent IDs to child Music Objects of those IDs.</param>
        /// <returns>A list of all top-level Music Objects or those without a reachable parent.</returns>
        private static IList<MusicObject> GetTopLevelMusicObjects(ILookup<uint, MusicObject> subtrees)
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
        private static void SerializeActor(StringBuilder sb, int depth, MusicObject o)
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
