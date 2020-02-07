using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CurrCalc.Services
{
    /// <inheritdoc />
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public MemoryService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <inheritdoc />
        public async Task ClearAsync<T>(object key)
        {
            var myLock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            await myLock.WaitAsync();
            try
            {
                _cache.Remove(key);
            }
            finally
            {
                myLock.Release();
            }
        }

        /// <inheritdoc />
        public async Task<T> GetOrCreateAsync<T>(object key, Func<Task<T>> createItem, int expireTimeInSec = 86400)
        {
            if (_cache.TryGetValue(key, out T cacheEntry)) return cacheEntry;

            var myLock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            await myLock.WaitAsync();
            try
            {
                if (!_cache.TryGetValue(key, out cacheEntry))
                {
                    cacheEntry = await createItem();

                    _cache.Set(key, cacheEntry,TimeSpan.FromSeconds(expireTimeInSec));
                }
            }
            finally
            {
                myLock.Release();
            }
            return cacheEntry;
        }
    }
}