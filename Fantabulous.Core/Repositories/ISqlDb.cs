using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// Wrapper for a SQL database connection. This object MUST be disposed
    /// after use.
    /// </summary>
    /// <inheritDoc/>
    public interface ISqlDb : IDisposable
    {
        /// <summary>
        /// A data-access object for users tied to this connection.
        /// </summary>
        IUserDao Users { get; }

        /// <summary>
        /// Execute a SQL query and return the first result.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute
        /// </param>
        /// <param name="param">
        /// Parameters to bind to the query
        /// </param>
        /// <returns>
        /// An object of the specified type, or null if the query returned no
        /// results
        /// </returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param);

        /// <summary>
        /// Execute a SQL query and return the results.
        /// </summary>
        /// <param name="sql">
        /// The SQL query to execute
        /// </param>
        /// <param name="param">
        /// Parameters to bind to the query
        /// </param>
        /// <returns>
        /// A sequence of objects of the specified type, empty if the query
        /// returned no results
        /// </returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);

        /// <summary>
        /// Begin a database transaction.
        /// </summary>
        Task BeginAsync();

        /// <summary>
        /// Commit the current database transction.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Roll back the current database transaction.
        /// </summary>
        Task RollbackAsync();
    }
}
