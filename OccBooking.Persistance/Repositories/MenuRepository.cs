using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Persistance.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(OccBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Menu> GetMenuAsync(Guid id)
        {
            return await _dbContext.Menus.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
