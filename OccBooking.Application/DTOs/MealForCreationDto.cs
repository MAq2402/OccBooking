using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.DTOs
{
    public class MealForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MealType Type { get; set; }
        public IEnumerable<string> Ingredients { get; set; } = new List<string>();
    }
}
