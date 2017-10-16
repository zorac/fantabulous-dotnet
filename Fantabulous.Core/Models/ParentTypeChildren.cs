using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A parent entity ID, type, and sequence of child entity IDs.
    /// </summary>
    public class ParentTypeChildren<TParent,TType,TChild>
        : ParentChildren<TParent,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        /// <summary>
        /// The type.
        /// </summary>
        public TType Type { get; set; }
    }
}
