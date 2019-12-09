using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.DTOs
{
    public class PlaceFilterDto
    {
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public decimal? MinCostPerPerson { get; set; }
        public decimal? MaxCostPerPerson { get; set; }
        public int? MinCapacity { get; set; }
        public string OccasionType { get; set; }
        public DateTimeOffset? FreeFrom { get; set; }
        public DateTimeOffset? FreeTo { get; set; }
    }
}
