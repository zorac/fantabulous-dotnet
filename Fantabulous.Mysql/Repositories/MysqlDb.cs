using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Dapper;
using MySql.Data.MySqlClient;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Repositories;
using Fantabulous.Mysql.DataAccess;

namespace Fantabulous.Mysql.Repositories
{
    /// <summary>
    /// Wrapper around a MySQL database connection. This will close the
    /// underlying connection on disposal (including rolling back any
    /// uncommitted transaction).
    /// </summary>
    public class MysqlDb : ISqlDb
    {
        // TODO maybe lazy-initialise these?
        public IUserDao Users => new MysqlUserDao(this);
        public IPseudDao Pseuds => new MysqlPseudDao(this);

        private readonly MySqlConnection Connection;

        private MySqlTransaction Transaction;

        /// <summary>
        /// Create a new database connection wrapper.
        /// </summary>
        /// <param name="connection">
        /// The connection to wrap; must already be opened.
        /// </param>
        internal MysqlDb(MySqlConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Transaction?.Rollback();
            Connection?.Close();
            Connection?.Dispose();
        }

        public Task<int> ExecuteAsync(
            string sql,
            object param = null)
        {
            return Connection.ExecuteAsync(sql, param, Transaction);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null)
        {
            return Connection.QueryAsync<T>(sql, param, Transaction);
        }

        public Task<T> QueryFirstAsync<T>(
            string sql,
            object param = null)
        {
            return Connection.QueryFirstOrDefaultAsync<T>(sql, param,
                Transaction);
        }

        public async Task BeginAsync()
        {
            if (Transaction != null) throw new InvalidOperationException(
                "A transaction is already in progress");
            Transaction = await Connection.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (Transaction == null) throw new InvalidOperationException(
                "No transaction in progress");
            await Transaction.CommitAsync();
            Transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (Transaction == null) throw new InvalidOperationException(
                "No transaction in progress");
            await Transaction.RollbackAsync();
            Transaction = null;
        }
    }
}
