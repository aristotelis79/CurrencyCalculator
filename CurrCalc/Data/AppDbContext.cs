using CurrCalc.Data.Entities;
using CurrCalc.Data.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurrCalc.Data
{
    /// <summary>
    /// Implementation of SqlServer DB context of application
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser>, IDbContext
    {
        #region ctor

        ///<inheritdoc />
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion


        #region Methods
        ///<inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyExchangeRateConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizeTextConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        ///<inheritdoc />
        public DbSet<TEntity> Set<TEntity,T>() where TEntity : BaseEntity<T> where T:struct
        {
            return base.Set<TEntity>();
        }

        #endregion
    }
}