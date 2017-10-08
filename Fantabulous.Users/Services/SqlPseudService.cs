using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Users.Services
{
    /// <summary>
    /// A pseudonym service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlPseudService : IPseudService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL pseudonym service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlPseudService(
            ISqlRepository repository,
            ILogger<SqlPseudService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<Pseud> GetPseudAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Pseuds.ForIdAsync(id);
            }
        }

        public async Task<Pseud> GetPseudAsync(long userId, string name)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Pseuds.ForUserIdAndNameAsync(userId, name);
            }
        }

        public async Task<IEnumerable<Pseud>> GetPseudsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Pseuds.ForIdsAsync(ids);
            }
        }

        public async Task<string> GetPseudJsonAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Pseuds.ForIdAsync(id));
            }
        }

        public async Task<string> GetPseudJsonAsync(long userId, string name)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Pseuds.ForUserIdAndNameAsync(userId, name));
            }
        }

        public async Task<IEnumerable<string>> GetPseudJsonsAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return (await db.Pseuds.ForIdsAsync(ids))
                    .Select(u => JsonConvert.SerializeObject(u));
            }
        }
    }
}
