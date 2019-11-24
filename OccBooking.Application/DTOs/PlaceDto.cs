using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.DTOs
{
    public class PlaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasRooms { get; set; }
        public decimal CostPerPerson { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public IEnumerable<AdditionalOptionDto> AdditionalOptions { get; set; } = new List<AdditionalOptionDto>();
        public List<string> OccasionTypes { get; set; } = new List<string>();
        public string Image { get; set; }
        public bool IsConfigured { get; set; }
    }
}