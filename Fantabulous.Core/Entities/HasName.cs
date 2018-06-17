namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// Abstract class for an entity which has a name.
    /// </summary>
    /// <inheritDoc/>
    public abstract class HasName : HasId
    {
        /// <summary>
        /// The entity's name
        /// </summary>
        public string Name { get; set; }
    }
}
