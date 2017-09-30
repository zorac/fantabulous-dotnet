using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

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
        /// The user objects which were found, in no specific order, empty if
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
    }
}
