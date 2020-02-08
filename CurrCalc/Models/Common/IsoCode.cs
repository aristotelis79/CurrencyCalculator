using System;
using System.ComponentModel.DataAnnotations;

namespace CurrCalc.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class IsoCode
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "This field must be 3 characters")]
        public string IsoCodeValue { get; set; }
    }
}