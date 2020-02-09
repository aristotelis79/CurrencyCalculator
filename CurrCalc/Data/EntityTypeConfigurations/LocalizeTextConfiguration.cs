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

            builder.HasOne(n => n.Language)
                .WithMany(x => x.LocalizedTexts)
                .HasForeignKey(f => f.LanguageId)
                .HasConstraintName("FK_Language_LocalizedTexts")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(i => new { i.LanguageId, i.Key })
                    .IsUnique()
                    .HasName("idx_LanguageId_Key");
        }
    }
}