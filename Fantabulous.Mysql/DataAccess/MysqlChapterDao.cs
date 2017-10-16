using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;
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

        public Task<IEnumerable<long>> IdsForWorkIdAsync(long workId)
        {
            return Mysql.QueryAsync<long>(ChapterSql.SelectIdsByWorkId,
                new { WorkId = workId });
        }

        public Task<IEnumerable<ParentChildren<Work,Chapter>>> IdsForWorkIdsAsync(
            IEnumerable<long> workIds)
        {
            return Mysql.QueryAsync<ParentChildren<Work,Chapter>>(
                ChapterSql.SelectIdsByWorkIds, new { WorkIds = workIds });
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
