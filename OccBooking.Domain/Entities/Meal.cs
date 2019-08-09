using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Meal : Entity
    {
        private List<string> ingredients = new List<string>();
        public Meal(Guid id, string name, string description, MealType type, IEnumerable<string> ingredients) : base(id)
        {
            SetName(name);
            SetDescription(description);
            SetType(type);
            SetIngredients(ingredients);
        }
        public IEnumerable<string> Ingredients => ingredients.AsReadOnly();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MealType Type { get; private set; }
        public Menu Menu { get; set; }

        private void SetIngredients(IEnumerable<string> newIngredients)
        {
            if(!newIngredients.Any())
            {
                throw new DomainException("List of ingredients is empty");
            }
            ingredients = newIngredients.ToList();
        }

        private void SetType(MealType type)
        {
            Type = type;
        }

        private void SetDescription(string description)
        {
            Description = description;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Meal name has not been provided");
            }
            Name = name;
        }
    }
}
