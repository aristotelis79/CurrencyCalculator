﻿using System;
using System.Collections.Generic;
using CurrCalc.Data.Entities;
using CurrCalc.Models;

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
                From = model.Day.Date,
                To = model.Day.Date.AddDays(1).AddTicks(-1)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Currency ToEntity(this CurrencyModel model)
        {
            return new Currency
            {
                Name = model.Name,
                Country = model.Country,
                IsoCode = model.IsoCode.ToUpperInvariant(),
                IsoNumber = model.IsoNumber
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CurrencyModel ToModel(this Currency entity)
        {
            if (entity == null) return null;

            return new CurrencyModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Country = entity.Country,
                IsoCode = entity.IsoCode,
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
               SourceIsoCode = entity.Source?.IsoCode,
               TargetIsoCode = entity.Target?.IsoCode,
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
        public static Currency Update(this Currency entity, CurrencyModel model)
        {
            entity.IsoNumber = model.IsoNumber > 0 ? model.IsoNumber : entity.IsoNumber;
            entity.IsoCode = !string.IsNullOrWhiteSpace(model.IsoCode) ? model.IsoCode : entity.IsoCode;
            entity.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name : entity.Name;
            entity.Country = !string.IsNullOrWhiteSpace(model.Country) ? model.Country : entity.Country;

            return entity;
        }
    }
}