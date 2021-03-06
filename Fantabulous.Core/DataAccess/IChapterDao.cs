using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for chapters.
    /// </summary>
    public interface IChapterDao
    {
        /// <summary>
        /// Fetch the chapter record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A chapter ID
        /// </param>
        /// <returns>
        /// The chapter record, or null if none found
        /// </returns>
        Task<Chapter> ForIdAsync(long id);

        /// <summary>
        /// Fetch the chapter objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some chapter IDs
        /// </param>
        /// <returns>
        /// The chapter objects which were found, in numerical order; empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<Chapter>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the chapter IDs for a work.
        /// </summary>
        /// <param name="workId">
        /// A work ID
        /// </param>
        /// <returns>
        /// The IDs of the work's chapters, ordered by position; empty if none
        /// found
        /// </returns>
        Task<IEnumerable<long>> IdsForWorkIdAsync(long workId);

        /// <summary>
        /// Fetch the chapter IDs for multiple works.
        /// </summary>
        /// <param name="workIds">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// The IDs of the works' chapters, ordered by work ID and position;
        /// empty if none found
        /// </returns>
        Task<IEnumerable<ParentChildren<Work,Chapter>>> IdsForWorkIdsAsync(
            IEnumerable<long> workIds);

        /// <summary>
        /// Create a new chapter.
        /// </summary>
        /// <param name="workId">
        /// The unique ID of the work this chapter belongs to
        /// </param>
        /// <param name="position">
        /// The position of this chapter withing the work
        /// </param>
        /// <param name="name">
        /// A chapter name
        /// </param>
        /// <returns>
        /// The newly-created chapter
        /// </returns>
        Task<Chapter> CreateAsync(long workId, short position, string name);
    }
}
