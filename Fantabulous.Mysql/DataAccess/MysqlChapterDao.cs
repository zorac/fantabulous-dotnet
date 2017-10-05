using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Types;
using Fantabulous.Mysql.Constants;
using Fantabulous.Mysql.Repositories;

namespace Fantabulous.Mysql.DataAccess
{
    /// <summary>
    /// Chapter data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlChapterDao : IChapterDao
    {
        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new chapter DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlChapterDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<Chapter> ForIdAsync(long id)
        {
            return Mysql.QueryFirstAsync<Chapter>(ChapterSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<Chapter>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<Chapter>(ChapterSql.SelectByIds,
                new { Ids = ids });
        }

        public async Task<Chapter> CreateAsync(
            long workId,
            short position,
            string name)
        {
            try
            {
                var chapter = new Chapter {
                    WorkId = workId,
                    Position = position,
                    Name = name
                };

                chapter.Id = await Mysql.QueryFirstAsync<long>(
                    ChapterSql.Insert, chapter);

                return chapter;
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong
                throw new CreateFailedException("Failed to create chapter", e);
            }
        }
    }
}
