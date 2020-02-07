using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrCalc.Data
{
    /// <summary>
    /// Represents Interface of application DB context
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T">Struct of Id of Base entity</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity,T>() where TEntity : BaseEntity<T> where T :struct;


        /// <inheritdoc cref="T:Microsoft.EntityFrameworkCore.SaveChangesAsync" />
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}