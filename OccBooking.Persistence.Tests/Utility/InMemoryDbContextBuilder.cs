using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Tests.Utility
{
    public static class InMemoryDbContextBuilder
    {
        public static OccBookingDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<OccBookingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var databaseContext = new OccBookingDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
