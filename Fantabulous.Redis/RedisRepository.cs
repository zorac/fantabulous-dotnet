using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using StackExchange.Redis;

namespace Fantabulous.Redis
{
    public class RedisRepository : IDisposable
    {
        protected internal readonly ConnectionMultiplexer Redis;
        protected internal readonly ILogger Logger;

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
