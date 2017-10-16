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
    /// Series data-access backed by a MySQL database.
    /// </summary>
    /// </inheritDoc>
    public class MysqlSeriesDao : ISeriesDao
    {
        private readonly MysqlDb Mysql;

        /// <summary>
        /// Create a new series DAO object.
        /// </summary>
        /// <param name="mysql">
        /// The wrapped MySQL connection to use.
        /// </param>
        internal MysqlSeriesDao(MysqlDb mysql)
        {
            Mysql = mysql;
        }

        public Task<Series> ForIdAsync(long id)
        {
            return Mysql.QueryFirstAsync<Series>(SeriesSql.SelectById,
                new { Id = id });
        }

        public Task<IEnumerable<Series>> ForIdsAsync(
            IEnumerable<long> ids)
        {
            return Mysql.QueryAsync<Series>(SeriesSql.SelectByIds,
                new { Ids = ids });
        }

        public Task<IEnumerable<long>> IdsForWorkIdAsync(long workId)
        {
            return Mysql.QueryAsync<long>(SeriesSql.SelectIdsByWorkId,
                new { WorkId = workId });
        }

        public Task<IEnumerable<ParentChildren<Work,Series>>> IdsForWorkIdsAsync(
            IEnumerable<long> workIds)
        {
            return Mysql.QueryAsync<ParentChildren<Work,Series>>(
                SeriesSql.SelectIdsByWorkIds, new { WorkIds = workIds });
        }

        public async Task<Series> CreateAsync(string name)
        {
            try
            {
                var series = new Series { Name = name };

                series.Id = await Mysql.QueryFirstAsync<long>(SeriesSql.Insert,
                    series);

                return series;
            }
            catch (Exception e)
            {
                // TODO figure out what went wrong
                throw new CreateFailedException("Failed to create series", e);
            }
        }
    }
}
