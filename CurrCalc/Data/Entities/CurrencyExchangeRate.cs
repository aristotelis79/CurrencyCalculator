using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CurrCalc.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrencyExchangeRate : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime To { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public virtual Currency Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Currency Target { get; set; }
    }
}