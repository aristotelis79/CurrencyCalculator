using System.ComponentModel.DataAnnotations;

namespace CurrCalc.Models.Currency
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyUpdateModel : CurrencyModel
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(3, MinimumLength = 3, ErrorMessage = "This field must be 3 characters or null")]
        public string Code { get; set; }
    }
}