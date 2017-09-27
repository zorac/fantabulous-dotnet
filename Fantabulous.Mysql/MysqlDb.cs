using System;
using System.Data.Common;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Repositories;
using Fantabulous.Mysql.DataAccess;

namespace Fantabulous.Mysql
{
    public class MysqlDb : ISqlDb
    {
        // TODO maybe lazy-initialise these?
        public IUserDao Users => new MysqlUserDao(this);

        private readonly MySqlConnection Connection;
        private MySqlTransaction Transaction;

        internal MysqlDb(MySqlConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection?.Close();
        }

        public async Task BeginAsync()
        {
            if (Transaction != null) throw new InvalidOperationException(
                "A transaction is already in progress");
            Transaction = await Connection.BeginTransactionAsync();
        }

        public DbCommand Command(string sql)
        {
            return new MySqlCommand(sql, Connection, Transaction);
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
