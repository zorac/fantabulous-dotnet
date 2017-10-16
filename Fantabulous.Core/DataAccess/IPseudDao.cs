using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;

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
        /// The pseud objects which were found, in numerical order; empty if
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
        /// Fetch a pseud record by user and name.
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
        Task<Pseud> ForUserIdAndNameAsync(long userId, string name);

        /// <summary>
        /// Fetch all the pseud IDs for a user.
        /// </summary>
        /// <param name="userId">
        /// A user ID
        /// </param>
        /// <returns>
        /// The pseud IDs for that user, ordered by name; empty if none found
        /// </returns>
        Task<IEnumerable<long>> IdsForUserIdAsync(long userId);

        /// <summary>
        /// Fetch all the pseud IDs for multiple users.
        /// </summary>
        /// <param name="userIds">
        /// Some user IDs
        /// </param>
        /// <returns>
        /// The pseud IDs for those users, ordered by user ID and name; empty
        /// if none found
        /// </returns>
        Task<IEnumerable<ParentChildren<User,Pseud>>> IdsForUserIdsAsync(
            IEnumerable<long> userIds);

        /// <summary>
        /// Fetch the pseud IDs which are the creators of a work.
        /// </summary>
        /// <param name="workId">
        /// A work ID
        /// </param>
        /// <returns>
        /// The pseud IDs which are creators of the work, ordered by position;
        /// empty if none found
        /// </returns>
        Task<IEnumerable<long>> IdsForWorkIdAsync(long workId);

        /// <summary>
        /// Fetch the pseud IDs which are the creators of multiple works.
        /// </summary>
        /// <param name="workIds">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// The pseud IDs which are creators of the works, ordered by work ID
        /// and position; empty if none found
        /// </returns>
        Task<IEnumerable<ParentChildren<Work,Pseud>>> IdsForWorkIdsAsync(
            IEnumerable<long> workIds);

        /// <summary>
        /// Fetch the pseud IDs which are the creators of a series.
        /// </summary>
        /// <param name="seriesId">
        /// A series ID
        /// </param>
        /// <returns>
        /// The pseud IDs which are creators of the series, ordered by
        /// frequency and first appearance; empty if none found
        /// </returns>
        Task<IEnumerable<long>> IdsForSeriesIdAsync(long seriesId);

        /// <summary>
        /// Fetch the pseud IDs which are the creators of multiple series.
        /// </summary>
        /// <param name="seriesIds">
        /// Some series IDs
        /// </param>
        /// <returns>
        /// The pseud IDs which are creators of the series, ordered by
        /// series ID, frequency and first appearance; empty if none found
        /// </returns>
        Task<IEnumerable<ParentChildren<Series,Pseud>>> IdsForSeriesIdsAsync(
            IEnumerable<long> seriesIds);

        /// <summary>
        /// Create a new pseudonym.
        /// </summary>
        /// <param name="userId">
        /// The unique ID of the pseudonym's user
        /// </param>
        /// <param name="name">
        /// A pseudonym
        /// </param>
        /// <returns>
        /// The newly-created pseudonym
        /// </returns>
        Task<Pseud> CreateAsync(long userId, string name);
    }
}
