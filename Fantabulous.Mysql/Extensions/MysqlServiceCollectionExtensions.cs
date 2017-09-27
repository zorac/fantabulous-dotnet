using System;

using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Mysql;

namespace Microsoft.Extensions.DependencyInjection {
    public static class MysqlServiceCollectionExtensions
    {
        public static IServiceCollection AddMysqlServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Mysql")
                .Get<MysqlOptions>() ?? new MysqlOptions();

            services.AddSingleton(options);
            services.AddSingleton<ISqlRepository,MysqlRepository>();

            return services;
        }
    }
}
