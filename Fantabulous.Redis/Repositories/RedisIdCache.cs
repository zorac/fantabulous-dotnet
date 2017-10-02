using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using StackExchange.Redis;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Repositories;
using Fantabulous.Redis.Options;

namespace Fantabulous.Redis.Repositories
{
    /// <summary>
    /// An ID-based cache backed by a Redis key/value store.
    /// </summary>
    /// <inheritDoc/>
    public class RedisIdCache<T> : RedisRepository, IIdCache<T> where T: HasId
    {
        /// <summary>
        /// Create a new Redis cache.
        /// </summary>
        /// <param name="options">
        /// Options to use to set up the Redis connection.
        /// </param>
        /// <param name="logger">
        /// A logger to use for this repository.
        /// </param>
        public RedisIdCache(
            RedisCacheOptions<T> options,
            ILogger<RedisIdCache<T>> logger)
            : base(options, logger)
        {
        }

        public async Task<T> GetAsync(long id)
        {
            var key = id.ToString();
            var redis = Redis.GetDatabase();
            var json = await redis.StringGetAsync(key);

            return json.IsNullOrEmpty ? null
                : JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<IEnumerable<T>> GetAsync(IEnumerable<long> ids)
        {
            var keys = ids.Select(id => (RedisKey)id.ToString()).ToArray();
            var length = keys.Length;
            var redis = Redis.GetDatabase();
            var jsons = await redis.StringGetAsync(keys);
            var results = new T[length];

            for (int i = 0; i < length; i++)
            {
                var json = jsons[i];

                results[i] = json.IsNullOrEmpty ? null
                    : JsonConvert.DeserializeObject<T>(json);
            }

            return results;
        }

        public async Task<string> GetJsonAsync(long id)
        {
            var redis = Redis.GetDatabase();
            var keys = id.ToString();
            var json = await redis.StringGetAsync(keys);

            return json.IsNullOrEmpty ? null : (string)json;
        }

        public async Task<IEnumerable<string>> GetJsonAsync(IEnumerable<long> ids)
        {
            var keys = ids.Select(id => (RedisKey)id.ToString()).ToArray();
            var length = keys.Length;
            var redis = Redis.GetDatabase();
            var jsons = await redis.StringGetAsync(keys);
            var results = new string[length];

            for (int i = 0; i < length; i++)
            {
                var json = jsons[i];

                results[i] = json.IsNullOrEmpty ? null : (string)json;
            }

            return results;
        }

        virtual public string SetInBackground(T value)
        {
            var key = value.Id.ToString();
            var json = JsonConvert.SerializeObject(value);
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);

            return json;
        }

        public void SetInBackground(long id, string json)
        {
            var key = id.ToString();
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
        }
    }
}
