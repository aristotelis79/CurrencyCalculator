using System;
using System.ComponentModel.DataAnnotations;
using CurrCalc.Models.Common;

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
        public IsoCode Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public IsoCode Target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public float Rate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime Day { get; set; }
    }
}