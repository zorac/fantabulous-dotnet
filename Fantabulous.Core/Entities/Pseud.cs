namespace Fantabulous.Core.Entities
{
    /// <summary>
    /// A pseudonym for a user.
    /// </summary>
    /// <inheritDoc/>
    public class Pseud : HasName
    {
        /// <summary>
        /// The unique ID of the user this pseud belongs to.
        /// </summary>
        public long UserId { get; set; }
    }
}
