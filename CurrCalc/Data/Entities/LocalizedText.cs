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
        public string Language { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
    }
}