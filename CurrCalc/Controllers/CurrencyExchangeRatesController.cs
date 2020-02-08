using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using CurrCalc.Mappers;
using CurrCalc.Models.CurrencyExchangeRate;
using CurrCalc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Route("api/exchanges/{Source.IsoCodeValue}/{Target.IsoCodeValue}")]
    public class CurrencyExchangeRatesController : BaseController
    {
        private readonly IRepository<CurrencyExchangeRate,int> _repository;
        private readonly IExchangeService _exchangeService;
        private readonly ICurrencyService _currencyService;

        /// <inheritdoc />
        public CurrencyExchangeRatesController(IRepository<CurrencyExchangeRate,int> repository,
                                                ILogger<CurrencyExchangeRatesController> logger,
                                                IExchangeService exchangeService,
                                                ICurrencyService currencyService) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(exchangeService));
            _currencyService = currencyService ?? throw new ArgumentException(nameof(currencyService));
        }

        /// <summary>
        /// Get_Exchange_Rate
        /// </summary>
        /// <param name="request">Currency codes for source and target currencies</param>
        /// <param name="day">Day of exchange rate</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] CurrencyExchangeRateRequest request, [FromQuery] DateTime? day = null, CancellationToken token = default)
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesByIsoCode(request.Source.IsoCodeValue,request.Target.IsoCodeValue, token: token)
                                                                            .ConfigureAwait(false);


                if (currencies.Values.Any(x=> x == null)) 
                    return NotFoundObjectResult(nameof(CurrencyService), "currency code don't exist");

                var exchangeRate = await _exchangeService.GetExchangeRateAsync(currencies["source"], currencies["target"], day, token)
                                                            .ConfigureAwait(false);

                return exchangeRate == null ? NotFoundObjectResult(nameof(ExchangeService), "exchange rate don't exist") 
                                            : OkObjectResult(exchangeRate.Rate);
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrencyExchangeRatesController), e);
            }
        }


        /// <summary>
        /// Put_Exchange_Rate
        /// </summary>
        /// <param name="request">Currency codes for source and target currencies</param>
        /// <param name="model">Additional information for create or update exchange range</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin,Trader")]
        public async Task<IActionResult> Put([FromRoute] CurrencyExchangeRateRequest request, [FromBody] CurrencyExchangeRateModel model, CancellationToken token = default)
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesByIsoCode(request.Source.IsoCodeValue,request.Target.IsoCodeValue, token: token)
                    .ConfigureAwait(false);

                if (currencies.Values.Any(x=> x == null)) 
                    return NotFoundObjectResult(nameof(CurrencyService), "currency code don't exist");

                var updateExchangeRate = await _exchangeService.GetExchangeRateAsync(currencies["source"], currencies["target"], model.Day?.Date, token)
                                                                .ConfigureAwait(false);

                await _repository.UpdateAsync(updateExchangeRate.Update(model), token: token).ConfigureAwait(false);

                return OkObjectResult(updateExchangeRate.ToModel());
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrencyExchangeRatesController), e);
            }
        }


        /// <summary>
        /// Post_Exchange_Rate
        /// </summary>
        /// <param name="request">Currency codes for source and target currencies</param>
        /// <param name="model">Additional information for create or update exchange range</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Trader")]
        public async Task<IActionResult> Post([FromRoute] CurrencyExchangeRateRequest request, [FromBody] CurrencyExchangeRateModel model, CancellationToken token = default)
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesByIsoCode(request.Source.IsoCodeValue,request.Target.IsoCodeValue, token: token)
                    .ConfigureAwait(false);

                if (currencies.Values.Any(x=> x == null)) 
                    return NotFoundObjectResult(nameof(CurrencyService), "currency code don't exist");

                var currencyExchangeRate = model.ToEntity(currencies);

                await _repository.InsertAsync(currencyExchangeRate, token: token).ConfigureAwait(false);

                return CreateObjectResult(currencyExchangeRate.ToModel());
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrencyExchangeRatesController), e);
            }
        }
    }
}
