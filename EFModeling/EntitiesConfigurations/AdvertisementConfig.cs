﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace EFModeling.EntitiesConfigurations
{
    public class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property("Title")
                .HasMaxLength(140);

            builder.Property("Description")
                .HasMaxLength(140);
        }
    }
}
