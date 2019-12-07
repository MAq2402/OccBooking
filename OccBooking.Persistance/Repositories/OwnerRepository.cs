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
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(dbContext,
            eventDispatcher)
        {
        }


        public async Task<Owner> GetAsync(Guid id)
        {
            return await _dbContext.Owners.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}