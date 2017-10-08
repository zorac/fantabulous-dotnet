using Microsoft.Extensions.Configuration;

using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Core.Services;
using Fantabulous.Redis.Repositories;
using Fantabulous.Works.Options;
using Fantabulous.Works.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ServiceCollection extensions for work-related services.
    /// </summary>
    public static class WorkServiceCollectionExtensions
    {
        /// <summary>
        /// Add work services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Works" section.
        /// </param>
        public static void AddWorkServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Works")
                .Get<WorkOptions>() ?? new WorkOptions();

            if (options.Sql)
            {
                services.AddSingleton<IWorkService,SqlWorkService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end work service specified, eg Works.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Works");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdCache<Work>,RedisIdCache<Work>>();
                services.Decorate<IWorkService,CacheWorkService>();
            }
        }

        /// <summary>
        /// Add chapteronym services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Chapters" section.
        /// </param>
        public static void AddChapterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Chapters")
                .Get<ChapterOptions>() ?? new ChapterOptions();

            if (options.Sql)
            {
                services.AddSingleton<IChapterService,SqlChapterService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end chapter service specified, eg Chapters.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Chapters");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdCache<Chapter>,RedisIdCache<Chapter>>();
                services.Decorate<IChapterService,CacheChapterService>();
            }
        }

        /// <summary>
        /// Add series services to the given service collection.
        /// </summary>
        /// <param name="configuration">
        /// A configuration containing a "Series" section.
        /// </param>
        public static void AddSeriesServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection("Series")
                .Get<SeriesOptions>() ?? new SeriesOptions();

            if (options.Sql)
            {
                services.AddSingleton<ISeriesService,SqlSeriesService>();
            }
            else
            {
                throw new InvalidConfigurationException(
                    "No back-end series service specified, eg Series.Sql=true");
            }

            if (options.Redis != null)
            {
                options.Redis.Validate("Series");
                services.AddSingleton(options.Redis);
                services.AddSingleton<IIdCache<Series>,RedisIdCache<Series>>();
                services.Decorate<ISeriesService,CacheSeriesService>();
            }
        }
    }
}
