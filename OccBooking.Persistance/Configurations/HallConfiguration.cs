using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasMany(h => h.HallReservations)
                .WithOne(hr => hr.Hall);

            builder.Metadata.FindNavigation(nameof(Hall.HallReservations))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(h => h.PossibleJoins);
        }
    }
}