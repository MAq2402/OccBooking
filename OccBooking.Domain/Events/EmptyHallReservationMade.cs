using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public class EmptyHallReservationMade : IEvent
    {
        public EmptyHallReservationMade(Guid hallId)
        {
            HallId = hallId;
        }

        public Guid HallId { get; }
    }
}