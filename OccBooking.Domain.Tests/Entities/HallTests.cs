using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class HallTests
    {
        [Fact]
        public void AddPossibleJoinShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var hallToJoin = new Hall(Guid.NewGuid(), 20);

            hall.AddPossibleJoin(hallToJoin);

            var expected = 1;

            Assert.Equal(expected, hall.PossibleJoins.Count());
            Assert.Equal(expected, hallToJoin.PossibleJoins.Count());
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecauseOfDuplicationOfPossibleJoins()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var hallToJoin = new Hall(Guid.NewGuid(), 20);

            hall.AddPossibleJoin(hallToJoin);
            Action action = () => hall.AddPossibleJoin(hallToJoin);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecauseOfNullValue()
        {
            var hall = new Hall(Guid.NewGuid(), 10);

            Action action = () => hall.AddPossibleJoin(null);

            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(int.MinValue)]
        public void CreationOfHallShouldFail(int capacity)
        {
            Assert.Throws<DomainException>(() => new Hall(Guid.NewGuid(), capacity));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public void CreationOfHallShouldWork(int capacity)
        {
            var hall = new Hall(Guid.NewGuid(), capacity);

            Assert.NotNull(hall);
        }

        [Fact]
        public void IsFreeOnDateShouldWork()
        {
            var hall = TestData.CorrectHall;
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today, TestData.CorrectClient, 50, TestData.CorrectMenu,
                OccasionType.FuneralMeal, new List<PlaceAdditionalOption>());
            hall.MakeReservation(reservation, 1);

            Assert.True(hall.IsFreeOnDate(DateTime.Today.AddDays(1)));
            Assert.False(hall.IsFreeOnDate(DateTime.Today));
        }

        [Fact]
        public void MakeReservationShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), 40);
            var menu = new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard", MenuType.Vegetarian, 10,
                new List<Meal>
                    {new Meal(new Guid("9cf0705d-7734-454b-8e35-d205cfa99d6b"), "Dumplings", "Nice", MealType.Main, new List<string>() {"Cheese"})});
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today, TestData.CorrectClient, 50, TestData.CorrectMenu,
                OccasionType.FuneralMeal, new List<PlaceAdditionalOption>());

            hall.MakeReservation(reservation, 40);
            var actual = reservation.Cost * 40 / 50;
            var expected = hall.HallReservations.First().Cost;

            Assert.True(hall.HallReservations.Any());
            Assert.Equal(expected, actual);
        }
    }
}
