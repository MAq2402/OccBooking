using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class ReservationTests
    {
        //[Fact]

        //public void Test1()
        //{
        //    var reservation = new Reservation();
        //    var place = new Place("", false, false, false, 10, 10, 100, "");
        //    var menu = new Menu();
        //    //place.MakeReservation(DateTime.Now, 10, )
        //    reservation.WholePlace = true;
        //    reservation.AmountOfPeople = 10;
        //    reservation.Menu.Cost = 10;
        //    reservation.Place = 

        //    var expected = reservation.Place.CostForRent + reservation.Menu.Cost * reservation.AmountOfPeople;
        //    var actual = 110;

        //    Assert.Equal(expected, actual);
        //}

        //[Theory]
        //[InlineData("")]
        //public void CreationShouldFail(DateTime dateTime, Client client, int amountOfPeople, )
        //{
        //    Action action = () => new Reservation()
        //}
        [Fact]
        public void AcceptShouldFailBecouseAccepted()
        {
            var reservation = new Reservation(Guid.NewGuid(), 
                DateTime.Today, 
                null, 
                10,
                null, 
                false, 
                PartyType.FuneralMeal, 
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept();
            Action action = () => reservation.Accept();

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldFailBecouseRejected()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                null,
                10,
                null,
                false,
                PartyType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();
            Action action = () => reservation.Accept();

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldWork()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                null,
                10,
                null,
                false,
                PartyType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept();

            Assert.True(reservation.IsAccepted);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsRejected);
        }

        [Fact]
        public void RejectShouldWork()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                null,
                10,
                null,
                false,
                PartyType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();

            Assert.True(reservation.IsRejected);
            Assert.True(reservation.IsAnswered);
            Assert.False(reservation.IsAccepted);
        }

        [Fact]
        public void RejectShouldFailBecouseRejected()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                null,
                10,
                null,
                false,
                PartyType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Reject();
            Action action = () => reservation.Reject();

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void RejectShouldFailBecouseAccepted()
        {
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                null,
                10,
                null,
                false,
                PartyType.FuneralMeal,
                Enumerable.Empty<PlaceAdditionalOption>());

            reservation.Accept();
            Action action = () => reservation.Reject();

            Assert.Throws<DomainException>(action);
        }
    }
}
