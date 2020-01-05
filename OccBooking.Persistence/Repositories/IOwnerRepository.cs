using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Repositories
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<Owner> GetAsync(Guid id);
    }
}
