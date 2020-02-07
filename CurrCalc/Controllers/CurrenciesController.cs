﻿using System;
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
using CurrCalc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
        /// <param name="isoCode"></param>
        /// <returns></returns>
        [HttpGet("{isoCode}")]
        public IActionResult Get(string isoCode)
        {
            var currency = _currencyService.GetCurrencyByIsoCode(isoCode).Result;
            return currency != null ? OkObjectResult(currency.ToModel()) 
                                    : NotFoundObjectResult(nameof(CurrencyService),"currency don't exist");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoCode"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{isoCode}")]
        public async Task<IActionResult> Put(string isoCode, [FromBody] CurrencyModel model, CancellationToken token = default)
        {
            try
            {
                var updateCurrency = await _currencyService.GetCurrencyByIsoCode(isoCode, true, token)
                                            .ConfigureAwait(false);


                var updateFields = await TryUpdateModelAsync(updateCurrency, "",
                        c => c.Name, c => c.Country, c => c.IsoNumber, c=>c.IsoNumber)
                                        .ConfigureAwait(false);


                if (!updateFields)
                    return NotUpdateObjectResult(nameof(CurrencyService), "currency don't update");

                await _repository.UpdateAsync(updateCurrency, token: token).ConfigureAwait(false);

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
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CurrencyModel model, CancellationToken token = default)
        {
            try
            {
                await _repository.InsertAsync(model.ToEntity(), token: token);

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
        /// <param name="isoCode"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{isoCode}")]
        public async Task<IActionResult> Delete(string isoCode, CancellationToken token = default)
        {
            try
            {
                var currency = await _currencyService.GetCurrencyByIsoCode(isoCode, true, token)
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