using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EF_Modeling.EntitiesConfigurations
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(c => c.CVV)
                   .IsFixedLength()
                   .HasMaxLength(3);

            builder.Property(c => c.CardNumber)
                   .IsFixedLength()
                   .HasColumnName("Card Number")
                   .HasMaxLength(16);

            builder.Property(c => c.UserId)
                   .HasColumnName("Owner Id");

            builder.Property(c => c.ExpirationDate)
                   .HasColumnName("Expiration Date");
        }
    }
}
