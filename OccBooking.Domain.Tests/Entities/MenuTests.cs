using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class MenuTests
    {
        [Theory]
        [MemberData(nameof(DataForCreationShouldFail))]
        public void CreationShouldFail(string name, MenuType type, int costForPerson, IEnumerable<Meal> meals) 
        {
            Action action = () => new Menu(Guid.NewGuid(), name, type, costForPerson, meals);

            Assert.Throws<DomainException>(action);
        }

        public static IEnumerable<object[]> DataForCreationShouldFail =>
            new List<object[]>
            {
                new object[] { "", MenuType.Vegetarian, 10, new[] { new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Tomato" }) } },
                new object[] { "Dumplingus", MenuType.Vegetarian, -10, new[] { new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Tomato" }) } },
                new object[] { "Dumplingus", MenuType.Vegetarian, 10, new List<Meal>() }
            };

        [Fact]
        public void CreationShouldWork()
        {
            var menu = new Menu(Guid.NewGuid(),
                "Dumplingus",
                MenuType.Vegetarian, 10,
                new[] { new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Tomato" }) });

            Assert.NotNull(menu);
        }
    }
}
