using System;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;

namespace CurrCalc.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExchangeService
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="source"></param>
       /// <param name="target"></param>
       /// <param name="day"></param>
       /// <param name="token"></param>
       /// <returns></returns>
       Task<CurrencyExchangeRate> GetExchangeRateAsync(Currency source, Currency target, DateTime? day = default, CancellationToken token = default);
    }
}