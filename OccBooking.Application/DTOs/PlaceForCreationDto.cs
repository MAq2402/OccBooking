using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class PlaceForCreationDto
    {
        public string Name { get; set; }
        public bool HasRooms { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
    }
}