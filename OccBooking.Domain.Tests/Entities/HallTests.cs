using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class HallTests
    {
        [Fact]
        public void AddPossibleJoinShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 10);
            var hallToJoin = new Hall(Guid.NewGuid(), "Big", 20);

            hall.AddPossibleJoin(hallToJoin);

            var expected = 1;

            Assert.Equal(expected, hall.PossibleJoins.Count());
            Assert.Equal(expected, hallToJoin.PossibleJoins.Count());
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecauseOfDuplicationOfPossibleJoins()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 10);
            var hallToJoin = new Hall(Guid.NewGuid(), "Big", 20);

            hall.AddPossibleJoin(hallToJoin);
            Action action = () => hall.AddPossibleJoin(hallToJoin);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecauseOfNullValue()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 10);

            Action action = () => hall.AddPossibleJoin(null);

            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(int.MinValue)]
        public void CreationOfHallShouldFail_Capacity(int capacity)
        {
            Assert.Throws<DomainException>(() => new Hall(Guid.NewGuid(), "Big", capacity));
        }

        [Fact]
        public void CreationOfHallShouldFail_Name()
        {
            Assert.Throws<DomainException>(() => new Hall(Guid.NewGuid(), "", 10));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public void CreationOfHallShouldWork(int capacity)
        {
            var hall = new Hall(Guid.NewGuid(), "Big", capacity);

            Assert.NotNull(hall);
        }

        [Fact]
        public void IsFreeOnDateShouldWork()
        {
            var hall = TestData.CorrectHall;
            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today, TestData.CorrectClient, OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>(), CorrectMenuOrders, Guid.NewGuid());
            hall.MakeReservation(reservation);

            Assert.True(hall.IsFreeOnDate(DateTime.Today.AddDays(1)));
            Assert.False(hall.IsFreeOnDate(DateTime.Today));
        }

        [Fact]
        public void MakeReservationShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 40);
            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today, TestData.CorrectClient, OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>(), CorrectMenuOrders, Guid.NewGuid());

            hall.MakeReservation(reservation);

            Assert.True(hall.HallReservations.Any());
        }
    }
}