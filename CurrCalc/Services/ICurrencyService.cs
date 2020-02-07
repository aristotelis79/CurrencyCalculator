using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;

namespace CurrCalc.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurrencyService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<Currency>> GetAll(CancellationToken token = default);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoCode"></param>
        /// <param name="getFromDb"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Currency> GetCurrencyByIsoCode(string isoCode, bool getFromDb = false, CancellationToken token = default);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="targetCode"></param>
        /// <param name="getFromDb"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Dictionary<string,Currency>> GetCurrenciesByIsoCode(string sourceCode, string targetCode, bool getFromDb = false, CancellationToken token = default);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task ClearCurrencyCacheAsync();
    }
}