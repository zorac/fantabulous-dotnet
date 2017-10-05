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
    /// Pseudonym data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlPseudDao : IPseudDao
    {
        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new pseud DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlPseudDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<Pseud> ForIdAsync(long id)
        {
            return Mysql.QueryFirstAsync<Pseud>(PseudSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<Pseud>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<Pseud>(PseudSql.SelectByIds,
                new { Ids = ids });
        }

        public Task<Pseud> DefaultForUserAsync(User user)
        {
            return ForUserAndNameAsync(user.Id, user.Name);
            // TODO allow other defaults!
        }

        public Task<Pseud> ForUserAndNameAsync(long userId, string name)
        {
            return Mysql.QueryFirstAsync<Pseud>(PseudSql.SelectByUserAndName,
                new { UserId = userId, Name = name });
        }

        public async Task<Pseud> CreateAsync(long userId, string name)
        {
            try
            {
                var pseud = new Pseud
                {
                    UserId = userId,
                    Name = name
                };

                pseud.Id = await Mysql.QueryFirstAsync<long>(PseudSql.Insert,
                    pseud);

                return pseud;
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong, not this exception
                throw new CreateFailedException("Failed to create pseud", e);
            }
        }
    }
}
