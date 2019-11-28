using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.DTOs
{
    public class MakeReservationRequestDto
    {
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientEmail{ get; set; }
        public string ClientPhoneNumber { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<AdditionalOptionDto> Options { get; set; }
        public string OccasionType { get; set; }
        public IEnumerable<MenuOrderDto> MenuOrders { get; set; }
    }
}
