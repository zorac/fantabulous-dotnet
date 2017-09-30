using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// A cache which stores and retrieves objects using both unique IDs and
    /// names.
    /// </summary>
    /// <inheritDoc/>
    public interface IIdNameCache<T> : IIdCacheCommon<T>  where T: HasName
    {
        /// <summary>
        /// Fetch the cached object for a given name.
        /// </summary>
        /// <param name="name">
        /// A name
        /// </param>
        /// <returns>
        /// The cached object, or null if none found
        /// </returns>
        Task<T> GetAsync(string name);

        /// <summary>
        /// Fetch a JSON representation of the cached object for a given name.
        /// </summary>
        /// <param name="name">
        /// A name
        /// </param>
        /// <returns>
        /// JSON of the cached object, or null if not found
        /// </returns>
        Task<string> GetJsonAsync(string name);

        /// <summary>
        /// Set/update the cached value for a unique ID and name.
        /// </summary>
        /// <param name="id">
        /// A unique ID
        /// </param>
        /// <param name="name">
        /// A name
        /// </param>
        /// <param name="json">
        /// A JSON representation of the object
        /// </param>
        void SetInBackground(long id, string name, string json);
    }
}
