using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OccBooking.Domain.Entities
{
    public class Menu : Entity
    {
        private List<Meal> meals = new List<Meal>();
        private MenuType vegetarian;
        private List<Meal> list;

        public Menu(Guid id, string name, MenuType type, decimal costForPerson, IEnumerable<Meal> meals) : base(id)
        {
            SetName(name);
            SetType(type);
            SetCostForPerson(costForPerson);
            SetMeals(meals);
        }

        private void SetMeals(IEnumerable<Meal> meals)
        {
            if (!meals.Any())
            {
                throw new DomainException("Menu without meals can not be created");
            }
            this.meals = meals.ToList();
        }
        private void SetType(MenuType type)
        {
            Type = type;
        }
        private void SetCostForPerson(decimal costForPerson)
        {
           if(costForPerson <= 0)
           {
                throw new DomainException("Menu cost has to be greater than 0");
           }
           CostForPerson = costForPerson;
        }
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Menu name is not provided");
            }
            Name = name;
        }
        public IEnumerable<Meal> Meals => meals.AsReadOnly();
        public string Name { get; private set; }
        public MenuType Type { get; private set; }
        public decimal CostForPerson { get; private set; }
    }
}
