using System.Collections.Generic;
using System.Diagnostics;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    /// <summary>
    /// A Wwise object in the Actor-Mixer or Interactive Music hierarchy.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Actor : HIRCObjectBase
    {
        /// <summary>
        /// Creates a new Actor with unknown type and zero length.
        /// </summary>
        private Actor() : base(HIRCObjectType.Unknown, 0)
        {

        }

        /// <summary>
        /// Creates a new Actor with the specified type and data length.
        /// </summary>
        /// <param name="type">The type of the actor object.</param>
        /// <param name="length">The length of the data describing the actor object.</param>
        public Actor(HIRCObjectType type, uint length) : base(type, length)
        {
            // Prevent null relationships
            _children = new List<Actor>();
        }

        #region Shared fields
        /// <summary>
        /// <para>Additional properties of the Actor.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// The count of the Actor's children.
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// IDs of the Actor's children.
        /// </summary>
        public uint[] ChildIds { get; set; }
        #endregion

        #region Relationship
        /// <summary>
        /// Children actor objects of the current actor object.
        /// </summary>
        private List<Actor> _children;

        /// <summary>
        /// Children actor objects of the current actor object.
        /// </summary>
        public IReadOnlyCollection<Actor> Children => _children;

        /// <summary>
        /// The parent actor object of the current actor object.
        /// </summary>
        public Actor Parent { get; private set; }

        /// <summary>
        /// Adds a child to the current actor. If successful, the <see cref="Parent"/> of the child actor will be updated.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public void AddChild(Actor actor)
        {
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
        #endregion

        #region Representation
        /// <summary>
        /// Returns a string summarizing the object. The result could be multi-line.
        /// Use <see cref="ToString"/> for a more concise representation.
        /// </summary>
        /// <returns>A string summarizing the object.</returns>
        public virtual string Serialize()
            => ToString();

        /// <summary>
        /// Returns a string representation of the object's Wwise ID and type.
        /// </summary>
        public override string ToString()
            => Id.ToHex() + ' ' + this.GetType().Name;

        /// <summary>
        /// Returns the string representation of the object and its child count.
        /// Used for IDE debugger display.
        /// </summary>
        private string DebuggerDisplay
            => ToString() + ", " + ChildCount
            + (ChildCount > 1 ? " children" : " child");
        #endregion
    }
}
