using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistance.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasMany(p => p.ReservationRequests)
                .WithOne(r => r.Place);

            builder.HasMany(p => p.Menus)
                .WithOne(m => m.Place);

            builder.HasMany(p => p.Halls)
                .WithOne(h => h.Place);

            builder.Property(p => p.AdditionalOptions)
                .HasConversion<string>(x => x.ToString(), y => (PlaceAdditionalOptions) y);

            var occasionalTypesToStringConverter = new ValueConverter<IEnumerable<OccasionType>, string>(
                v => string.Join(',', v),
                v => string.IsNullOrEmpty(v)
                    ? Enumerable.Empty<OccasionType>().ToHashSet()
                    : v.Split(',', StringSplitOptions.None)
                        .Select(x => (OccasionType) Enum.Parse(typeof(OccasionType), x)));

            builder.Property(p => p.AvailableOccasionTypes).HasConversion(occasionalTypesToStringConverter);

            builder.OwnsOne(p => p.Address);
        }
    }
}