using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;
using Fantabulous.Core.Types;
using Fantabulous.Mysql.Constants;
using Fantabulous.Mysql.Repositories;

namespace Fantabulous.Mysql.DataAccess
{
    /// <summary>
    /// Tag data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlTagDao : ITagDao
    {
        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new tag DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlTagDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<Tag> ForIdAsync(long id)
        {
            return Mysql.QueryFirstAsync<Tag>(TagSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<Tag>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<Tag>(TagSql.SelectByIds,
                new { Ids = ids });
        }

        public Task<IEnumerable<long>> IdsForWorkIdAsync(long workId)
        {
            return Mysql.QueryAsync<long>(TagSql.SelectIdsByWorkId,
                new { WorkId = workId });
        }

        public Task<IEnumerable<IdPair<Work,Tag>>> IdsForWorkIdsAsync(
            IEnumerable<long> workIds)
        {
            return Mysql.QueryAsync<IdPair<Work,Tag>>(TagSql.SelectIdsByWorkIds,
                new { WorkIds = workIds });
        }

        public Task<IEnumerable<long>> IdsForSeriesIdAsync(long seriesId)
        {
            return Mysql.QueryAsync<long>(TagSql.SelectIdsBySeriesId,
                new { SeriesId = seriesId });
        }

        public Task<IEnumerable<IdPair<Series,Tag>>> IdsForSeriesIdsAsync(
            IEnumerable<long> seriesIds)
        {
            return Mysql.QueryAsync<IdPair<Series,Tag>>(
                TagSql.SelectIdsBySeriesIds, new { SeriesIds = seriesIds });
        }

        public async Task<Tag> CreateAsync(
            TagType type,
            long aliasFor,
            string name)
        {
            try
            {
                var tag = new Tag {
                    Type = type,
                    AliasFor = aliasFor,
                    Name = name
                };

                tag.Id = await Mysql.QueryFirstAsync<long>(TagSql.Insert, tag);

                return tag;
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong
                throw new CreateFailedException("Failed to create tag", e);
            }
        }
    }
}
