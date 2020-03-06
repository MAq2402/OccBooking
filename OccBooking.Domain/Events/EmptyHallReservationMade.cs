using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public class EmptyHallReservationMade : IEvent
    {
        public EmptyHallReservationMade(Guid hallId, DateTime dateTime)
        {
            HallId = hallId;
            DateTime = dateTime;
        }

        public Guid HallId { get; }
        public DateTime DateTime { get; }
    }
}