using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for pseudonyms.
    /// </summary>
    public interface IPseudDao
    {
        /// <summary>
        /// Fetch the pseud record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A pseud ID
        /// </param>
        /// <returns>
        /// The pseud record, or null if none found
        /// </returns>
        Task<Pseud> ForIdAsync(long id);

        /// <summary>
        /// Fetch the pseud objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some pseud IDs
        /// </param>
        /// <returns>
        /// The pseud objects which were found, in no specific order, empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<Pseud>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the default pseud for a user.
        /// </summary>
        /// <param name="user">
        /// A user
        /// </param>
        /// <returns>
        /// The pseud record, or null if not found
        /// </returns>
        Task<Pseud> DefaultForUserAsync(User user);

        /// <summary>
        /// Fetch a pseud record by name.
        /// </summary>
        /// <param name="userId">
        /// A user ID
        /// </param>
        /// <param name="name">
        /// A psedonym
        /// </param>
        /// <returns>
        /// The pseud record, or null if none found
        /// </returns>
        Task<Pseud> ForUserAndNameAsync(long userId, string name);

        /// <summary>
        /// Create a new pseudonym.
        /// </summary>
        /// <param name="userId">
        /// The unique ID of the pseud's user
        /// </param>
        /// <param name="name">
        /// A pseudonym
        /// </param>
        /// <returns>
        /// The newly-created pseud
        /// </returns>
        Task<Pseud> CreateAsync(long userId, string name);
    }
}
