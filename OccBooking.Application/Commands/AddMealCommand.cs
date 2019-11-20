using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.Commands
{
    public class AddMealCommand : ICommand
    {
        public AddMealCommand(string name, string description, MealType type, IEnumerable<string> ingredients,
            Guid menuId)
        {
            Name = name;
            Description = description;
            Type = type;
            Ingredients = ingredients;
            MenuId = menuId;
        }

        public string Name { get; }
        public string Description { get; }
        public MealType Type { get; }
        public IEnumerable<string> Ingredients { get; }
        public Guid MenuId { get; }
    }
}