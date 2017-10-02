namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// Abstract class for an entity which has a unique ID.
    /// </summary>
    public abstract class HasId
    {
        /// <summary>
        /// The unique ID of this entity
        /// </summary>
        public long Id { get; set; }
    }
}
