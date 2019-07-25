using OccBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Menu : Entity
    {
        private List<Meal> meals = new List<Meal>();
        public Menu(string name, MenuType type, decimal cost)
        {
            Name = name;
            Type = type;
            Cost = cost;
        }
        public IEnumerable<Meal> Meals => meals.AsReadOnly();
        public string Name { get; private set; }
        public MenuType Type { get; private set; }
        public decimal Cost { get; private set; }
    }
}
