using CurrCalc.Models.Common;
using Newtonsoft.Json;

namespace CurrCalc.Models.Currency
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyCreateModel : CurrencyModel
    {
        /// <inheritdoc />
        public CurrencyCreateModel()
        {
        }

        /// <inheritdoc />
        public CurrencyCreateModel(CurrencyModel model, IsoCode code) : base(model)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        public IsoCode Code { get; set; }
    }
}