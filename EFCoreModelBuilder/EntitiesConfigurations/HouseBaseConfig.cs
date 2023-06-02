using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EFCoreModelBuilder.EntitiesConfigurations
{
    public class HouseBaseConfig : IEntityTypeConfiguration<HouseBase>
    {
        public void Configure(EntityTypeBuilder<HouseBase> builder)
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
