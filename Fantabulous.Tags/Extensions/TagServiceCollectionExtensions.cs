using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Redis.Repositories;
using Fantabulous.Tags.Options;
using Fantabulous.Tags.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ServiceCollection extensions for tag-related services.
    /// </summary>
    public static class TagServiceCollectionExtensions
    {
        /// <summary>
        /// Add tag services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Tags" section.
        /// </param>
        public static void AddTagServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Tags")
                .Get<TagOptions>() ?? new TagOptions();

            if (options.Sql)
            {
                services.AddSingleton<ITagService,SqlTagService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end tag service specified, eg Tags.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Tags");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdCache<Tag>,RedisIdCache<Tag>>();
                services.Decorate<ITagService,CacheTagService>();
            }
        }
    }
}
