using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.Extensions;
using OccBooking.Persistence.Entities;

namespace OccBooking.Persistence.DbContexts
{
    public class OccBookingDbContext : IdentityDbContext
    {
        public OccBookingDbContext(DbContextOptions<OccBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<ReservationRequest> ReservationRequests { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<HallJoin> HallJoins { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<HallReservation> HallReservations { get; set; }
        public DbSet<User> OccBookingUsers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PlaceImage> PlaceImages { get; set; }
        public DbSet<MenuOrder> MenuOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(OccBookingDbContext)));
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}