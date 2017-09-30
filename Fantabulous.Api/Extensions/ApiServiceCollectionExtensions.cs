using System;

using Microsoft.Extensions.Configuration;

using Fantabulous.Api.Filters;
using Fantabulous.Api.Options;
using Fantabulous.Core.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Some extensions for adding to a service collection.
    /// </summary>
    public static class ApiServiceCollectionExtensions
    {
        /// <summary>
        /// Add MVC services, additionally configuring some filters.
        /// </summary>
        public static void AddMvcWithFilters(
            this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidationFilter));
            });
        }

        /// <summary>
        /// Add the services required to support sessions to a service
        /// collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Sessions" section
        /// </param>
        public static void AddSessionServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Sessions")
                .Get<SessionsOptions>() ?? new SessionsOptions();

            if (options.Redis != null)
            {
                options.Redis.Validate("Sessions");
                services.AddDistributedRedisCache(redisOptions =>
                {
                    redisOptions.Configuration =
                        $"{options.Redis.Hostname}:{options.Redis.Port}";
                    redisOptions.InstanceName = "sessions";
                });
            }
            else if (options.Memory)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No session storage specified");
            }

            services.AddSession(sessionOptions =>
            {
                sessionOptions.Cookie.Name = options.CookieName;
                sessionOptions.Cookie.HttpOnly = true;
                sessionOptions.IdleTimeout = options.Timeout;
            });
        }
    }
}
