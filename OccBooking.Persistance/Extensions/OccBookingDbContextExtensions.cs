using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Entities;

namespace OccBooking.Persistance.Extensions
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
