using System;

namespace CurrCalc.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyExchangeRateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SourceIsoCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TargetIsoCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public float Rate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime Day { get; set; }
    }
}