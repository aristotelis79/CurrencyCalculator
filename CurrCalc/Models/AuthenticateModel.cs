using System.ComponentModel.DataAnnotations;

namespace CurrCalc.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}