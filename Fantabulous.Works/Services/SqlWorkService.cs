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
                work.TagIds = (await db.Tags.IdsForWorkIdAsync(id)).ToArray();
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
                var pseuds = new IdPairReader<Work,Pseud>(
                    await db.Pseuds.IdsForWorkIdsAsync(ids));
                var tags = new IdPairReader<Work,Tag>(
                    await db.Tags.IdsForWorkIdsAsync(ids));
                var series = new IdPairReader<Work,Series>(
                    await db.Series.IdsForWorkIdsAsync(ids));
                var chapters = new IdPairReader<Work,Chapter>(
                    await db.Chapters.IdsForWorkIdsAsync(ids));

                return works
                    .Select(s => AddArrays(s, pseuds, tags, series, chapters));
            }
        }

        private Work AddArrays(
            Work work,
            IdPairReader<Work,Pseud> pseuds,
            IdPairReader<Work,Tag> tags,
            IdPairReader<Work,Series> series,
            IdPairReader<Work,Chapter> chapters)
        {
            work.PseudIds = pseuds.GetChildIdsForParentId(work.Id);
            work.TagIds = tags.GetChildIdsForParentId(work.Id);
            work.SeriesIds = series.GetChildIdsForParentId(work.Id);
            work.ChapterIds = chapters.GetChildIdsForParentId(work.Id);

            return work;
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
