using System;

using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Redis;
using Fantabulous.Users;

namespace Microsoft.Extensions.DependencyInjection {
    /// <summary>
    /// ServiceCollection extensions dor user/authentication services.
    /// </summary>
    public static class UserServiceCollectionExtensions
    {
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
            else if (options.Mock)
            {
                services.AddSingleton<IUserService,MockUserService>();
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
        /// Add authentication services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing an "Auth" section.
        /// </param>
        public static void AddAuthServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IAuthService,MockUserService>();
        }
    }
}
