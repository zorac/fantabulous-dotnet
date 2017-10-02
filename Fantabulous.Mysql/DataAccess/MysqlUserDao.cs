using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Mysql.Constants;
using Fantabulous.Mysql.Repositories;

namespace Fantabulous.Mysql.DataAccess
{
    /// <summary>
    /// User data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlUserDao : IUserDao
    {
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
            return Mysql.QueryFirstAsync<User>(UserSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<User>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<User>(UserSql.SelectByIds,
                new { Ids = ids });
        }

        public Task<User> ForNameAsync(string name)
        {
            return Mysql.QueryFirstAsync<User>(UserSql.SelectByName,
                new { Name = name });
        }

        public async Task<User> CreateAsync(
            string username,
            string email,
            string password)
        {
            try
            {
                var id = await Mysql.QueryFirstAsync<long>(UserSql.Insert, new
                    {
                        Username = username,
                        Email = email,
                        Password = password
                    });

                return new User { Id = id, Name = username };
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong
                throw new AuthenticationException("Failed to create user", e);
            }
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            // TODO allow login by email address
            var user = await Mysql.QueryFirstAsync<User>(UserSql.LoginByUsername, new
                {
                    Username = username,
                    Password = password
                });

            if (user != null) return user;
            throw new AuthenticationException("Login failed");
        }

        public async Task ChangePasswordAsync(
            long id,
            string oldPassword,
            string newPassword)
        {
            var rows = await Mysql.ExecuteAsync(UserSql.UpdatePassword, new
                {
                    Id = id,
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                });

            if (rows != 1) throw new AuthenticationException(
                "Failed to change password");
        }
    }
}
