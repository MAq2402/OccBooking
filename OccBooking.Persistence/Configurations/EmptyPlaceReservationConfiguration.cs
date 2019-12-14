using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Persistence.Configurations
{
    public class EmptyPlaceReservationConfiguration : IEntityTypeConfiguration<EmptyPlaceReservation>
    {
        public void Configure(EntityTypeBuilder<EmptyPlaceReservation> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
