using System;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace Fantabulous.Redis
{
    public class RedisRepository : IDisposable
    {
        protected internal readonly ConnectionMultiplexer Redis;

        public RedisRepository(RedisOptions options)
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
