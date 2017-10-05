using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for works.
    /// </summary>
    public interface IWorkDao
    {
        /// <summary>
        /// Fetch the work record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A work ID
        /// </param>
        /// <returns>
        /// The work record, or null if none found
        /// </returns>
        Task<Work> ForIdAsync(long id);

        /// <summary>
        /// Fetch the work objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// The work objects which were found, in no specific order, empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<Work>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Create a new work.
        /// </summary>
        /// <param name="name">
        /// A work name
        /// </param>
        /// <returns>
        /// The newly-created work
        /// </returns>
        Task<Work> CreateAsync(string name);
    }
}
