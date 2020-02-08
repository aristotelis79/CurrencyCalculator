using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;

namespace CurrCalc.Data.Repository
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="T">Primitive type for Id of Entity</typeparam>
    public interface IRepository<TEntity, in T> where TEntity : BaseEntity<T> where T : struct
    {
        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <param name="token"> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity,bool saveChanges= true, CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <param name="token"> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, bool saveChanges= true, CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}