using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Works.Services
{
    /// <summary>
    /// A chapter service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlChapterService : IChapterService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL chapter service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlChapterService(
            ISqlRepository repository,
            ILogger<SqlChapterService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<Chapter> GetChapterAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Chapters.ForIdAsync(id);
            }
        }

        public async Task<IEnumerable<Chapter>> GetChaptersAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Chapters.ForIdsAsync(ids);
            }
        }

        public async Task<string> GetChapterJsonAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Chapters.ForIdAsync(id));
            }
        }

        public async Task<IEnumerable<string>> GetChapterJsonsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return (await db.Chapters.ForIdsAsync(ids))
                    .Select(u => JsonConvert.SerializeObject(u));
            }
        }
    }
}
