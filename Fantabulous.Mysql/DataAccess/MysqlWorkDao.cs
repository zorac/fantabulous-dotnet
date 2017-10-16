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
    /// Work data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlWorkDao : IWorkDao
    {
        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new work DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlWorkDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<Work> ForIdAsync(long id)
        {
            return Mysql.QueryFirstAsync<Work>(WorkSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<Work>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<Work>(WorkSql.SelectByIds,
                new { Ids = ids });
        }

        public Task<IEnumerable<long>> IdsForSeriesIdAsync(long seriesId)
        {
            return Mysql.QueryAsync<long>(WorkSql.SelectIdsBySeriesId,
                new { SeriesId = seriesId });
        }

        public Task<IEnumerable<ParentChildren<Series,Work>>> IdsForSeriesIdsAsync(
            IEnumerable<long> seriesIds)
        {
            return Mysql.QueryAsync<ParentChildren<Series,Work>>(
                WorkSql.SelectIdsBySeriesIds, new { SeriesIds = seriesIds });
        }

        public async Task<Work> CreateAsync(string name)
        {
            try
            {
                var work = new Work { Name = name };

                work.Id = await Mysql.QueryFirstAsync<long>(WorkSql.Insert, work);

                return work;
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong
                throw new CreateFailedException("Failed to create work", e);
            }
        }
    }
}
