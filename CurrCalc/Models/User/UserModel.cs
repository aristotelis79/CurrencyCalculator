using CurrCalc.Models.Common;

namespace CurrCalc.Models.User
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