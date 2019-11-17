using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Repositories
{
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        public HallRepository(OccBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Hall>> GetHallsAsync(Guid placeId)
        {
            return await _dbContext.Halls.Include(h => h.Place).Include(h => h.HallReservations)
                .Where(h => h.Place.Id == placeId).ToListAsync();
        }
    }
}