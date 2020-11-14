using System;
using System.Collections.Generic;

namespace Samples.RocketLaunches.Api.Services
{
    public interface ICacheClient
    {
        bool Remove(string key);
        void Remove(IEnumerable<string> keys);
        T Get<T>(string key);
        void Set<T>(T value, string key, TimeSpan? expiresIn = null);
        bool Add<T>(T value, string key, TimeSpan? expiresIn = null);
    }
}
