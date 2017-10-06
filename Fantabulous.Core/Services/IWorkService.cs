using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides work-related actions.
    /// </summary>
    public interface IWorkService
    {
        /// <summary>
        /// Fetch a work by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A work ID
        /// </param>
        /// <returns>
        /// The work, or null if not found
        /// </returns>
        Task<Work> GetWorkAsync(long id);

        /// <summary>
        /// Fetch some works by its IDs.
        /// </summary>
        /// <param name="ids">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// The works which were found, empty if none
        /// </returns>
        Task<IEnumerable<Work>> GetWorksAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a work by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A work ID
        /// </param>
        /// <returns>
        /// JSON of the work, or null if not found
        /// </returns>
        Task<string> GetWorkJsonAsync(long id);

        /// <summary>
        /// Fetch JSON representations of some works by its unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some work IDs
        /// </param>
        /// <returns>
        /// JSONs of the works which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetWorkJsonsAsync(IEnumerable<long> ids);
    }
}
