using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;

namespace Fantabulous.Users
{
    /// <summary>
    /// A user service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlUserService : IUserService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL user service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlUserService(
            ISqlRepository repository,
            ILogger<SqlUserService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");
        }

        public async Task<User> GetUserAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Users.ForIdAsync(id);
            }
        }

        public async Task<User> GetUserAsync(string name)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Users.ForNameAsync(name);
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Users.ForIdsAsync(ids);
            }
        }

        public async Task<string> GetUserJsonAsync(long id)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Users.ForIdAsync(id));
            }
        }

        public async Task<string> GetUserJsonAsync(string name)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return JsonConvert.SerializeObject(
                    await db.Users.ForNameAsync(name));
            }
        }

        public async Task<IEnumerable<string>> GetUsersJsonAsync(
            IEnumerable<long> ids)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return (await db.Users.ForIdsAsync(ids))
                    .Select(u => JsonConvert.SerializeObject(u));
            }
        }
    }
}
