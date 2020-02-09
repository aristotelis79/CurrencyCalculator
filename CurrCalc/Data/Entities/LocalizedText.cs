namespace CurrCalc.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedText : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int LanguageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }
    }
}