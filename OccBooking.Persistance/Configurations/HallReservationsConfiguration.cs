using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistance.Configurations
{
    public class HallReservationsConfiguration : IEntityTypeConfiguration<HallReservation>
    {
        public void Configure(EntityTypeBuilder<HallReservation> builder)
        {
            builder.HasOne(hr => hr.Menu);

            builder.Property(hr => hr.AdditionalOptions)
                .HasConversion<string>(x => x.ToString(), y => (PlaceAdditionalOptions)y);

            builder.OwnsOne(hr => hr.Client, ba => ba.OwnsOne(c => c.Name));
            builder.OwnsOne(hr => hr.Client, ba => ba.OwnsOne(c => c.Email));
            builder.OwnsOne(hr => hr.Client, ba => ba.OwnsOne(c => c.PhoneNumber));
        }
    }
}