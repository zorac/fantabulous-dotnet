using System.Threading.Tasks;

using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides authentication-related actions.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Creare a new user.
        /// </summary>
        /// <param name="username">
        /// A username to log in as.
        /// </param>
        /// <param name="password">
        /// A password to log on with.
        /// </param>
        /// <returns>
        /// The user object.
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// Thrown if the user could not be created
        /// </exception>
        Task<User> CreateUserAsync(string username, string password);

        /// <summary>
        /// Log in a user.
        /// </summary>
        /// <param name="username">
        /// A username to log in as.
        /// </param>
        /// <param name="password">
        /// A password to log on with.
        /// </param>
        /// <returns>
        /// The user object.
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// Thrown if the username or password was incorrect
        /// </exception>
        Task<User> LoginAsync(string username, string password);

        /// <summary>
        /// Change a user's password
        /// </summary>
        /// <param name="id">
        /// A user ID
        /// </param>
        /// <param name="oldPassword">
        /// The user's current password
        /// </param>
        /// <param name="newPassword">
        /// A new password
        /// </param>
        /// <exception cref="AuthenticationException">
        /// Thrown if the password change failed
        /// </exception>
        Task ChangePasswordAsync(
            long id,
            string oldPassword,
            string newPassword);
    }
}
