namespace CurrCalc.Data.Entities
{
    /// <summary>
    /// Base class for entities of Db
    /// </summary>
    public abstract class BaseEntity<T> where T : struct
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public T Id { get; set; }
    }
}