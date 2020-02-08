using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CurrCalc.Models.Common
{
    /// <inheritdoc />
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Language
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("English")]
        En,
        /// <summary>
        /// 
        /// </summary>
        [Description("Greek")]
        El
    }
}