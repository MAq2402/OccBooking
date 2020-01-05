using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Repositories
{
    public interface IHallRepository : IRepository<Hall>
    {
        Task<IEnumerable<Hall>> GetHallsAsync(Guid placeId);
        Task<IEnumerable<Hall>> GetHallsAsync(IEnumerable<Guid> ids);
        Task<Hall> GetHallAsync(Guid hallId);
        Task<HallJoin> GetJoinAsync(Hall first, Hall second);
        Task RemoveHallJoinAsync(HallJoin joinToDelete);
    }
}
