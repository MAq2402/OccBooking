using Moq;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class PlaceTests
    {
        [Theory]
        [InlineData("", 10)]
        [InlineData("Calvados", -1)]
        public void CreationShouldFail(string name, int costPerPerson)
        {
            Assert.Throws<DomainException>(() => new Place(Guid.NewGuid(), name, false, costPerPerson, "", CorrectAddress));
        }

        [Fact]
        public void CreationShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "Nice place", CorrectAddress);

            Assert.Equal("Calvados", place.Name);
            Assert.False(place.HasRooms);
            Assert.Equal(10, place.CostPerPerson);
            Assert.Equal("Nice place", place.Description);
        }

        [Fact]
        public void AssignMenuShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);

            Action action = () => place.AssignMenu(null);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);

            place.AllowParty(OccasionType.Wedding);

            Assert.Contains(OccasionType.Wedding, place.AvailableOccasionTypes);
        }

        [Fact]
        public void DisallowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);

            place.AllowParty(OccasionType.Wedding);
            place.AllowParty(OccasionType.FuneralMeal);
            place.DisallowParty(OccasionType.Wedding);

            Assert.DoesNotContain(OccasionType.Wedding, place.AvailableOccasionTypes);
            Assert.Contains(OccasionType.FuneralMeal, place.AvailableOccasionTypes);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void AddHallShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 10);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            place.AddHall(hall);

            Assert.Contains(hall, place.Halls);
        }

        [Fact]
        public void AddHallShouldFail()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);

            Assert.Throws<DomainException>(() => place.AddHall(null));
        }

        [Theory]
        [InlineData(10, 10, 10, new[] {10}, 200)]
        public void MakeReservationShouldWork(int menuCost, int placeCost, int amountOfPeople, int[] hallSizes,
            int expected)
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            foreach (var hallSize in hallSizes)
            {
                var hall = new Hall(Guid.NewGuid(), "Big", hallSize);
                place.AddHall(hall);
            }

            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservationRequest(reservation);
            var actual = reservation.Cost;

            Assert.Contains(reservation, place.ReservationRequests);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotHaveGivenMenu()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                TestData.CorrectClient,
                amountOfPeopleForReservation,
                menuForReservation,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain such a menu", execption.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportSuchParty()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                TestData.CorrectClient,
                amountOfPeopleForReservation,
                menu,
                OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>());

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var excpetion = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not allow to organize such an events", excpetion.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportOption()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                TestData.CorrectClient,
                amountOfPeopleForReservation,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>() {new PlaceAdditionalOption("Photos", 50)});

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place dose not support those options", exception.Message);
        }

        [Theory]
        [InlineData(10, 20, 29, 1)]
        [InlineData(10, 20, 1, 1)]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecauseOfCapacity_1(int hallSize1,
            int hallSize2,
            int amountOfPeople1,
            int amountOfPeople2)
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople2,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservationRequest(reservation);
            place.AcceptReservationRequest(reservation, new[] {hall1, hall2});
            Action action = () => place.ValidateMakeReservationRequest(reservation1);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Making reservation on this date and with this amount of people is impossible",
                exception.Message);
        }

        [Theory]
        [InlineData(10, 20, 10, 29, 11)]
        [InlineData(10, 20, 10, 1, 11)]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecauseOfCapacity_2(int hallSize1,
            int hallSize2,
            int hallSize3,
            int amountOfPeople1,
            int amountOfPeople2)
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var hall3 = new Hall(Guid.NewGuid(), "Big", hallSize3);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople2,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservationRequest(reservation);
            place.AcceptReservationRequest(reservation, new[] {hall1, hall2});
            Action action = () => place.ValidateMakeReservationRequest(reservation1);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Making reservation on this date and with this amount of people is impossible",
                exception.Message);
        }

        [Theory]
        [InlineData(10, 20, 31)]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecauseOfCapacity_3(int hallSize1,
            int hallSize2,
            int amountOfPeople1)
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Making reservation on this date and with this amount of people is impossible",
                exception.Message);
        }

        [Fact]
        public void CapacityShouldReturnCorrectValue_1()
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", 10);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 20);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);

            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);

            hall1.AddPossibleJoin(hall3);

            var expected = 40;
            var actual = place.Capacity;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CapacityShouldReturnCorrectValue_2()
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", 10);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 20);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);

            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "", CorrectAddress);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);

            hall1.AddPossibleJoin(hall3);
            hall3.AddPossibleJoin(hall2);

            var expected = 60;
            var actual = place.Capacity;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AcceptShouldWork()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);
            place.MakeReservationRequest(reservation4);

            place.AcceptReservationRequest(reservation1, new List<Hall>() {hall1, hall2});

            Assert.True(reservation2.IsRejected);
            Assert.True(reservation1.IsAccepted);
            Assert.False(reservation3.IsAnswered);
            Assert.False(reservation4.IsAnswered);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_PlaceDoesNotOwnHall()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);
            place.MakeReservationRequest(reservation4);

            var hall4 = new Hall(Guid.NewGuid(), "Big", 30);
            Action action = () => place.ValidateAcceptReservationRequest(reservation1, new List<Hall>() {hall4});

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain given halls", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_EmptyHallList()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);
            place.MakeReservationRequest(reservation4);

            Action action = () => place.AcceptReservationRequest(reservation1, new List<Hall>() { });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Halls has not been provided", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_ReservationIsForDifferentPlace()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);

            Action action = () => place.AcceptReservationRequest(reservation4, new List<Hall>() {hall1});

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Reservation does not belong to this place", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_HallAlreadyReserved()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "", CorrectAddress);
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                TestData.CorrectClient,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                TestData.CorrectClient,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);
            place.MakeReservationRequest(reservation4);

            place.AcceptReservationRequest(reservation4, new List<Hall>() {hall1});
            Action action = () => place.AcceptReservationRequest(reservation4, new List<Hall>() {hall1});

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Some or all given halls are already reserved", exception.Message);
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
    }
}