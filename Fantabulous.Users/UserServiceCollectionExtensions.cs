using System;
using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Redis;
using Fantabulous.Users;

namespace Microsoft.Extensions.DependencyInjection {
    public static class UserServiceCollectionExtensions
    {
        public static IServiceCollection AddUserServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.Get<UserOptions>()
                ?? new UserOptions();

            services.AddSingleton<IUserService,MockUserService>();

            if (options.Redis != null)
            {
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdNameCache<User>,RedisIdNameCache<User>>();
                services.Decorate<IUserService,CacheUserService>();
            }

            return services;
        }

        public static IServiceCollection AddAuthServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IAuthService,MockUserService>();

            return services;
        }
    }
}
