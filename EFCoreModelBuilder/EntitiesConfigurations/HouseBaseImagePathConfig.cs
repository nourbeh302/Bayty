using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EFCoreModelBuilder.EntitiesConfigurations
{
    public class HouseBaseImagePathConfig : IEntityTypeConfiguration<HouseBaseImagePath>
    {
        public void Configure(EntityTypeBuilder<HouseBaseImagePath> builder)
        {
            builder.HasKey("Id", "ImagePath");

            builder.Property(ip => ip.ImagePath)
                   .HasColumnName("Image Path");
        }
    }
}
