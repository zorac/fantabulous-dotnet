using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;
using Fantabulous.Core.Types;

namespace Fantabulous.Core.DataAccess
{
    /// <summary>
    /// A data-access object for tags.
    /// </summary>
    public interface ITagDao
    {
        /// <summary>
        /// Fetch the tag record for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A tag ID
        /// </param>
        /// <returns>
        /// The tag record, or null if none found
        /// </returns>
        Task<Tag> ForIdAsync(long id);

        /// <summary>
        /// Fetch the tag objects for multiple unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some tag IDs
        /// </param>
        /// <returns>
        /// The tag objects which were found, in numerical order; empty if
        /// none were found
        /// </returns>
        Task<IEnumerable<Tag>> ForIdsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the tag types and IDs for a work.
        /// </summary>
        /// <param name="workId">
        /// A work ID
        /// </param>
        /// <returns>
        /// The types and IDs of the tags attached to the work, ordered by
        /// position; empty if none found
        /// </returns>
        Task<IEnumerable<TypeChildren<TagType,Tag>>> TypesAndIdsForWorkIdAsync(
            long workId);

        /// <summary>
        /// Fetch the tag types and IDs for multiple works.
        /// </summary>
        /// <param name="workIds">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// The IDs of the works and types and IDs of their tags, ordered by
        /// work ID and position; empty if none found
        /// </returns>
        Task<IEnumerable<ParentTypeChildren<Work,TagType,Tag>>> TypesAndIdsForWorkIdsAsync(
            IEnumerable<long> workIds);

        /// <summary>
        /// Fetch the tag types and IDs for a series.
        /// </summary>
        /// <param name="seriesId">
        /// A series ID
        /// </param>
        /// <returns>
        /// The types and IDs of the tags attached to the works in the series,
        /// ordered by type, frequency and first appearance; empty if none found
        /// </returns>
        Task<IEnumerable<TypeChildren<TagType,Tag>>> TypesAndIdsForSeriesIdAsync(
            long seriesId);

        /// <summary>
        /// Fetch the tag types and IDs for multiple series.
        /// </summary>
        /// <param name="seriesIds">
        /// Some series IDs
        /// </param>
        /// <returns>
        /// The IDs of the series and types and IDs of thir tags, ordered by
        /// series ID, type, frequency and first appearance; empty if none found
        /// </returns>
        Task<IEnumerable<ParentTypeChildren<Series,TagType,Tag>>> TypesAndIdsForSeriesIdsAsync(
            IEnumerable<long> seriesIds);

        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="type">
        /// A tag type
        /// </param>
        /// <param name="aliasFor">
        /// The unique ID of the tag this is an alias for, or 0 for canonical.
        /// </param>
        /// <param name="name">
        /// A tag name
        /// </param>
        /// <returns>
        /// The newly-created tag
        /// </returns>
        Task<Tag> CreateAsync(TagType type, long aliasFor, string name);
    }
}
