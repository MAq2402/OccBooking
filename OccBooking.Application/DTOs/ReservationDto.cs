using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class ReservationDto
    {
        public bool IsEmpty { get; set; }
        public IEnumerable<HallReservationDto> HallReservations { get; set; }
    }
}
