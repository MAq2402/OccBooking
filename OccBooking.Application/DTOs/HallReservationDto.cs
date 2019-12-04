using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class HallReservationDto
    {
        public HallDto Hall { get; set; }
        public ReservationRequestDto ReservationRequest { get; set; }
    }
}
