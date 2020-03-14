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
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        public HallRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(dbContext,
            eventDispatcher)
        {
        }

        public async Task<IEnumerable<Hall>> GetHallsAsync(Guid placeId)
        {
            return await _dbContext.Halls.Include(h => h.HallReservations)
                .Where(h => h.PlaceId == placeId).ToListAsync();
        }

        public async Task<IEnumerable<Hall>> GetHallsAsync(IEnumerable<Guid> ids)
        {
            return await _dbContext.Halls.Include(h => h.HallReservations)
                .Where(h => ids.Any(id => id == h.Id)).ToListAsync();
        }

        public async Task<Hall> GetHallAsync(Guid hallId)
        {
            return await _dbContext.Halls
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall)
                .FirstOrDefaultAsync(h => h.Id == hallId);
        }

        public async Task<HallJoin> GetJoinAsync(Hall first, Hall second)
        {
            return await _dbContext.HallJoins.Include(j => j.FirstHall).Include(j => j.SecondHall)
                .FirstOrDefaultAsync(j => j.ParticipatesIn(first) && j.ParticipatesIn(second));
        }

        public async Task RemoveHallJoinAsync(HallJoin joinToDelete)
        {
            await Task.FromResult(_dbContext.Remove(joinToDelete));
        }
    }
}