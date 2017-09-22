using System;

using Fantabulous.Api.Options;

namespace Microsoft.Extensions.DependencyInjection {
    public static class FantabulousSessionServiceCollectionExtensions
    {
        public static IServiceCollection AddFantabulousSession(
            this IServiceCollection services,
            SessionsOptions options)
        {
            services.AddDistributedRedisCache(redisOptions =>
            {
                redisOptions.Configuration = $"{options.Hostname}:{options.Port}";
                redisOptions.InstanceName = "sessions";
            });

            services.AddSession(sessionOptions =>
            {
                sessionOptions.Cookie.Name = "fantabulous.session";
                sessionOptions.Cookie.HttpOnly = true;
                sessionOptions.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            return services;
        }
    }
}
