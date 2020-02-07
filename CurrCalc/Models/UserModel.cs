using CurrCalc.Data.Entities;

namespace CurrCalc.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserModel : AuthenticateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Role Role { get; set; } = Role.User;
    }
}