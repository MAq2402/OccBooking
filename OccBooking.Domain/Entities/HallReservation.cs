using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Domain.Helpers
{
    public class HallReservation : Entity
    {
        public static HallReservation CreateFromReservationRequest(ReservationRequest reservationRequest)
        {
            return new HallReservation();
        }
    }
}
