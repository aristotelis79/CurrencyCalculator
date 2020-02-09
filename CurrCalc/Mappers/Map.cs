using System;
using System.Collections.Generic;
using CurrCalc.Data.Entities;
using CurrCalc.Models;
using CurrCalc.Models.Common;
using CurrCalc.Models.Currency;
using CurrCalc.Models.CurrencyExchangeRate;

namespace CurrCalc.Mappers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="currencies"></param>
        /// <returns></returns>
        public static CurrencyExchangeRate ToEntity(this CurrencyExchangeRateModel model, Dictionary<string,Currency> currencies)
        {
            return new CurrencyExchangeRate
            {
                SourceId = currencies["source"].Id,
                TargetId = currencies["target"].Id,
                Rate = model.Rate,
                From = (model.Day?.Date ?? DateTime.UtcNow.Date),
                To =  (model.Day?.Date.AddDays(1).AddTicks(-1) ?? DateTime.UtcNow.Date.AddDays(1).AddTicks(-1))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Currency ToEntity(this CurrencyCreateModel model)
        {
            return new Currency
            {
                Name = model.Name,
                Country = model.Country,
                IsoCode = model.Code.IsoCodeValue.ToUpperInvariant(),
                IsoNumber = model.IsoNumber
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CurrencyCreateModel ToModel(this Currency entity)
        {
            if (entity == null) return null;

            return new CurrencyCreateModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Country = entity.Country,
                Code = new IsoCode {IsoCodeValue = entity.IsoCode},
                IsoNumber = entity.IsoNumber
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CurrencyExchangeRateModel ToModel(this CurrencyExchangeRate entity)
        {
            if (entity == null) return null;

            return new CurrencyExchangeRateModel
            { 
               Id = entity.Id,
               Rate = entity.Rate,
               Day = entity.From,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Currency Update(this Currency entity, CurrencyUpdateModel model)
        {
            if (entity == null) return null;

            entity.IsoNumber = model.IsoNumber > 0 ? model.IsoNumber : entity.IsoNumber;
            entity.IsoCode = !string.IsNullOrWhiteSpace(model.Code) ? model.Code : entity.IsoCode;
            entity.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name : entity.Name;
            entity.Country = !string.IsNullOrWhiteSpace(model.Country) ? model.Country : entity.Country;

            return entity;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CurrencyExchangeRate Update(this CurrencyExchangeRate entity, CurrencyExchangeRateModel model)
        {
            if (entity == null) return null;

            entity.Rate = model.Rate > 0 ? model.Rate : entity.Rate;
            return entity;
        }
    }
}