using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestAccepted : Event
    {
        public ReservationRequestAccepted(Guid reservationRequestId, Guid placeId, IEnumerable<Guid> hallIds) : base(reservationRequestId)
        {
            PlaceId = placeId;
            HallIds = hallIds;
        }

        public Guid PlaceId { get; }
        public IEnumerable<Guid> HallIds { get; }
    }
}
