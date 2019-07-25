using OccBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Meal : Entity
    {
        private List<string> ingredients = new List<string>();
        public Meal(Guid id, string name, string description, MealType type, IEnumerable<string> ingredients)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            this.ingredients = ingredients.ToList();
        }
        public IEnumerable<string> Ingredients => ingredients.AsReadOnly();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MealType Type { get; private set; }
    }
}
