using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistence.Configurations
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
                .HasConversion(x => x.ToString(), y => (PlaceAdditionalOptions) y);


            builder.Property(p => p.AvailableOccasionTypes)
                .HasConversion(x => x.ToString(), y => (OccasionTypes) y);

            builder.OwnsOne(p => p.Address);

            builder.HasMany(p => p.EmptyReservations)
                .WithOne(r => r.Place);
        }
    }
}