using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides chapter-related actions.
    /// </summary>
    public interface IChapterService
    {
        /// <summary>
        /// Fetch a chapter by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A chapter ID
        /// </param>
        /// <returns>
        /// The chapter, or null if not found
        /// </returns>
        Task<Chapter> GetChapterAsync(long id);

        /// <summary>
        /// Fetch some chapters by its IDs.
        /// </summary>
        /// <param name="ids">
        /// Some chapter IDs
        /// </param>
        /// <returns>
        /// The chapters which were found, empty if none
        /// </returns>
        Task<IEnumerable<Chapter>> GetChaptersAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a chapter by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A chapter ID
        /// </param>
        /// <returns>
        /// JSON of the chapter, or null if not found
        /// </returns>
        Task<string> GetChapterJsonAsync(long id);

        /// <summary>
        /// Fetch JSON representations of some chapters by its unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some chapter IDs
        /// </param>
        /// <returns>
        /// JSONs of the chapters which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetChapterJsonsAsync(IEnumerable<long> ids);
    }
}
