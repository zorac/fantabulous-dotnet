using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

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
        /// The chapter objects which were found, in no specific order, empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<Chapter>> ForIdsAsync(IEnumerable<long> ids);

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
