using System.Threading.Tasks;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// Sepcifies a SQL database repository.
    /// </summary>
    public interface ISqlRepository
    {
        /// <summary>
        /// Create a new database connection. This method and its return value
        /// should normally be accesed with "using".
        /// </summary>
        /// <returns>
        /// A database connection object, which must be disposed
        /// </returns>
        Task<ISqlDb> GetDatabaseAsync();
    }
}
