using System.Collections.Generic;
using System.Diagnostics;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Actor : HIRCObjectBase
    {
        private Actor() : base(HIRCObjectType.Unknown, 0)
        {

        }

        public Actor(HIRCObjectType type, uint length) : base(type, length)
        {

        }

        /// <summary>
        /// <para>Additional properties of the Actor.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        public uint ChildCount { get; set; }

        public uint[] ChildIds { get; set; }

        private List<Actor> _children;

        public IReadOnlyCollection<Actor> Children => _children;

        public Actor Parent { get; private set; }

        private string DebuggerDisplay
        {
            get => $"{GetType().Name}, {ChildCount} children";
        }

        /// <summary>
        /// Adds a child to the current actor. If successful, the <see cref="Parent"/> of the child actor will be updated.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public void AddChild(Actor actor)
        {
            _children ??= new List<Actor>();
            if (actor.Properties.ParentId == Id)
            {
                actor.Parent ??= this;
                _children.Add(actor);
            }
        }

        /// <summary>
        /// Sets the parent of the current actor. If successful, the <see cref="Children"/> of the parent actor will be updated.
        /// </summary>
        /// <param name="actor">The parent actor.</param>
        public void SetParent(Actor actor)
        {
            if (actor.Id == Properties.ParentId)
            {
                Parent = actor;
                actor.AddChild(this);
            }
        }

        /// <summary>
        /// Serializes the Actor in the format of its hexadecimal ID
        /// appended by a whitespace and its type name.
        /// </summary>
        /// <returns>The serialized representation.</returns>
        public virtual string Serialize()
        {
            return Id.ToHex() + ' ' + this.GetType().Name;
        }
    }
}
