using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace CurrCalc.Services
{
    /// <inheritdoc />
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency, int> _repository;
        private readonly IMemoryService _memoryService;

        private const string CURRENCIES_CACHE_KEY = "currencies";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="memoryService"></param>
        public CurrencyService(IRepository<Currency, int> repository,
                                IMemoryService memoryService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _memoryService = memoryService ?? throw new ArgumentException(nameof(memoryService));
        }

        /// <inheritdoc />
        public async Task<IList<Currency>> GetAll(CancellationToken token = default)
        {
            return await _memoryService.GetOrCreateAsync(CURRENCIES_CACHE_KEY, async () => await _repository.TableNoTracking
                                                                                                                .ToListAsync(token)
                                                                                                                .ConfigureAwait(false));
        }
        /// <inheritdoc />
        public async Task<Currency> GetCurrencyByIsoCode(string isoCode, bool getFromDb = false, CancellationToken token = default)
        {
            return getFromDb ? await _repository.Table.FirstOrDefaultAsync(x => x.IsoCode.Equals(isoCode.ToUpperInvariant()), token)
                                                        .ConfigureAwait(false)
                             
                             : (await GetAll(token).ConfigureAwait(false))
                                                    .FirstOrDefault(x => x.IsoCode.Equals(isoCode.ToUpperInvariant()));
        }

        /// <inheritdoc />
        public async Task<Dictionary<string,Currency>> GetCurrenciesByIsoCode(string sourceCode, string targetCode, bool getFromDb = false, CancellationToken token = default)
        {
            var sourceCurrencyTask = GetCurrencyByIsoCode(sourceCode, getFromDb, token);
            var targetCurrencyTask = GetCurrencyByIsoCode(targetCode, getFromDb, token);

            await Task.WhenAll(sourceCurrencyTask, targetCurrencyTask).ConfigureAwait(false);

            return new Dictionary<string, Currency>{{"source",sourceCurrencyTask.Result},{"target", targetCurrencyTask.Result}};
        }

        /// <inheritdoc />
        public async Task ClearCurrencyCacheAsync()
        {
           await _memoryService.ClearAsync<string>(CURRENCIES_CACHE_KEY);
        }
    }
}