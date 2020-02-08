using System;
using CurrCalc.Models.Common;

namespace CurrCalc.Models.CurrencyExchangeRate
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyExchangeRateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public IsoCode Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IsoCode Target { get; set; }
    }
}