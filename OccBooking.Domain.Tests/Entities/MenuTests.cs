using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class MenuTests
    {
        [Theory]
        [MemberData(nameof(DataForCreationShouldFail))]
        public void CreationShouldFail(string name, MenuType type, int costForPerson)
        {
            Action action = () => new Menu(Guid.NewGuid(), name, type, costForPerson);

            Assert.Throws<DomainException>(action);
        }

        public static IEnumerable<object[]> DataForCreationShouldFail =>
            new List<object[]>
            {
                new object[] {"", MenuType.Vegetarian, 10},
                new object[] {"Dumplingus", MenuType.Vegetarian, -10},
            };

        [Fact]
        public void CreationShouldWork()
        {
            var menu = new Menu(Guid.NewGuid(),
                "Dumplingus",
                MenuType.Vegetarian, 10);

            Assert.NotNull(menu);
        }

        [Fact]
        public void AddMenuShouldFail()
        {
            var menu = CorrectMenu;
            Assert.Throws<DomainException>(() => menu.AddMeal(null));
        }

        [Fact]
        public void AddMenuShouldWork()
        {
            var menu = CorrectMenu;
            var meal = new Meal(Guid.NewGuid(), "Very good", "", MealType.Main, new List<string>()
            {
                "Tomatoes"
            });
            menu.AddMeal(meal);

            var expected = 1;
            var actual = menu.Meals.Count();

            Assert.Equal(expected, actual);
        }
    }
}