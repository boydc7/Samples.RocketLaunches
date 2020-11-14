using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class InMemoryCacheClient : ICacheClient
    {
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new ConcurrentDictionary<string, CacheItem>(StringComparer.OrdinalIgnoreCase);

        public bool Remove(string key) => _cache.TryRemove(key, out _);

        public void Remove(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                _cache.TryRemove(key, out _);
            }
        }

        public T Get<T>(string key)
        {
            if (!_cache.TryGetValue(key, out var item))
            {
                return default;
            }

            if (item?.Item == null || item.IsExpired())
            {
                _cache.TryRemove(key, out _);

                return default;
            }

            if (!(item.Item is T itemT) || itemT == null)
            {
                return default;
            }

            return itemT;
        }

        public bool Add<T>(T value, string key, TimeSpan? expiresIn = null)
        {
            var timestamp = DateTime.UtcNow.ToUnixTimestamp();

            var added = _cache.TryAdd(key, new CacheItem
                                           {
                                               Item = value,
                                               Timestamp = timestamp,
                                               Expires = expiresIn == null || expiresIn.Value.TotalSeconds < 1
                                                             ? 0
                                                             : timestamp + (long)expiresIn.Value.TotalSeconds
                                           });

            return added;
        }

        public void Set<T>(T value, string key, TimeSpan? expiresIn = null)
        {
            var timestamp = DateTime.UtcNow.ToUnixTimestamp();

            _cache.AddOrUpdate(key,
                               new CacheItem
                               {
                                   Item = value,
                                   Timestamp = timestamp,
                                   Expires = expiresIn == null || expiresIn.Value.TotalSeconds < 1
                                                 ? 0
                                                 : timestamp + (long)expiresIn.Value.TotalSeconds
                               },
                               (k, x) =>
                               {
                                   x.Item = value;
                                   x.Timestamp = timestamp;

                                   x.Expires = expiresIn == null || expiresIn.Value.TotalSeconds < 1
                                                   ? 0
                                                   : timestamp + (long)expiresIn.Value.TotalSeconds;

                                   return x;
                               });
        }
    }
}
