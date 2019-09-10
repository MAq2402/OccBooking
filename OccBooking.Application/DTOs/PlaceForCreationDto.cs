using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class PlaceForCreationDto
    {
        public string Name { get; set; }
        public bool HasRooms { get; set; }
        public decimal CostPerPerson { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
    }
}
