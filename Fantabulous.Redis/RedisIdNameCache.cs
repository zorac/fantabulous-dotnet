using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using StackExchange.Redis;

using Fantabulous.Core.Models;
using Fantabulous.Core.Repositories;

namespace Fantabulous.Redis
{
    public class RedisIdNameCache<T> : RedisIdCache<T>, IIdNameCache<T> where T: HasName
    {
        public RedisIdNameCache(RedisCacheOptions<T> options) : base(options)
        {
        }

        public async Task<T> GetAsync(string name)
        {
            var key = ":" + name;
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
            var key = ":" + name;
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

        override public void SetInBackground(long id, string json)
        {
            throw new NotSupportedException("Name is required");
        }

        public void SetInBackground(long id, string name, string json)
        {
            var key = id.ToString();
            var nameKey = ":" + name;
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
            redis.StringSet(nameKey, key, flags: CommandFlags.FireAndForget);
        }

        override public string SetInBackground(T value)
        {
            var key = value.Id.ToString();
            var nameKey = ":" + value.Name;
            var json = JsonConvert.SerializeObject(value);
            var redis = Redis.GetDatabase();

            redis.StringSet(key, json, flags: CommandFlags.FireAndForget);
            redis.StringSet(nameKey, key, flags: CommandFlags.FireAndForget);

            return json;
        }
    }
}
