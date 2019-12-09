using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _dbContext.Places
                .Include(p => p.ReservationRequests)
                .ThenInclude(r => r.Place)
                .Include(p => p.Halls)
                .ThenInclude(h => h.HallReservations)
                .Include(p => p.Halls)
                .ThenInclude(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(p => p.Halls)
                .ThenInclude(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.Halls)
                .ThenInclude(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(p => p.Halls)
                .ThenInclude(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall)
                .Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Place> GetPlaceByHallAsync(Guid hallId)
        {
            return await _dbContext.Places.Include(p => p.ReservationRequests).ThenInclude(r => r.Place)
                .Include(p => p.Halls).Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Halls.Any(h => h.Id == hallId));
        }
    }
}