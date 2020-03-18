using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(dbContext,
            eventDispatcher)
        {
        }

        public async Task<Place> GetPlaceAsync(Guid id)
        {
            return await _dbContext.Places
                .Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Place> GetPlaceByHallAsync(Guid hallId)
        {
            var hall = await _dbContext.Halls.FirstOrDefaultAsync(h => h.Id == hallId);
            return await _dbContext.Places.Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => hall.PlaceId == p.Id);
        }

        public bool IsPlaceConfigured(Guid id)
        {
            var place = GetPlaceAsync(id).Result;
            var placeHasHalls = _dbContext.Halls.Any(h => h.PlaceId == id);
            var placeHasMenus = _dbContext.Menus.Any(m => m.PlaceId == id);
            return placeHasMenus && placeHasHalls && place.AvailableOccasionTypes.Any();
        }
    }
}