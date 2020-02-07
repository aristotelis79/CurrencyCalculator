using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace CurrCalc.Services
{
    /// <inheritdoc />
    public class ExchangeService : IExchangeService
    {
        private readonly IRepository<CurrencyExchangeRate, int> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ExchangeService(IRepository<CurrencyExchangeRate, int> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CurrencyExchangeRate> GetExchangeRateAsync(Currency source, Currency target, DateTime? time = default, CancellationToken token = default)
        {
            var query = _repository.TableNoTracking.Where(x => x.SourceId == source.Id && x.TargetId == target.Id);

            var to = time ?? DateTime.UtcNow;

            return await query.FirstOrDefaultAsync(x => x.From >= to && to <= x.To, token).ConfigureAwait(false);
        }
    }
}