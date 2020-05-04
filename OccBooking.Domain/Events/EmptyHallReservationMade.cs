using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public class EmptyHallReservationMade : Event
    {
        public EmptyHallReservationMade(Guid hallId, DateTime dateTime) : base(hallId)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
    }
}