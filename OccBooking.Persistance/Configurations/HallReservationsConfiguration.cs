using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Helpers;

namespace OccBooking.Persistance.Configurations
{
    public class HallReservationsConfiguration : IEntityTypeConfiguration<HallReservations>
    {
        public void Configure(EntityTypeBuilder<HallReservations> builder)
        {
            builder.HasKey(hr => new {hr.HallId, hr.ReservationId});
            builder.HasOne(hr => hr.Reservation)
                .WithMany(r => r.HallReservationes)
                .HasForeignKey(hr => hr.ReservationId);
            builder.HasOne(hr => hr.Hall)
                .WithMany(h => h.HallReservations)
                .HasForeignKey(bc => bc.HallId);
        }
    }
}