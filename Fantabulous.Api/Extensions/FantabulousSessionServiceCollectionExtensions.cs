using System;
using Microsoft.Extensions.Configuration;

using Fantabulous.Api.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FantabulousSessionServiceCollectionExtensions
    {
        public static IServiceCollection AddSessionServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.Get<SessionsOptions>()
                ?? new SessionsOptions();

            if (options.Redis != null)
            {
                services.AddDistributedRedisCache(redisOptions =>
                {
                    redisOptions.Configuration =
                        $"{options.Redis.Hostname}:{options.Redis.Port}";
                    redisOptions.InstanceName = "sessions";
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            services.AddSession(sessionOptions =>
            {
                sessionOptions.Cookie.Name = options.CookieName;
                sessionOptions.Cookie.HttpOnly = true;
                sessionOptions.IdleTimeout = options.Timeout;
            });

            return services;
        }
    }
}
