using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrCalc.Data;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using CurrCalc.Mappers;
using CurrCalc.Models;
using CurrCalc.Models.Common;
using CurrCalc.Models.CurrencyExchangeRate;
using CurrCalc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [ApiController]
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="day"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet, Route("api/exchange/{Source.IsoCodeValue}/{Target.IsoCodeValue}")]
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut, Route("api/[controller]")]
        [Authorize(Roles = "Admin,Trader")]
        public async Task<IActionResult> Put([FromBody] CurrencyExchangeRateModel model, CancellationToken token = default)
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesByIsoCode(model.Source.IsoCodeValue,model.Target.IsoCodeValue, token: token)
                    .ConfigureAwait(false);

                if (currencies.Values.Any(x=> x == null)) 
                    return NotFoundObjectResult(nameof(CurrencyService), "currency code don't exist");

                var updateExchangeRate = await _exchangeService.GetExchangeRateAsync(currencies["source"], currencies["target"], model.Day.Date, token)
                                                                .ConfigureAwait(false);

                await _repository.UpdateAsync(updateExchangeRate.Update(model), token: token).ConfigureAwait(false);

                return OkObjectResult(updateExchangeRate.ToModel(currencies));
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrencyExchangeRatesController), e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost, Route("api/[controller]")]
        [Authorize(Roles = "Admin,Trader")]
        public async Task<IActionResult> Post([FromBody] CurrencyExchangeRateModel model, CancellationToken token = default)
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesByIsoCode(model.Source.IsoCodeValue,model.Target.IsoCodeValue, token: token)
                    .ConfigureAwait(false);

                if (currencies.Values.Any(x=> x == null)) 
                    return NotFoundObjectResult(nameof(CurrencyService), "currency code don't exist");

                var currencyExchangeRate = model.ToEntity(currencies);

                await _repository.InsertAsync(currencyExchangeRate, token: token)
                    .ConfigureAwait(false);

                return CreateObjectResult(currencyExchangeRate.ToModel(currencies));
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrencyExchangeRatesController), e);
            }
        }
    }
}
