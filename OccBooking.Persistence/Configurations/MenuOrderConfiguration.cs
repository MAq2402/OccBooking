using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Configurations
{
    public class MenuOrderConfiguration : IEntityTypeConfiguration<MenuOrder>
    {
        public void Configure(EntityTypeBuilder<MenuOrder> builder)
        {
            builder.HasOne<Menu>().WithMany().HasForeignKey(mo => mo.MenuId);
        }
    }
}
