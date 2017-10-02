using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Repositories
{
    /// <summary>
    /// Extensions for cache repositories to perform cache miss resolution.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Fetch an entity by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A unique id
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve a cache miss
        /// </param>
        /// <returns>
        /// The entity, or null if not found
        /// </returns>
        public async static Task<T> GetAsync<T>(
            this IIdCacheCommon<T> cache,
            long id,
            Func<long,Task<T>> resolver)
            where T: HasId
        {
            var entity = await cache.GetAsync(id);

            if (entity == null)
            {
                entity = await resolver(id);
                if (entity != null) cache.SetInBackground(entity);
            }

            return entity;
        }

        /// <summary>
        /// Fetch some entities by their unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some unique ids
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve cache misses
        /// </param>
        /// <returns>
        /// The entities, empty if not found
        /// </returns>
        public async static Task<IEnumerable<T>> GetAsync<T>(
            this IIdCacheCommon<T> cache,
            IEnumerable<long> ids,
            Func<IEnumerable<long>,Task<IEnumerable<T>>> resolver)
            where T: HasId
        {
            var entityIds = ids.ToArray();
            var entities = (await cache.GetAsync(entityIds)).ToArray();
            var hits = new List<T>();
            var misses = new List<long>();

            for (int i = 0; i < entityIds.Length; i++)
            {
                if (entities[i] == null)
                {
                    misses.Add(entityIds[i]);
                }
                else
                {
                    hits.Add(entities[i]);
                }
            }

            if (misses.Count > 0)
            {
                foreach (var entity in await resolver(misses))
                {
                    hits.Add(entity);
                    cache.SetInBackground(entity);
                }
            }

            return hits;
        }

        /// <summary>
        /// Fetch an entity by its name.
        /// </summary>
        /// <param name="name">
        /// An entity name
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve a cache miss
        /// </param>
        /// <returns>
        /// The entity, or null if not found
        /// </returns>
        public async static Task<T> GetAsync<T>(
            this IIdNameCache<T> cache,
            string name,
            Func<string,Task<T>> resolver)
            where T: HasName
        {
            var entity = await cache.GetAsync(name);

            if (entity == null)
            {
                entity = await resolver(name);
                if (entity != null) cache.SetInBackground(entity);
            }

            return entity;
        }

        /// <summary>
        /// Fetch a JSON representation of an entity by its unique ID.
        /// </summary>
        /// <param name="id">
        /// A unique id
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve a cache miss
        /// </param>
        /// <returns>
        /// The entity JSON, or null if not found
        /// </returns>
        public async static Task<string> GetJsonAsync<T>(
            this IIdCacheCommon<T> cache,
            long id,
            Func<long,Task<T>> resolver)
            where T: HasId
        {
            var json = await cache.GetJsonAsync(id);

            if (json == null)
            {
                var entity = await resolver(id);
                if (entity != null) json = cache.SetInBackground(entity);
            }

            return json;
        }

        /// <summary>
        /// Fetch JSON representations of some entities by their IDs.
        /// </summary>
        /// <param name="ids">
        /// Some unique ids
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve cache misses
        /// </param>
        /// <returns>
        /// The entity JSONs, empty if not found
        /// </returns>
        public async static Task<IEnumerable<string>> GetJsonAsync<T>(
            this IIdCacheCommon<T> cache,
            IEnumerable<long> ids,
            Func<IEnumerable<long>,Task<IEnumerable<T>>> resolver)
            where T: HasId
        {
            var entityIds = ids.ToArray();
            var jsons = (await cache.GetJsonAsync(entityIds)).ToArray();
            var hits = new List<string>();
            var misses = new List<long>();

            for (int i = 0; i < entityIds.Length; i++)
            {
                if (jsons[i] == null)
                {
                    misses.Add(entityIds[i]);
                }
                else
                {
                    hits.Add(jsons[i]);
                }
            }

            if (misses.Count > 0)
            {
                foreach (var entity in await resolver(misses))
                {
                    hits.Add(cache.SetInBackground(entity));
                }
            }

            return hits;
        }

        /// <summary>
        /// Fetch a JSON representation of an entity by its name.
        /// </summary>
        /// <param name="name">
        /// An entity name
        /// </param>
        /// <param name="resolver">
        /// A function which can resolve a cache miss
        /// </param>
        /// <returns>
        /// The entity JSON, or null if not found
        /// </returns>
        public async static Task<string> GetJsonAsync<T>(
            this IIdNameCache<T> cache,
            string name,
            Func<string,Task<T>> resolver)
            where T: HasName
        {
            var json = await cache.GetJsonAsync(name);

            if (json == null)
            {
                var entity = await resolver(name);
                if (entity != null) json = cache.SetInBackground(entity);
            }

            return json;
        }
    }
}
