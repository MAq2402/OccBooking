using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.DTOs
{
    public class MenuForCreationDto
    {
        public string Name { get; set; }
        public MenuType Type { get; set; }
        public decimal CostPerPerson { get; set; }
    }
}
