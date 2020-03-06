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
        Task<IEnumerable<ReservationRequest>> GetImpossibleReservationRequestsAsync(Guid placeId, DateTime dateTime);
        Task<IEnumerable<ReservationRequest>> GetReservationRequestsAsync(Guid placeId, DateTime dateTime,
            bool isAnswered);
    }
}
