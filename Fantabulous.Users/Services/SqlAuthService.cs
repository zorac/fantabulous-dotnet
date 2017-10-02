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

namespace Fantabulous.Users.Services
{
    /// <summary>
    /// An auth service using a SQL database backend.
    /// </summary>
    /// <inheritDoc/>
    public class SqlAuthService : IAuthService
    {
        private readonly ISqlRepository Repository;
        private readonly ILogger Logger;

        /// <summary>
        /// Creates a new SQL auth service.
        /// </summary>
        /// <param name="repository">
        /// The SQL repository to load data from.
        /// </param>
        /// <param name="logger">
        /// A logger for this service.
        /// </param>
        public SqlAuthService(
            ISqlRepository repository,
            ILogger<SqlAuthService> logger)
        {
            Repository = repository;
            Logger = logger;
            logger.LogInformation("Service initialised");

        }

        public async Task<User> LoginAsync(string username, string password)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Users.LoginAsync(username, password);
            }
        }

        public async Task<User> CreateUserAsync(
            string username,
            string password,
            string email)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                return await db.Users.CreateAsync(username, password, email);
            }
        }

        public async Task ChangePasswordAsync(
            long id,
            string oldPassword,
            string newPassword)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                await db.Users.ChangePasswordAsync(id, oldPassword, newPassword);
            }
        }
    }
}
