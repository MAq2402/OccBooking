using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class MenuOrderTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(int.MinValue)]
        public void CreationShouldThrowException_AmountOfPeople(int amountOfPeople)
        {
            Assert.Throws<DomainException>(() => new MenuOrder(CorrectMenu, amountOfPeople));
        }

        [Theory]
        [InlineData(1,100, 100)]
        [InlineData(10, 2, 20)]
        public void CalculateCostShouldWork(int amountOfPeople, decimal menuCost, decimal expectedCost)
        {
            var menuOrder = new MenuOrder(new Menu(new Guid(), "Some", MenuType.Normal, menuCost ), amountOfPeople);

            var actual = menuOrder.Cost;

            Assert.Equal(expectedCost, expectedCost);
        }
    }
}
