namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A user.
    /// </summary>
    /// <inheritDoc/>
    public class User : HasName
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="id">
        /// The user's unique ID
        /// </param>
        /// <param name="name">
        /// The username
        /// </param>
        public User(long id, string name) : base(id, name)
        {
        }
    }
}
