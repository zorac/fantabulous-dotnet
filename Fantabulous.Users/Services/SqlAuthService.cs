using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Fantabulous.Core.DataAccess;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Requests;
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

        public async Task<UserAndPseud> LoginAsync(LoginRequest request)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var response = new UserAndPseud();

                response.User = await db.Users.LoginAsync(request.Username,
                    request.Password);
                response.Pseud = await db.Pseuds.DefaultForUserAsync(
                    response.User);

                return response;
            }
        }

        public async Task<UserAndPseud> CreateUserAsync(
            CreateUserRequest request)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                var response = new UserAndPseud();

                await db.BeginAsync();
                response.User = await db.Users.CreateAsync(request.Name,
                    request.Password, request.Email);
                response.Pseud = await db.Pseuds.CreateAsync(
                    response.User.Id, request.Name);
                await db.CommitAsync();

                return response;
            }
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            using (var db = await Repository.GetDatabaseAsync())
            {
                await db.Users.ChangePasswordAsync(request.UserId,
                    request.OldPassword, request.NewPassword);
            }
        }
    }
}
