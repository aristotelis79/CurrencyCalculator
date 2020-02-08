using CurrCalc.Models.Common;

namespace CurrCalc.Models.Currency
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyModel 
    {
        /// <summary>
        /// 
        /// </summary>
        public CurrencyModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public CurrencyModel(CurrencyModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Country = model.Country;
            IsoNumber = model.IsoNumber;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

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
        public int IsoNumber { get; set; }

    }
}