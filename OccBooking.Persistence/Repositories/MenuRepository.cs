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
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(OccBookingDbContext dbContext, IEventDispatcher eventDispatcher) : base(dbContext,
            eventDispatcher)
        {
        }

        public async Task<Menu> GetMenuAsync(Guid id)
        {
            return await _dbContext.Menus.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}