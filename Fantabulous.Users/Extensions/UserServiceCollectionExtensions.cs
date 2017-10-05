using System;

using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Redis.Repositories;
using Fantabulous.Users.Options;
using Fantabulous.Users.Services;

namespace Microsoft.Extensions.DependencyInjection {
    /// <summary>
    /// ServiceCollection extensions dor user/authentication services.
    /// </summary>
    public static class UserServiceCollectionExtensions
    {
        /// <summary>
        /// Add authentication services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing an "Auth" section.
        /// </param>
        public static void AddAuthServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Auth")
                .Get<AuthOptions>() ?? new AuthOptions();

            if (options.Sql)
            {
                services.AddSingleton<IAuthService,SqlAuthService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end auth service specified, eg Auth.Sql=true");
            }
        }

        /// <summary>
        /// Add user services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Users" section.
        /// </param>
        public static void AddUserServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Users")
                .Get<UserOptions>() ?? new UserOptions();

            if (options.Sql)
            {
                services.AddSingleton<IUserService,SqlUserService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end user service specified, eg Users.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Users");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdNameCache<User>,RedisIdNameCache<User>>();
                services.Decorate<IUserService,CacheUserService>();
            }
        }

        /// <summary>
        /// Add pseudonym services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Pseuds" section.
        /// </param>
        public static void AddPseudServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Pseuds")
                .Get<PseudOptions>() ?? new PseudOptions();

            if (options.Sql)
            {
                services.AddSingleton<IPseudService,SqlPseudService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end pseud service specified, eg Pseuds.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Pseuds");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdCache<Pseud>,RedisIdCache<Pseud>>();
                services.Decorate<IPseudService,CachePseudService>();
            }
        }
    }
}
