using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A series service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlSeriesService : ISeriesService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL series service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlSeriesService(
            ISqlRepository repository,
            ILogger<SqlSeriesService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<Series> GetSeriesAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var series = await db.Series.ForIdAsync(id);

                series.WorkIds = (await db.Works.IdsForSeriesIdAsync(id))
                    .ToArray();

                return series;
            }
        }

        public async Task<IEnumerable<Series>> GetSeriesAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var series = await db.Series.ForIdsAsync(ids);
                var works = new IdPairReader<Series,Work>(
                    await db.Works.IdsForSeriesIdsAsync(ids));

                return series.Select(s => AddArrays(s, works));
            }
        }

        private Series AddArrays(
            Series series,
            IdPairReader<Series,Work> works)
        {
            series.WorkIds = works.GetChildIdsForParentId(series.Id);

            return series;
        }

        public async Task<string> GetSeriesJsonAsync(long id)
        {
            return JsonConvert.SerializeObject(await GetSeriesAsync(id));
        }

        public async Task<IEnumerable<string>> GetSeriesJsonsAsync(
            IEnumerable<long> ids)
        {
            return (await GetSeriesAsync(ids))
                .Select(u => JsonConvert.SerializeObject(u));
        }
    }
}
