namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A chapter of a work.
    /// </summary>
    /// <inheritDoc/>
    public class Chapter : HasName
    {
        /// <summary>
        /// The unique ID or the work this chapter belongs to.
        /// </summary>
        public long WorkId { get; set; }

        /// <summary>
        /// The chapter number.
        /// </summary>
        public short Position { get; set; }
    }
}
