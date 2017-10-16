using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Core.Types;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A work service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlWorkService : IWorkService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL work service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlWorkService(
            ISqlRepository repository,
            ILogger<SqlWorkService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<Work> GetWorkAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var work = await db.Works.ForIdAsync(id);

                work.PseudIds = (await db.Pseuds.IdsForWorkIdAsync(id)).ToArray();
                work.TagIds = (await db.Tags.TypesAndIdsForWorkIdAsync(id)).ToDictionary();
                work.SeriesIds = (await db.Series.IdsForWorkIdAsync(id)).ToArray();
                work.ChapterIds = (await db.Chapters.IdsForWorkIdAsync(id)).ToArray();

                return work;
            }
        }

        public async Task<IEnumerable<Work>> GetWorksAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var works = await db.Works.ForIdsAsync(ids);
                var pseuds = (await db.Pseuds.IdsForWorkIdsAsync(ids)).GetReader();
                var tags = (await db.Tags.TypesAndIdsForWorkIdsAsync(ids)).GetReader();
                var series = (await db.Series.IdsForWorkIdsAsync(ids)).GetReader();
                var chapters = (await db.Chapters.IdsForWorkIdsAsync(ids)).GetReader();

                return works.Select(w => {
                    w.PseudIds = pseuds.Get(w.Id);
                    w.TagIds = tags.Get(w.Id);
                    w.SeriesIds = series.Get(w.Id);
                    w.ChapterIds = chapters.Get(w.Id);
                    return w;
                });
            }
        }

        public async Task<string> GetWorkJsonAsync(long id)
        {
            return JsonConvert.SerializeObject(await GetWorkAsync(id));
        }

        public async Task<IEnumerable<string>> GetWorkJsonsAsync(
            IEnumerable<long> ids)
        {
            return (await GetWorksAsync(ids))
                .Select(u => JsonConvert.SerializeObject(u));
        }
    }
}
