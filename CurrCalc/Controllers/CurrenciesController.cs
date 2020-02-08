using System;
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
        /// Localize_Key_Get_Currencies
        /// </summary>
        /// <param name="code">Iso currency code</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <response code="200">OK</response>
        /// <response code="400">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>Currency model</returns>
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
        /// Localize_Key_Put_Currencies
        /// </summary>
        /// <param name="code">Iso currency code</param>
        /// <param name="model">Additional information for create or update currency</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <response code="200">OK</response>
        /// <response code="400">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>Currency model</returns>
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
        /// Localize_Key_Post_Currencies
        /// </summary>
        /// <param name="code">Iso currency code</param>
        /// <param name="model">Additional information for create or update currency</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>currency model</returns>
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
        /// Localize_Key_Delete_Currencies
        /// </summary>
        /// <param name="code">Iso currency code</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <response code="200">OK</response>
        /// <response code="204">NoContent</response>
        /// <response code="400">NotFound</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">InternalServerError</response>
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
