using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestRejected : Event
    {
        public ReservationRequestRejected(Guid reservationRequestId) : base(reservationRequestId)
        {
        }
    }
}
