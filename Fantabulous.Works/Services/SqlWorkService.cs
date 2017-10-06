using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Entities;
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
                return await db.Works.ForIdAsync(id);
            }
        }

        public async Task<IEnumerable<Work>> GetWorksAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Works.ForIdsAsync(ids);
            }
        }

        public async Task<string> GetWorkJsonAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Works.ForIdAsync(id));
            }
        }

        public async Task<IEnumerable<string>> GetWorkJsonsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return (await db.Works.ForIdsAsync(ids))
                    .Select(u => JsonConvert.SerializeObject(u));
            }
        }
    }
}
