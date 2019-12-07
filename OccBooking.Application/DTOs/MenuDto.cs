using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.DTOs
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal CostPerPerson { get; set; }
        public MenuType Type { get; set; }
        public IEnumerable<MealDto> Meals { get; set; } = new List<MealDto>();
    }
}
