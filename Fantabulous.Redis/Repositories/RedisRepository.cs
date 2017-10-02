using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using StackExchange.Redis;

using Fantabulous.Redis.Options;

namespace Fantabulous.Redis.Repositories
{
    /// <summary>
    /// A repository which uses a Redis key/value store.
    /// </summary>
    /// <inheritDoc/>
    public class RedisRepository : IDisposable
    {
        protected internal readonly ConnectionMultiplexer Redis;
        protected internal readonly ILogger Logger;

        /// <summary>
        /// Create a new Redis repository.
        /// </summary>
        /// <param name="options">
        /// Options to use to set up the Redis connection.
        /// </param>
        /// <param name="logger">
        /// A logger to use for this repository.
        /// </param>
        public RedisRepository(
            RedisOptions options,
            ILogger<RedisRepository> logger)
        {
            var config = new ConfigurationOptions
            {
                EndPoints = {
                    { options.Hostname, options.Port }
                },
                Password = options.Password,
                AbortOnConnectFail = false,
                DefaultDatabase = options.Database
            };

            Redis = ConnectionMultiplexer.Connect(config);
            Logger = logger;
            logger.LogInformation("Repository initialised");
        }

        /// <summary>
        /// Create a connection to the Redis database.
        /// </summary>
        /// <returns>
        /// A database connection
        /// </returns>
        public IDatabase GetDatabase()
        {
            return Redis.GetDatabase();
        }

        public void Dispose()
        {
            Redis.Close();
        }
    }
}
