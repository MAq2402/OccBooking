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
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class ReservationRequestTests
    {
        [Theory]
        [ClassData(typeof(DataForCreationShouldFail))]
        public void CreationShouldFail(DateTime dateTime, Client client)
        {
            Action action = () => ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                dateTime,
                client,
                OccasionType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>(),
                CorrectMenuOrders,
                Guid.NewGuid());

            Assert.Throws<DomainException>(action);
        }

        public class DataForCreationShouldFail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                    {DateTime.Today.AddDays(-1), TestData.CorrectClient};
                yield return new object[] {DateTime.Today, null};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void AcceptShouldFailBecauseAccepted()
        {
            var reservation = CorrectReservationRequest;

            reservation.Accept(Guid.NewGuid(), new List<Guid>() {Guid.NewGuid()});
            Action action = () => reservation.Accept(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldFailBecauseRejected()
        {
            var reservation = CorrectReservationRequest;

            reservation.Reject();
            Action action = () => reservation.Accept(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldWork()
        {
            var reservation = CorrectReservationRequest;

            reservation.Accept(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

            Assert.True(reservation.IsAccepted);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsRejected);
        }

        [Fact]
        public void RejectShouldWork()
        {
            var reservation = CorrectReservationRequest;

            reservation.Reject();

            Assert.True(reservation.IsRejected);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsAccepted);
        }

        [Fact]
        public void RejectShouldFailBecauseRejected()
        {
            var reservation = CorrectReservationRequest;

            reservation.Reject();
            Action action = () => reservation.Reject();

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void RejectShouldFailBecauseAccepted()
        {
            var reservation = CorrectReservationRequest;

            reservation.Accept(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });
            Action action = () => reservation.Reject();

            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [ClassData(typeof(DataForReservationCreation))]
        public void CalculateCostShouldWork(IEnumerable<MenuOrder> menuOrders, PlaceAdditionalOptions options,
            decimal expectedCost,
            int expectedAmountOfPeople)
        {
            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today, CorrectClient,
                OccasionType.FuneralMeal,
                options,
                menuOrders,
                Guid.NewGuid());

            var actualCost = reservation.Cost;
            var actualAmountOfPeople = reservation.AmountOfPeople;

            Assert.Equal(expectedCost, actualCost);
            Assert.Equal(expectedAmountOfPeople, actualAmountOfPeople);
        }

        private class DataForReservationCreation : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<MenuOrder>()
                    {
                        new MenuOrder(new Menu(Guid.NewGuid(), "Normal", MenuType.Normal, 100), 100),
                        new MenuOrder(new Menu(Guid.NewGuid(), "VegeTFU", MenuType.Vegetarian, 200), 5)
                    },
                    new PlaceAdditionalOptions(new List<PlaceAdditionalOption>()
                        {new PlaceAdditionalOption("Some", 1000), new PlaceAdditionalOption("Some", 500)}),
                    12500,
                    105
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}