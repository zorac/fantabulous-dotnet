namespace Fantabulous.Core.Requests
{
    /// <summary>
    /// A request to change a password.
    /// </summary>
    /// <inheritDoc/>
    public class ChangePasswordRequest : Request
    {
        /// <summary>
        /// A user ID.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// The user's current password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// A new password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
