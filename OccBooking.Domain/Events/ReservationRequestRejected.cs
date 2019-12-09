using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestRejected : IEvent
    {
        public ReservationRequestRejected(Guid reservationRequestId)
        {
            ReservationRequestId = reservationRequestId;
        }

        public Guid ReservationRequestId { get; }
    }
}
