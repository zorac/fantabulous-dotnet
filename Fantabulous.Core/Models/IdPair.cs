using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A pair of entity IDs.
    /// </summary>
    public class IdPair<TParent,TChild>
        where TParent: HasId
        where TChild: HasId
    {
        /// <summary>
        /// The parent entity ID.
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// The child entity ID.
        /// </summary>
        public long ChildId { get; set; }
    }
}
