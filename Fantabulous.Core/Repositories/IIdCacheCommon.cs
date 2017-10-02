using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// A cache which retrieves objects using unique IDs.
    /// </summary>
    public interface IIdCacheCommon<T> where T: HasId
    {
        /// <summary>
        /// Fetch the cached object for a given ID.
        /// </summary>
        /// <param name="id">
        /// A unique ID
        /// </param>
        /// <returns>
        /// The cached object, or null if none found
        /// </returns>
        Task<T> GetAsync(long id);

        /// <summary>
        /// Fetch the cached objects for a sequence of unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some unique IDs
        /// </param>
        /// <returns>
        /// The cached objects, in the same order as their input IDs; any not
        /// found will be returned as nulls
        /// </returns>
        Task<IEnumerable<T>> GetAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch a JSON representation of the cached object for a given ID.
        /// </summary>
        /// <param name="id">
        /// A unique ID
        /// </param>
        /// <returns>
        /// JSON of the cached object, or null if not found
        /// </returns>
        Task<string> GetJsonAsync(long id);

        /// <summary>
        /// Fetch JSON representations of the cached objects for a sequence of
        /// unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some unique IDs
        /// </param>
        /// <returns>
        /// JSON of the cached objects, in the same order as their input IDs;
        /// any not found will be returned as nulls
        /// </returns>
        Task<IEnumerable<string>> GetJsonAsync(IEnumerable<long> ids);

        /// <summary>
        /// Set/update the cached value for a unique ID.
        /// </summary>
        /// <param name="value">
        /// The object to store
        /// </param>
        /// <returns>
        /// A JSON representation of the object
        /// </returns>
        string SetInBackground(T value);
    }
}
