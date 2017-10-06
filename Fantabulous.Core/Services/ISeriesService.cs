using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides series-related actions.
    /// </summary>
    public interface ISeriesService
    {
        /// <summary>
        /// Fetch a series by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A series ID
        /// </param>
        /// <returns>
        /// The series, or null if not found
        /// </returns>
        Task<Series> GetSeriesAsync(long id);

        /// <summary>
        /// Fetch some seriess by its IDs.
        /// </summary>
        /// <param name="ids">
        /// Some series IDs
        /// </param>
        /// <returns>
        /// The seriess which were found, empty if none
        /// </returns>
        Task<IEnumerable<Series>> GetSeriesAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a series by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A series ID
        /// </param>
        /// <returns>
        /// JSON of the series, or null if not found
        /// </returns>
        Task<string> GetSeriesJsonAsync(long id);

        /// <summary>
        /// Fetch JSON representations of some seriess by its unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some series IDs
        /// </param>
        /// <returns>
        /// JSONs of the seriess which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetSeriesJsonsAsync(IEnumerable<long> ids);
    }
}
