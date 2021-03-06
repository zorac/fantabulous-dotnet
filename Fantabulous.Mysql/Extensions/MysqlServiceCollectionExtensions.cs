using Microsoft.Extensions.Configuration;
using Fantabulous.Core.Repositories;
using Fantabulous.Mysql.Options;
using Fantabulous.Mysql.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ServiceCollection extensions for a MySQL repository.
    /// </summary>
    public static class MysqlServiceCollectionExtensions
    {
        /// <summary>
        /// Add MySQL repository services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Mysql" section.
        /// </param>
        public static void AddMysqlServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Mysql")
                .Get<MysqlOptions>() ?? new MysqlOptions();

            options.Validate();
            services.AddSingleton(options);
            services.AddSingleton<ISqlRepository,MysqlRepository>();
        }
    }
}
