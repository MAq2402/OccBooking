using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(dbContext,
            eventDispatcher)
        {
        }

        public async Task<Place> GetPlaceAsync(Guid id)
        {
            return await _dbContext.Places.Include(p => p.ReservationRequests).ThenInclude(r => r.Place)
                .Include(p => p.Halls).Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}