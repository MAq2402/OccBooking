using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestCreated : IEvent
    {
        public ReservationRequestCreated(Guid reservationRequestId)
        {
            ReservationRequestId = reservationRequestId;
        }

        public Guid ReservationRequestId { get; }
    }
}
