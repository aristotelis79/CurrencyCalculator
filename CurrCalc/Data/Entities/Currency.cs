using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrCalc.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Currency : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public Currency()
        {
            SourceCurrencyExchangeRates = new HashSet<CurrencyExchangeRate>();
            TargetCurrencyExchangeRates = new HashSet<CurrencyExchangeRate>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsoCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsoNumber { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<CurrencyExchangeRate> SourceCurrencyExchangeRates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<CurrencyExchangeRate> TargetCurrencyExchangeRates { get; set; }
    }
}