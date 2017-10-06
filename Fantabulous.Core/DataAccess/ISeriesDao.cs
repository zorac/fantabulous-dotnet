using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for seriess.
    /// </summary>
    public interface ISeriesDao
    {
        /// <summary>
        /// Fetch the series record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A series ID
        /// </param>
        /// <returns>
        /// The series record, or null if none found
        /// </returns>
        Task<Series> ForIdAsync(long id);

        /// <summary>
        /// Fetch the series objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some series IDs
        /// </param>
        /// <returns>
        /// The series objects which were found, in numerical order, empty if
        /// none found
        /// </returns>
        Task<IEnumerable<Series>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the series IDs for a work.
        /// </summary>
        /// <param name="workId">
        /// A work ID
        /// </param>
        /// <returns>
        /// The IDs of the series this work is part of, in numerical order,
        /// empty if none found
        /// </returns>
        Task<IEnumerable<long>> IdsForWorkAsync(long workId);

        /// <summary>
        /// Create a new series.
        /// </summary>
        /// <param name="name">
        /// A series name
        /// </param>
        /// <returns>
        /// The newly-created series
        /// </returns>
        Task<Series> CreateAsync(string name);
    }
}
