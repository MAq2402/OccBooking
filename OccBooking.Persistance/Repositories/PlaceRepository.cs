using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(OccBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Place> GetPlaceAsync(Guid id)
        {
            return await _dbContext.Places.Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}