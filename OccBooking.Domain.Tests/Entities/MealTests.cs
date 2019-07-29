using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class MealTests
    {
        [Theory]
        [InlineData("", "", MealType.Main, new[] {"Tomato"})]
        [InlineData("Vege", "", MealType.Main, new string[] {  })]
        public void CreationShouldFail(string name, string description, MealType type, string[] ingredients)
        {
            Action action = () => new Meal(Guid.NewGuid(), name, description, type, ingredients);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void CreationShouldWork()
        {
            var meal = new Meal(Guid.NewGuid(), "Vege", "", MealType.Main, new[] { "Tomato" });
            var expectedName = "Vege";
            var expectedDescription = "";
            var expectedType = MealType.Main;
            var expectedIngredients = new List<string>() { "Tomato" };

            Assert.Equal(expectedName, meal.Name);
            Assert.Equal(expectedDescription, meal.Description);
            Assert.Equal(expectedType, meal.Type);
            Assert.Equal(expectedIngredients[0], meal.Ingredients.ToArray()[0]);
        }
    }
}
