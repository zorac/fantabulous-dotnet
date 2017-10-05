namespace Fantabulous.Core.Requests
{
    /// <summary>
    /// A request to log in.
    /// </summary>
    /// <inheritDoc/>
    public class LoginRequest : Request
    {
        /// <summary>
        /// A user name or email address.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// A password.
        /// </summary>
        public string Password { get; set; }
    }
}
