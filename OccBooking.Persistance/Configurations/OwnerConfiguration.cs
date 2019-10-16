using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasMany(o => o.Places)
                   .WithOne(p => p.Owner);

            builder.OwnsOne(o => o.Email);
            builder.OwnsOne(o => o.PhoneNumber);
            builder.OwnsOne(o => o.Name);

            builder.Metadata.FindNavigation(nameof(Owner.Places))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}