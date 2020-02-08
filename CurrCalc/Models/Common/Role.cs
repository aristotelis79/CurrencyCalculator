using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CurrCalc.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        /// <summary>
        /// 
        /// </summary>
        User,
        /// <summary>
        /// 
        /// </summary>
        Trader,
        /// <summary>
        /// 
        /// </summary>
        Admin
    }
}