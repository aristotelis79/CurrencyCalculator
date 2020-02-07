using System;
using System.Threading.Tasks;

namespace CurrCalc.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMemoryService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task ClearAsync<T>(object key);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="createItem"></param>
        /// <param name="expireTimeInSec"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOrCreateAsync<T>(object key, Func<Task<T>> createItem, int expireTimeInSec = 86400);
    }
}