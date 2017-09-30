namespace Fantabulous.Core.Models
{
    /// <summary>
    /// Abstract class for an entity which has a name.
    /// </summary>
    public abstract class HasName : HasId
    {
        /// <summary>
        /// The entity's name
        /// </summary>
        /// <inheritDoc/>
        public string Name { get; }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <param name="id">
        /// The entity's unique ID
        /// </param>
        /// <param name="name">
        /// The entity's name
        /// </param>
        public HasName(long id, string name) : base(id)
        {
            Name = name;
        }
    }
}
