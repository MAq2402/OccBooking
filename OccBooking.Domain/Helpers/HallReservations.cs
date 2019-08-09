using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Domain.Helpers
{
    public class HallReservations
    {
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
