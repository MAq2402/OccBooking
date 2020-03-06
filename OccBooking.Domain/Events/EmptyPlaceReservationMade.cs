using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public class EmptyPlaceReservationMade : IEvent
    {
        public EmptyPlaceReservationMade(Guid placeId, DateTime dateTime)
        {
            PlaceId = placeId;
            DateTime = dateTime;
        }

        public Guid PlaceId { get; }
        public DateTime DateTime { get; }
    }
}
