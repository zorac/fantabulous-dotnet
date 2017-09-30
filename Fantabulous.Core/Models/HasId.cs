namespace Fantabulous.Core.Models
{
    /// <summary>
    /// Abstract class for an entity which has a unique ID.
    /// </summary>
    public abstract class HasId
    {
        /// <summary>
        /// The unique ID of this entity
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <param name="id">
        /// The entity's unique ID
        /// </param>
        public HasId(long id)
        {
            Id = id;
        }
    }
}
