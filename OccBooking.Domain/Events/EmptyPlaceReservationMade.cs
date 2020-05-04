using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public class EmptyPlaceReservationMade : Event
    {
        public EmptyPlaceReservationMade(Guid placeId, DateTime dateTime) : base(placeId)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
    }
}
