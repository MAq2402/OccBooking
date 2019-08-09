using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistance.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.Menu);
            builder.OwnsOne(r => r.Client,  ba => ba.OwnsOne(c => c.Name));
            builder.OwnsOne(r => r.Client, ba => ba.OwnsOne(c => c.Email));
            builder.OwnsOne(r => r.Client, ba => ba.OwnsOne(c => c.PhoneNumber));
            builder.Property(p => p.AdditionalOptions)
                .HasConversion<string>(x => x.ToString(), y => (PlaceAdditionalOptions)y);
        }
    }
}
