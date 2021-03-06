using System;
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
    /// An ID/name-based cache backed by a Redis key/value store.
    /// </summary>
    /// <inheritDoc/>
    public class RedisIdNameCache<T> : RedisIdCache<T>, IIdNameCache<T> where T: HasName
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
        public RedisIdNameCache(
            RedisCacheOptions<T> options,
            ILogger<RedisIdNameCache<T>> logger)
            : base(options, logger)
        {
        }

        public async Task<T> GetAsync(string name)
        {
            var key = ":" + name.ToLowerInvariant();
            var redis = Redis.GetDatabase();
            var id = await redis.StringGetAsync(key);

            if (id.IsNullOrEmpty) return null;

            var json = await redis.StringGetAsync((string)id);

            if (json.HasValue)
            {
                var result = JsonConvert.DeserializeObject<T>(json);

                if (result.Name == name) return result;
            }

            redis.KeyDelete(key, flags: CommandFlags.FireAndForget);

            return null;
        }

        public async Task<string> GetJsonAsync(string name)
        {
            var key = ":" + name.ToLowerInvariant();
            var redis = Redis.GetDatabase();
            var id = await redis.StringGetAsync(key);

            if (id.IsNullOrEmpty) return null;

            var json = await redis.StringGetAsync((string)id);

            if (json.HasValue)
            {
                var result = JsonConvert.DeserializeObject<T>(json);

                if (result.Name == name) return json;
            }

            redis.KeyDelete(key, flags: CommandFlags.FireAndForget);

            return null;
        }

        override public string SetInBackground(T value)
        {
            var key = value.Id.ToString();
            var nameKey = ":" + value.Name.ToLowerInvariant();
            var json = JsonConvert.SerializeObject(value);
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
            redis.StringSet(nameKey, key, flags: CommandFlags.FireAndForget);

            return json;
        }

        new public void SetInBackground(long id, string json)
        {
            throw new NotSupportedException("Name is required");
        }

        public void SetInBackground(long id, string name, string json)
        {
            var key = id.ToString();
            var nameKey = ":" + name.ToLowerInvariant();
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
            redis.StringSet(nameKey, key, flags: CommandFlags.FireAndForget);
        }
    }
}
