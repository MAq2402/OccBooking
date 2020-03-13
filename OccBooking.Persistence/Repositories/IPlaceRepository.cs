using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Repositories
{
    public interface IPlaceRepository : IRepository<Place>
    {
        Task<Place> GetPlaceAsync(Guid id);
        Task<Place> GetPlaceByHallAsync(Guid hallId);
        bool IsPlaceConfigured(Guid id);
    }
}
