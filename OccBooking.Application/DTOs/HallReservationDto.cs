using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class HallReservationDto
    {
        public DateTime Date { get; set; }
        public string ClientEmail { get; set; }
        public decimal Cost { get; set; }
        public string Occasion { get; set; }
        public int AmountOfPeople { get; set; }
    }
}
