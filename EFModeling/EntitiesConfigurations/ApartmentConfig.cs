using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EF_Modeling.EntitiesConfigurations
{
    public class ApartmentConfig : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(a => a.RoomsCount)
                .HasColumnName("Rooms Count");

            builder.Property(a => a.KitchensCount)
                .HasColumnName("Kitchens Count");

            builder.Property(a => a.BathroomsCount)
                .HasColumnName("Bathrooms Count");
        }
    }
}
