using OccBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Menu : Entity
    {
        private List<Meal> meals = new List<Meal>();
        public Menu(Guid id, string name, MenuType type, decimal costForPerson, IEnumerable<Meal> meals)
        {
            Id = id;
            Name = name;
            Type = type;
            CostForPerson = costForPerson;
            this.meals = meals.ToList();
        }
        public IEnumerable<Meal> Meals => meals.AsReadOnly();
        public string Name { get; private set; }
        public MenuType Type { get; private set; }
        public decimal CostForPerson { get; private set; }
    }
}
