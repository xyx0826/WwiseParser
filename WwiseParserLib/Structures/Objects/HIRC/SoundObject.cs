using System;
using System.Collections.Generic;
using System.Diagnostics;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    /// <summary>
    /// A Wwise object in the Actor-Mixer hierarchy.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SoundObject : HIRCObjectBase
    {
        /// <summary>
        /// Creates a new Sound Object with unknown type and zero length.
        /// </summary>
        private SoundObject() : base(HIRCObjectType.Unknown, 0)
        {

        }

        /// <summary>
        /// Creates a new Sound Object with the specified type and data length.
        /// </summary>
        /// <param name="type">The type of the Sound Object.</param>
        /// <param name="length">The length of the data excluding type ID and data length.</param>
        public SoundObject(HIRCObjectType type, uint length) : base(type, length)
        {
            // Prevent null relationships
            _children = new List<SoundObject>();
        }

        #region Shared fields
        /// <summary>
        /// <para>Additional properties of the Sound Object.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// The count of the Sound Object's children.
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// IDs of the Sound Object's children.
        /// </summary>
        public uint[] ChildIds { get; set; }
        #endregion

        #region Relationship
        /// <summary>
        /// Sound Objects that are descendents of the current Sound Object.
        /// </summary>
        protected List<SoundObject> _children;

        /// <summary>
        /// Children of the current Sound Object.
        /// </summary>
        public IReadOnlyList<SoundObject> Children => _children;

        /// <summary>
        /// The parent of the current Sound Object.
        /// </summary>
        public SoundObject Parent { get; private set; }

        /// <summary>
        /// Adds a child to the current Sound Object. If successful, <see cref="Parent"/> of the child will be updated.
        /// </summary>
        /// <param name="c">The child.</param>
        public void AddChild(SoundObject c)
        {
            if (c.Properties.ParentId == Id)
            {
                c.Parent ??= this;
                _children.Add(c);
            }
            else
            {
                throw new ArgumentException("The parent of the specified child isn't the current object.");
            }
        }

        /// <summary>
        /// Sets the parent of the current Sound Object. If successful, <see cref="Children"/> of the parent will be updated.
        /// </summary>
        /// <param name="o">The parent Sound Object.</param>
        public void SetParent(SoundObject o)
        {
            if (o.Id == Properties.ParentId)
            {
                Parent = o;
                o.AddChild(this);
            }
            else
            {
                throw new ArgumentException("The parent of the current object isn't the specified object.");
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
            => Id.ToHex() + ' ' + GetType().Name;

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
