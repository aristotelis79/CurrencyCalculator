using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrCalc.Data.EntityTypeConfigurations
{
    ///<inheritdoc cref="CurrencyExchangeRate"/>
    public class CurrencyExchangeRateConfiguration : IEntityTypeConfiguration<CurrencyExchangeRate>
    {
        ///<inheritdoc cref="CurrencyExchangeRate"/>
        public void Configure(EntityTypeBuilder<CurrencyExchangeRate> builder)
        {
            builder.ToTable(nameof(CurrencyExchangeRate))
                .HasKey(k => k.Id);

            builder.Property(p => p.Rate)
                .HasColumnType("decimal(18,4)");

            builder.Property(p => p.From)
                .IsRequired();

            builder.Property(p => p.To)
                .IsRequired();

            builder.HasOne(n => n.Target)
                .WithMany(x=>x.TargetCurrencyExchangeRates)
                .HasForeignKey(f => f.TargetId)
                .HasConstraintName("Currency_Target_FK")
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(n => n.Source)
                .WithMany(x=>x.SourceCurrencyExchangeRates)
                .HasForeignKey(n => n.SourceId)
                .HasConstraintName("Currency_Source_FK")
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasIndex(i => new { i.SourceId, i.TargetId, i.From, i.To })
                .IsUnique()
                .HasName("idx_Source_Target");
        }
    }
}