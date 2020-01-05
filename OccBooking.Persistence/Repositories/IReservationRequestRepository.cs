using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Repositories
{
    public interface IReservationRequestRepository : IRepository<ReservationRequest>
    {
        Task<ReservationRequest> GetReservationRequestAsync(Guid id);
    }
}
