using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EF_Modeling.EntitiesConfigurations
{
    public class BuildingConfig : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.Property(b => b.NumberOfFloors)
                .HasColumnName("Floors Count");

            builder.Property(b => b.NumberOfFlats)
                .HasColumnName("Flats Count");

            builder.Property(b => b.HasElevator)
                .HasColumnName("Has Elevator");

        }
    }
}
