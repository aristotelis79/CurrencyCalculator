using CurrCalc.Models.Common;
using Newtonsoft.Json;

namespace CurrCalc.Models.Currency
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyCreateModel : CurrencyModel
    {

        /// <summary>
        /// 
        /// </summary>
        public IsoCode Code { get; set; }
    }
}