using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A parent entity ID and sequence of child entity IDs.
    /// </summary>
    public class ParentChildren<TParent,TChild>
        : Children<TChild>
        where TParent: HasId
        where TChild: HasId
    {
        /// <summary>
        /// The parent entity ID.
        /// </summary>
        public long ParentId { get; set; }
    }
}
