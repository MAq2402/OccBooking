using Moq;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class PlaceTests
    {
        [Theory]
        [InlineData("")]
        public void CreationShouldFail(string name)
        {
            Assert.Throws<DomainException>(() =>
                new Place(Guid.NewGuid(), name, false, "", CorrectAddress, Guid.NewGuid()));
        }

        [Fact]
        public void CreationShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, "Nice place", CorrectAddress, Guid.NewGuid());

            Assert.Equal("Calvados", place.Name);
            Assert.False(place.HasRooms);
            Assert.Equal("Nice place", place.Description);
        }

        [Fact]
        public void AssignMenuShouldWork()
        {
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var place = CorrectPlace;

            Action action = () => place.AssignMenu(null);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = CorrectPlace;

            place.AllowParty(OccasionType.Wedding);

            Assert.Contains(OccasionType.Wedding, place.AvailableOccasionTypes);
        }

        [Fact]
        public void DisallowPartyShouldWork()
        {
            var place = CorrectPlace;

            place.AllowParty(OccasionType.Wedding);
            place.AllowParty(OccasionType.FuneralMeal);
            place.DisallowParty(OccasionType.Wedding);

            Assert.DoesNotContain(OccasionType.Wedding, place.AvailableOccasionTypes);
            Assert.Contains(OccasionType.FuneralMeal, place.AvailableOccasionTypes);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = CorrectPlace;
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void MakeEmptyReservationShouldWork()
        {
            var place = CorrectPlace;
            place.MakeEmptyReservation(DateTime.Today);

            var expected = 1;
            var actual = place.EmptyReservations.Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(MinimalCostPerPersonShouldReturnCorrectValueData))]
        public void MinimalCostPerPersonShouldReturnCorrectValue(decimal? expected, decimal[] menuCosts)
        {
            var place = CorrectPlace;
            foreach (var cost in menuCosts)
            {
                var menu = new Menu(Guid.NewGuid(), "Delicious", MenuType.Normal, cost);
                place.AssignMenu(menu);
            }

            var actual = place.MinimalCostPerPerson;

            Assert.Equal(expected, actual);
        }

        private class MinimalCostPerPersonShouldReturnCorrectValueData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {100.0m, new decimal[] {100}};
                yield return new object[] {null, new decimal[] { }};
                yield return new object[] {50.0m, new decimal[] {100, 50, 200, 300, 400, 51}};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}