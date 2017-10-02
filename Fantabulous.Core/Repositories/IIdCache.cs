using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// A cache which stores and retrieves objects using unique IDs.
    /// </summary>
    /// <inheritDoc/>
    public interface IIdCache<T> : IIdCacheCommon<T> where T: HasId
    {
        /// <summary>
        /// Set/update the cached value for a unique ID.
        /// </summary>
        /// <param name="id">
        /// A unique ID
        /// </param>
        /// <param name="json">
        /// A JSON representation of the object
        /// </param>
        void SetInBackground(long id, string json);
    }
}
