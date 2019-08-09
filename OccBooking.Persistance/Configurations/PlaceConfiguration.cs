using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;

namespace OccBooking.Persistance.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasMany(p => p.Reservations)
                .WithOne(r => r.Place);

            builder.HasMany(p => p.Menus)
                .WithOne(m => m.Place);

            builder.HasMany(p => p.Halls)
                .WithOne(h => h.Place);

            builder.OwnsOne(p => p.AdditionalOptions);

            var occasionalTypesToStringConverter = new ValueConverter<IEnumerable<OccasionType>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.None)
                    .Select(x => (OccasionType) Enum.Parse(typeof(OccasionType), x)));

            builder.Property(o => o.AvailableOccasionTypes).HasConversion<string>(occasionalTypesToStringConverter);
        }
    }
}