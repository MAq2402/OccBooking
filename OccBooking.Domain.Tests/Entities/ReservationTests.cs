using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class ReservationTests
    {
        private static Client correctClient = new Client("Michal", "Kowalski", "michal@michal.com", "505111111");

        private static Menu correctMenu = new Menu(Guid.NewGuid(), "Standard", MenuType.Vegetarian, 10,
            new List<Meal>
                {new Meal(Guid.NewGuid(), "Dumplings", "Nice", MealType.Main, new List<string>() {"Cheese"})});

        [Theory]
        [ClassData(typeof(DataForCreationShouldFail))]
        public void CreationShouldFail(DateTime dateTime, Client client, int amountOfPeople, Menu menu)
        {
            Action action = () => new Reservation(Guid.NewGuid(),
                dateTime,
                client,
                amountOfPeople,
                menu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            Assert.Throws<DomainException>(action);
        }

        public class DataForCreationShouldFail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {DateTime.Today.AddDays(-1), correctClient, 50, correctMenu};
                yield return new object[] {DateTime.Today, null, 50, correctMenu};
                yield return new object[] {DateTime.Today, correctClient, 0, correctMenu};
                yield return new object[] {DateTime.Today, correctClient, 50, null};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void CreationShouldWork()
        {
            var reservation = new Reservation(Guid.NewGuid(), DateTime.Today, correctClient, 50, correctMenu,
                OccasionType.FuneralMeal, new List<PlaceAdditionalOption>());

            Assert.Equal(DateTime.Today, reservation.DateTime);
            Assert.Equal(correctClient, reservation.Client);
            Assert.Equal(50, reservation.AmountOfPeople);
            Assert.Equal(correctMenu, reservation.Menu);
            Assert.Equal(OccasionType.FuneralMeal, reservation.OccasionType);
        }

        [Fact]
        public void AcceptShouldFailBecauseAccepted()
        {
            var halls = new List<Hall>() {new Hall(Guid.NewGuid(), 10)};
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept(halls);
            Action action = () => reservation.Accept(halls);

            Assert.Throws<ReservationHasBeenAlreadyAnsweredException>(action);
        }

        [Fact]
        public void AcceptShouldFailBecauseRejected()
        {
            var halls = new List<Hall>() {new Hall(Guid.NewGuid(), 10)};
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();
            Action action = () => reservation.Accept(halls);

            Assert.Throws<ReservationHasBeenAlreadyAnsweredException>(action);
        }

        [Fact]
        public void AcceptShouldWork()
        {
            var halls = new List<Hall>() {new Hall(Guid.NewGuid(), 10)};
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept(halls);

            Assert.True(reservation.IsAccepted);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsRejected);
            Assert.True(halls.All(h => reservation.Halls.Contains(h)));
        }

        [Fact]
        public void RejectShouldWork()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();

            Assert.True(reservation.IsRejected);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsAccepted);
        }

        [Fact]
        public void RejectShouldFailBecauseRejected()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();
            Action action = () => reservation.Reject();

            Assert.Throws<ReservationHasBeenAlreadyAnsweredException>(action);
        }

        [Fact]
        public void RejectShouldFailBecauseAccepted()
        {
            var halls = new List<Hall>() {new Hall(Guid.NewGuid(), 10)};
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                10,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept(halls);
            Action action = () => reservation.Reject();

            Assert.Throws<ReservationHasBeenAlreadyAnsweredException>(action);
        }

        [Theory]
        [InlineData(10, 20, 30, 61)]
        [InlineData(10, 10, 10, 40)]
        public void AcceptShouldFailBecauseHallsCapacity(int capacity1, int capacity2, int capacity3,
            int amountOfPeople)
        {
            var halls = new List<Hall>()
            {
                new Hall(Guid.NewGuid(), capacity1),
                new Hall(Guid.NewGuid(), capacity2),
                new Hall(Guid.NewGuid(), capacity3)
            };
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                amountOfPeople,
                correctMenu,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            Action action = () => reservation.Accept(halls);

            Assert.Throws<ToSmallCapacityException>(action);
        }

        [Theory]
        [InlineData(10, 10, new[] {5, 5, 5}, 10, 215)]
        [InlineData(5, 1, new[] {0}, 200, 1005)]
        public void CalculateCostShouldWork(int amountOfPeople, decimal menuCost, int[] optionsCosts, int placeCost,
            int expected)
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vege", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            var options = new PlaceAdditionalOptions(Enumerable.Empty<PlaceAdditionalOption>());
            foreach (var optionCost in optionsCosts)
            {
                options = options.AddOption(new PlaceAdditionalOption("Some", optionCost));
            }

            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                correctClient,
                amountOfPeople,
                menu,
                OccasionType.FuneralMeal,
                options);

            reservation.CalculateCost(placeCost);

            var actual = reservation.Cost;

            Assert.Equal(expected, actual);
        }
    }
}