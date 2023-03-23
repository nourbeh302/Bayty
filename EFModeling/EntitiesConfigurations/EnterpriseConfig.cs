using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EF_Modeling.EntitiesConfigurations
{
    public class EnterpriseConfig : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.Property(e => e.PhoneNumber)
                .HasColumnName("Phone Number")
                .HasMaxLength(15);

            builder.Property(e => e.TaxRecordNumber)
                .HasColumnName("Tax Record Number")
                .IsFixedLength()
                .HasMaxLength(9);

        }
    }
}
