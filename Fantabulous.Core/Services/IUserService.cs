using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A servcie which provides user-related actions.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Fetch a user by their unique ID.
        /// </summary>
        /// <param name="id">
        /// A user ID
        /// </param>
        /// <returns>
        /// The user, or null if not found
        /// </returns>
        Task<User> GetUserAsync(long id);

        /// <summary>
        /// Fetch a user by their username.
        /// </summary>
        /// <param name="name">
        /// A username
        /// </param>
        /// <returns>
        /// The user, or null if not found
        /// </returns>
        Task<User> GetUserAsync(string name);

        /// <summary>
        /// Fetch some users by their IDs.
        /// </summary>
        /// <param name="ids">
        /// Some user IDs
        /// </param>
        /// <returns>
        /// The users which were found, empty if none
        /// </returns>
        Task<IEnumerable<User>> GetUsersAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a user by their unique ID.
        /// </summary>
        /// <param name="id">
        /// A user ID
        /// </param>
        /// <returns>
        /// JSON of the user, or null if not found
        /// </returns>
        Task<string> GetUserJsonAsync(long id);

        /// <summary>
        /// Fetch the JSON representation of a user by their username.
        /// </summary>
        /// <param name="name">
        /// A username
        /// </param>
        /// <returns>
        /// JSON of the user, or null if not found
        /// </returns>
        Task<string> GetUserJsonAsync(string name);

        /// <summary>
        /// Fetch JSON representations of some users by their unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some user IDs
        /// </param>
        /// <returns>
        /// JSONS of the users which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetUsersJsonAsync(IEnumerable<long> ids);
    }
}
