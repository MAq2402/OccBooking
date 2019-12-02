using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Repositories
{
    public class ReservationRequestRepository : Repository<ReservationRequest>, IReservationRequestRepository
    {
        public ReservationRequestRepository(OccBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ReservationRequest> GetReservationRequestAsync(Guid id)
        {
            return await _dbContext.ReservationRequests.Include(r => r.Place).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
