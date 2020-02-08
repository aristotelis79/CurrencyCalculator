using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrCalc.Data.Repository
{
    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="T">Struct of Id of Base entity</typeparam>
    public class EfRepository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T> where T : struct
    {
        #region Fields

        private readonly IDbContext _context;

        private DbSet<TEntity> _entities;

        #endregion

        #region Properties
  
        /// <inheritdoc />
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <inheritdoc />
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities => _entities ??= _context.Set<TEntity,T>();

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Application sql server DB context </param>
        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken token = default)
        {
            return await Entities.FindAsync(new object[]{id},token)
                                    .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<int> InsertAsync(TEntity entity, bool saveChanges= true,  CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entities.AddAsync(entity,token).ConfigureAwait(false);

                if (!saveChanges) return 0;

                return await _context.SaveChangesAsync(token).ConfigureAwait(false);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }


        /// <inheritdoc />
        public virtual async Task<int> UpdateAsync(TEntity entity , bool saveChanges= true, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);

                if (!saveChanges) return 0;

                return await _context.SaveChangesAsync(token).ConfigureAwait(false);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <inheritdoc />
        public virtual async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }
            
            try
            { 
                var result =  _context.SaveChangesAsync().Result;
                return exception.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString(); 
            }
        }

        #endregion
    }
}