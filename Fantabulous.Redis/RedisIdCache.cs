using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using StackExchange.Redis;

using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;

namespace Fantabulous.Redis
{
    public class RedisIdCache<T> : RedisRepository, IIdCache<T> where T: HasId
    {
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

        public async Task<T[]> GetAsync(IEnumerable<long> ids)
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

        public async Task<string[]> GetJsonAsync(IEnumerable<long> ids)
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

        virtual public void SetInBackground(long id, string json)
        {
            var key = id.ToString();
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
        }

        virtual public string SetInBackground(T value)
        {
            var key = value.Id.ToString();
            var json = JsonConvert.SerializeObject(value);
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);

            return json;
        }
    }
}
