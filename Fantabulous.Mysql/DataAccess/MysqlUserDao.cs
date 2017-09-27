using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Models;

namespace Fantabulous.Mysql.DataAccess
{
    public class MysqlUserDao : IUserDao
    {
        private const string SELECT = @"
            SELECT  user_id,
                    name
            FROM    users";
        private const string SELECT_BY_ID = SELECT + @"
            WHERE   user_id = @id";
        private const string SELECT_BY_IDS = SELECT + @"
            WHERE   user_id IN(@ids)";
        private const string SELECT_BY_NAME = SELECT + @"
            WHERE   name = @name";

        private readonly MysqlDb Mysql;

        internal MysqlUserDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public async Task<User> ForIdAsync(long id)
        {
            var command = Mysql.Command(SELECT_BY_ID) as MySqlCommand;

            command.BindId(id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                return await ReadAsync(reader);
            }
        }

        public async Task<IEnumerable<User>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            var command = Mysql.Command(SELECT_BY_IDS) as MySqlCommand;

            command.BindIds(ids);

            using (var reader = await command.ExecuteReaderAsync())
            {
                var users = new List<User>();
                User user;

                while ((user = await ReadAsync(reader)) != null)
                {
                    users.Add(user);
                }

                return users;
            }
        }

        public async Task<User> ForNameAsync(string name)
        {
            var command = Mysql.Command(SELECT_BY_NAME) as MySqlCommand;

            command.BindName(name);

            using (var reader = await command.ExecuteReaderAsync())
            {
                return await ReadAsync(reader);
            }
        }

        private async Task<User> ReadAsync(DbDataReader reader)
        {
            if (await reader.ReadAsync())
            {
                return new User(reader.GetInt64(0), reader.GetString(1));
            }
            else
            {
                return null;
            }
        }
    }
}
