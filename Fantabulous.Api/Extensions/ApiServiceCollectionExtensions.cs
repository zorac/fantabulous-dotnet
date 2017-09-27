using System;

using Microsoft.Extensions.Configuration;

using Fantabulous.Api.Filters;
using Fantabulous.Api.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcWithFilters(
            this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidationFilter));
            });

            return services;
        }

        public static IServiceCollection AddSessionServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Sessions")
                .Get<SessionsOptions>() ?? new SessionsOptions();

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
