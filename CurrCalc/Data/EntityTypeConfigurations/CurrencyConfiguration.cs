using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrCalc.Data.EntityTypeConfigurations
{
    ///<inheritdoc cref="Currency"/>
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        ///<inheritdoc cref="Currency"/>
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable(nameof(Currency))
                .HasKey(k => k.Id);

            builder.Property(p => p.IsoCode)
                .HasColumnType("char(3)")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Country)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.IsoNumber)
                .IsRequired();

            builder.HasIndex(i => i.IsoCode)
                .IsUnique()
                .HasName("idx_Currency_IsoCode");
        }
    }
}