using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EF_Modeling.EntitiesConfigurations
{
    public class ApartmentImagePathConfig : IEntityTypeConfiguration<ApartmentImagePath>
    {
        public void Configure(EntityTypeBuilder<ApartmentImagePath> builder)
        {
            builder.HasKey("Id", "ImagePath");

            builder.Property(ip => ip.ImagePath)
                   .HasColumnName("Image Path");
        }
    }
}
