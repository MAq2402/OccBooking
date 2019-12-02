using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class ReservationRequestDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ClientDto Client { get; set; }
        public string ClientEmail { get; set; }
        public decimal Cost { get; set; }
        public string Occasion { get; set; }
        public int AmountOfPeople { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; }
        public bool IsRejected { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAnswered { get; set; }
    }
}
