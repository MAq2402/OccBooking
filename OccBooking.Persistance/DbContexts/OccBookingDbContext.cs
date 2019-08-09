﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Helpers;

namespace OccBooking.Persistance.DbContexts
{
    public class OccBookingDbContext : DbContext
    {
        public OccBookingDbContext(DbContextOptions<OccBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Reservation> Reservations  { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<HallJoin> HallJoins{ get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<HallReservations> HallReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(OccBookingDbContext)));
        }
    }
}