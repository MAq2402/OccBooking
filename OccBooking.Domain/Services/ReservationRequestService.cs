using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.Services
{
    public class ReservationRequestService : IReservationRequestService
    {
        public void ValidateAcceptReservationRequest(Place place, ReservationRequest request, IEnumerable<Hall> halls)
        {
            if (!halls.Any())
            {
                throw new DomainException("Halls has not been provided");
            }

            if (!place.ReservationRequests.Contains(request))
            {
                throw new DomainException("Reservation does not belong to this place");
            }

            if (!place.ContainsHalls(halls))
            {
                throw new DomainException("Place does not contain given halls");
            }

            if (place.IsAnyHallReservedOnDate(request.DateTime, halls))
            {
                throw new DomainException("Some or all given halls are already reserved");
            }
        }
    }
}
