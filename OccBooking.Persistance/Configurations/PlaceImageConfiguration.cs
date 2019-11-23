using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Persistance.Entities;

namespace OccBooking.Persistance.Configurations
{
    public class PlaceImageConfiguration : IEntityTypeConfiguration<PlaceImage>
    {
        public void Configure(EntityTypeBuilder<PlaceImage> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Place);
        }
    }
}
