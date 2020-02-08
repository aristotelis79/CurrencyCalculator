using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using CurrCalc.Mappers;
using CurrCalc.Models;
using CurrCalc.Models.Common;
using CurrCalc.Models.Currency;
using CurrCalc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController : BaseController
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRepository<Currency, int> _repository;

        /// <inheritdoc cref="CurrenciesController" />
        public CurrenciesController(ICurrencyService currencyService,
            ILogger<BaseController> logger,
            IRepository<Currency, int> repository) : base(logger)
        {
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{IsoCodeValue}")]
        [Authorize(Roles = "Admin,Trader")]
        public async Task<IActionResult> Get([FromRoute] IsoCode code, CancellationToken token = default)
        {
            var currency = await _currencyService.GetCurrencyByIsoCode(code.IsoCodeValue, token: token).ConfigureAwait(false) ??
                           await _currencyService.GetCurrencyByIsoCode(code.IsoCodeValue, true, token).ConfigureAwait(false);

            return currency != null ? OkObjectResult(currency.ToModel()) 
                                    : NotFoundObjectResult(nameof(CurrencyService),"currency don't exist");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{IsoCodeValue}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute] IsoCode code, [FromBody] CurrencyUpdateModel model, CancellationToken token = default)
        {
            try
            {
                var updateCurrency = await _currencyService.GetCurrencyByIsoCode(code.IsoCodeValue, true, token)
                                            .ConfigureAwait(false);

                if (updateCurrency == null)
                    return NotFoundObjectResult(nameof(CurrencyService), "currency don't exist");

                await _repository.UpdateAsync(updateCurrency.Update(model), token: token).ConfigureAwait(false);

                await _currencyService.ClearCurrencyCacheAsync();

                return OkObjectResult(updateCurrency.ToModel());
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrenciesController), e);

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{IsoCodeValue}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromRoute] IsoCode code, CurrencyModel model, CancellationToken token = default)
        {
            try
            {
                await _repository.InsertAsync((new CurrencyCreateModel(model,code)).ToEntity(), token: token);

                await _currencyService.ClearCurrencyCacheAsync();

                return CreateObjectResult(model);
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrenciesController), e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{IsoCodeValue}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] IsoCode code, CancellationToken token = default)
        {
            try
            {
                var currency = await _currencyService.GetCurrencyByIsoCode(code.IsoCodeValue, true, token)
                                                                        .ConfigureAwait(false);
                if (currency == null)
                    return NotFoundObjectResult(nameof(CurrencyService), "currency don't exist");

                await _repository.DeleteAsync(currency, token: token).ConfigureAwait(false);

                return new ObjectResult(new ApiResponse
                {
                    Data = null,
                    Status = StatusCodes.Status204NoContent
                });
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(CurrenciesController), e);
            }
        }
    }
}
