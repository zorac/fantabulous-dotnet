using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Models;

namespace Fantabulous.Mysql.DataAccess
{
    /// <summary>
    /// User data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlUserDao : IUserDao
    {
        private const string SELECT = @"
            SELECT  user_id AS Id,
                    name AS Name
            FROM    users";
        private const string SELECT_BY_ID = SELECT + @"
            WHERE   user_id = @Id";
        private const string SELECT_BY_IDS = SELECT + @"
            WHERE   user_id IN @Ids";
        private const string SELECT_BY_NAME = SELECT + @"
            WHERE   name = @Name";

        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new user DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlUserDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<User> ForIdAsync(long id)
        {
            return Mysql.QueryFirstOrDefaultAsync<User>(
                SELECT_BY_ID, new { Id = id });
        }

        public Task<IEnumerable<User>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<User>(
                SELECT_BY_IDS, new { Ids = ids });
        }

        public Task<User> ForNameAsync(string name)
        {
            return Mysql.QueryFirstOrDefaultAsync<User>(
                SELECT_BY_NAME, new { Name = name });
        }
    }
}
