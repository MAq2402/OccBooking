using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Domain.Services
{
    public interface IReservationRequestService
    {
        void ValidateAcceptReservationRequest(Place place, ReservationRequest request, IEnumerable<Hall> halls);
        void ValidateMakeReservationRequest(Place place, ReservationRequest request);
    }
}
