namespace Fantabulous.Core.Requests
{
    /// <summary>
    /// A request to create a user.
    /// </summary>
    /// <inheritDoc/>
    public class CreateUserRequest : Request
    {
        /// <summary>
        /// A user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// An email address.
        /// </summary>
        public string Email { get; set; }
    }
}
