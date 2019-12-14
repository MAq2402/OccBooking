using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistence.Configurations
{
    public class ReservationRequestConfiguration : IEntityTypeConfiguration<ReservationRequest>
    {
        public void Configure(EntityTypeBuilder<ReservationRequest> builder)
        {
            builder.HasMany(r => r.MenuOrders).WithOne(o => o.ReservationRequest);

            builder.OwnsOne(r => r.Client, ba => ba.OwnsOne(c => c.Name));
            builder.OwnsOne(r => r.Client, ba => ba.OwnsOne(c => c.Email));
            builder.OwnsOne(r => r.Client, ba => ba.OwnsOne(c => c.PhoneNumber));
            builder.OwnsOne(r => r.OccasionType);

            builder.Property(p => p.AdditionalOptions)
                .HasConversion<string>(x => x.ToString(), y => (PlaceAdditionalOptions) y);
        }
    }
}