namespace Fantabulous.Core.Entities
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
        public string Name { get; set; }
    }
}
