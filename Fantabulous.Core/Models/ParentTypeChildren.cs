using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A parent entity ID, type, and sequence of child entity IDs.
    /// </summary>
    public class ParentTypeChildren<TParent,TType,TChild>
        : TypeChildren<TType,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        /// <summary>
        /// The parent entity ID.
        /// </summary>
        public long ParentId { get; set; }
    }
}
