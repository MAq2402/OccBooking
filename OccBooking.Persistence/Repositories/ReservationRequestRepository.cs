using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Repositories
{
    public class ReservationRequestRepository : Repository<ReservationRequest>, IReservationRequestRepository
    {
        public ReservationRequestRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(
            dbContext, eventDispatcher)
        {
        }

        public async Task<ReservationRequest> GetReservationRequestAsync(Guid id)
        {
            return await _dbContext.ReservationRequests.Include(r => r.Place).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}