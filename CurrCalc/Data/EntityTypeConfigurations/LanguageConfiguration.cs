using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrCalc.Data.EntityTypeConfigurations
{
    /// <inheritdoc />
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable(nameof(Language))
                    .HasKey(k => k.Id);

            builder.Property(p=>p.LanguageCode)
                    .HasColumnType("char(2)")
                    .IsRequired();

            builder.HasIndex(i => i.LanguageCode)
                    .IsUnique()
                    .HasName("idx_LanguageCode");
        }
    }
}