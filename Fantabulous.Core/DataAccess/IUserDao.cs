using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for users.
    /// </summary>
    public interface IUserDao
    {
        /// <summary>
        /// Fetch the user record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A user ID
        /// </param>
        /// <returns>
        /// The user record, or null if none found
        /// </returns>
        Task<User> ForIdAsync(long id);

        /// <summary>
        /// Fetch the user objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some user IDs
        /// </param>
        /// <returns>
        /// The user objects which were found, in numerical order, empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<User>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the user record for a username.
        /// </summary>
        /// <param name="name">
        /// A username
        /// </param>
        /// <returns>
        /// The user record, or null if none found
        /// </returns>
        Task<User> ForNameAsync(string name);

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="username">
        /// The username to use
        /// </param>
        /// <param name="password">
        /// A password to user
        /// </param>
        /// <param name="email">
        /// An emauk address to use
        /// </param>
        /// <returns>
        /// The newly-created user
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// If the user could not be created
        /// </exception>
        Task<User> CreateAsync(string username, string password, string email);

        /// <summary>
        /// Fetch a user via their login credentials.
        /// </summary>
        /// <param name="username">
        /// A login username
        /// </param>
        /// <param name="password">
        /// A login password
        /// </param>
        /// <returns>
        /// The user to log in
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// If the credentials are incorrect
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
