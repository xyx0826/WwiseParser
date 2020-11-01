using System;
using System.Collections.Generic;
using WwiseParserLib.Structures.Objects.HIRC.Structs;

namespace WwiseParserLib.Structures.Objects.HIRC
{
    public class MusicObject : HIRCObjectBase
    {
        /// <summary>
        /// Creates a new Music Object with unknown type and zero length.
        /// </summary>
        private MusicObject() : base(HIRCObjectType.Unknown, 0)
        {

        }

        /// <summary>
        /// Creates a new Music Object with the specified type and data length.
        /// </summary>
        /// <param name="type">The type of the Music Object.</param>
        /// <param name="length">The length of the data excluding type ID and data length.</param>
        public MusicObject(HIRCObjectType type, uint length) : base(type, length)
        {
            // Prevent null relationships
            _children = new List<MusicObject>();
        }

        #region Shared fields
        /// <summary>
        /// <para>Additional properties of the Music Object.</para>
        /// </summary>
        public AudioProperties Properties { get; set; }

        /// <summary>
        /// The count of the Music Object's children.
        /// </summary>
        public uint ChildCount { get; set; }

        /// <summary>
        /// IDs of the Music Object's children.
        /// </summary>
        public uint[] ChildIds { get; set; }

        /// <summary>
        /// <para>The tempo (BPM) of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Tempo</para>
        /// </summary>
        public float Tempo { get; set; }

        /// <summary>
        /// <para>The upper part of the time signature of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureUpper { get; set; }

        /// <summary>
        /// <para>The lower part of the time signature of the Music object.</para>
        /// <para>Located at: Property Editor > General Settings > Time Settings > Time Signature</para>
        /// </summary>
        public byte TimeSignatureLower { get; set; }
        #endregion

        #region Relationship
        /// <summary>
        /// Music Objects that are descendents of the current Music Object.
        /// </summary>
        protected List<MusicObject> _children;

        /// <summary>
        /// Children of the current Music Object.
        /// </summary>
        public IReadOnlyList<MusicObject> Children => _children;

        /// <summary>
        /// The parent of the current Music Object.
        /// </summary>
        public MusicObject Parent { get; private set; }

        /// <summary>
        /// Adds a child to the current Music Object. If successful, <see cref="Parent"/> of the child will be updated.
        /// </summary>
        /// <param name="c">The child.</param>
        public void AddChild(MusicObject c)
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
        /// Sets the parent of the current Music Object. If successful, <see cref="Children"/> of the parent will be updated.
        /// </summary>
        /// <param name="o">The parent Music Object.</param>
        public void SetParent(MusicObject o)
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
