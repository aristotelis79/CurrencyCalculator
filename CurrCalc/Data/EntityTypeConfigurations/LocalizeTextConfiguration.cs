using CurrCalc.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrCalc.Data.EntityTypeConfigurations
{
    ///<inheritdoc cref="LocalizedText"/>
    public class LocalizeTextConfiguration : IEntityTypeConfiguration<LocalizedText>
    {
        ///<inheritdoc cref="LocalizedText"/>
        public void Configure(EntityTypeBuilder<LocalizedText> builder)
        {
            builder.ToTable(nameof(LocalizedText))
                    .HasKey(k => k.Id);

            builder.Property(p => p.Language)
                .HasColumnType("char(2)");

            builder.HasIndex(i => new {i.Language, i.Key})
                .IsUnique()
                .HasName("idx_Lang_Key");
        }
    }
}