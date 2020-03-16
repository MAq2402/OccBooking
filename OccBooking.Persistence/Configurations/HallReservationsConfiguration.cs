using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistence.Configurations
{
    public class HallReservationsConfiguration : IEntityTypeConfiguration<HallReservation>
    {
        public void Configure(EntityTypeBuilder<HallReservation> builder)
        {
            builder.HasOne<ReservationRequest>().WithMany().HasForeignKey(hr => hr.ReservationRequestId);
        }
    }
}