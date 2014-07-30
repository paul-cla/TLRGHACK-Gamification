using System;
using System.Runtime.Caching;

namespace Keywords.DataAccess
{
    public static class ObjectCacheExtensions
    {
        public static TValue GetOrAdd<TValue>(this ObjectCache cache, string cacheKey, Func<TValue> valueFactory, CacheItemPolicy policy)
        {
            object cachedValue = cache.Get(cacheKey);
            if (cachedValue != null)
                return (TValue)cachedValue;
            var queriedValue = valueFactory();
            cache.Add(cacheKey, queriedValue, policy);
            return queriedValue;
        }
    }
}
