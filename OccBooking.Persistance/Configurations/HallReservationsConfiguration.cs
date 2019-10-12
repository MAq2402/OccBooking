using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Configurations
{
    public class HallReservationsConfiguration : IEntityTypeConfiguration<HallReservation>
    {
        public void Configure(EntityTypeBuilder<HallReservation> builder)
        {
            builder.HasKey(hr => new {hr.HallId, hr.ReservationId});
            builder.HasOne(hr => hr.Reservation)
                .WithMany(r => r.HallReservations)
                .HasForeignKey(hr => hr.ReservationId);
            builder.HasOne(hr => hr.Hall)
                .WithMany(h => h.HallReservations)
                .HasForeignKey(bc => bc.HallId);
        }
    }
}