using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Tags.Services
{
    /// <summary>
    /// A tag service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlTagService : ITagService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL tag service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlTagService(
            ISqlRepository repository,
            ILogger<SqlTagService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<Tag> GetTagAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Tags.ForIdAsync(id);
            }
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Tags.ForIdsAsync(ids);
            }
        }

        public async Task<string> GetTagJsonAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Tags.ForIdAsync(id));
            }
        }

        public async Task<IEnumerable<string>> GetTagJsonsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return (await db.Tags.ForIdsAsync(ids))
                    .Select(u => JsonConvert.SerializeObject(u));
            }
        }
    }
}
