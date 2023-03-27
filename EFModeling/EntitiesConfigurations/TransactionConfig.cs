using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EFModeling.EntitiesConfigurations
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
             builder.HasOne(t => t.Initiator)
                    .WithOne()
                    .HasForeignKey<Transaction>(t => t.InitiatorId);

            builder.HasOne(t => t.Receiver)
                   .WithOne()
                   .HasForeignKey<Transaction>(t => t.ReceiverId);

            builder.Property(t => t.RentalCost)
                .HasColumnName("Rental Cost");
        }
    }
}
