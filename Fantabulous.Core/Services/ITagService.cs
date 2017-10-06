using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides tag-related actions.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Fetch a tag by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A tag ID
        /// </param>
        /// <returns>
        /// The tag, or null if not found
        /// </returns>
        Task<Tag> GetTagAsync(long id);

        /// <summary>
        /// Fetch some tags by its IDs.
        /// </summary>
        /// <param name="ids">
        /// Some tag IDs
        /// </param>
        /// <returns>
        /// The tags which were found, empty if none
        /// </returns>
        Task<IEnumerable<Tag>> GetTagsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a tag by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A tag ID
        /// </param>
        /// <returns>
        /// JSON of the tag, or null if not found
        /// </returns>
        Task<string> GetTagJsonAsync(long id);

        /// <summary>
        /// Fetch JSON representations of some tags by its unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some tag IDs
        /// </param>
        /// <returns>
        /// JSONs of the tags which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetTagJsonsAsync(IEnumerable<long> ids);
    }
}
