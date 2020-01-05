using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Entities;

namespace OccBooking.Persistence.Extensions
{
    public static class OccBookingDbContextExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient(1, "Fasola"),
                new Ingredient(2, "Ziemniaki")
            );
        }
    }
}
