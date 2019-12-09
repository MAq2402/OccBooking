using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestAccepted : IEvent
    {
        public ReservationRequestAccepted(Guid reservationRequestId)
        {
            ReservationRequestId = reservationRequestId;
        }

        public Guid ReservationRequestId { get; }
    }
}
