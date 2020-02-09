using System.Collections.Generic;

namespace CurrCalc.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Language : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LanguageName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<LocalizedText>  LocalizedTexts{ get; set; }
    }
}