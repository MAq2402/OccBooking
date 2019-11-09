using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OccBooking.Domain.Entities
{
    public class Menu : AggregateRoot
    {
        private List<Meal> meals = new List<Meal>();

        public Menu(Guid id, string name, MenuType type, decimal costForPerson) : base(id)
        {
            SetName(name);
            SetType(type);
            SetCostForPerson(costForPerson);
        }

        private Menu()
        {
        }

        public IEnumerable<Meal> Meals => meals.AsReadOnly();
        public string Name { get; private set; }
        public MenuType Type { get; private set; }
        public decimal CostPerPerson { get; private set; }
        public Place Place { get; private set; }

        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new DomainException("Meal has not been provided");
            }

            meals.Add(meal);
        }

        private void SetType(MenuType type)
        {
            Type = type;
        }

        private void SetCostForPerson(decimal costForPerson)
        {
            if (costForPerson <= 0)
            {
                throw new DomainException("Menu cost has to be greater than 0");
            }

            CostPerPerson = costForPerson;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Menu name is not provided");
            }

            Name = name;
        }
    }
}