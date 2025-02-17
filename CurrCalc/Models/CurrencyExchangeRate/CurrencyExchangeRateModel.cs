﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CurrCalc.Models.CurrencyExchangeRate
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyExchangeRateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public float Rate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? Day { get; set; }
    }
}