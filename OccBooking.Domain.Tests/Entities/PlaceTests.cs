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
using static OccBooking.Domain.Tests.DataFactories.IsPlaceConfiguredDataFactory;

namespace OccBooking.Domain.Tests.Entities
{
    public class PlaceTests
    {
        [Theory]
        [InlineData("")]
        public void CreationShouldFail(string name)
        {
            Assert.Throws<DomainException>(() =>
                new Place(Guid.NewGuid(), name, false, "", CorrectAddress));
        }

        [Fact]
        public void CreationShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, "Nice place", CorrectAddress);

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
        public void AddHallShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 10);
            var place = CorrectPlace;
            place.AddHall(hall);

            Assert.Contains(hall, place.Halls);
        }

        [Fact]
        public void AddHallShouldFail()
        {
            var place = CorrectPlace;

            Assert.Throws<DomainException>(() => place.AddHall(null));
        }

        [Theory]
        [InlineData(10, 10, new[] {10})]
        public void MakeReservationShouldWork(int menuCost, int amountOfPeople, int[] hallSizes)
        {
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople)});

            place.MakeReservationRequest(reservation);

            Assert.Contains(reservation, place.ReservationRequests);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotHaveGivenMenu()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menuForReservation, 100)}
            );

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain some or all menus in reservation request", execption.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportSuchParty()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 100)});

            Action action = () => place.ValidateMakeReservationRequest(reservation);

            var excpetion = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not allow to organize such an events", excpetion.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportOption()
        {
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>() {new PlaceAdditionalOption("Photos", 50)},
                new List<MenuOrder>() {new MenuOrder(menu, 100)});

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
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople1)});
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople2)});

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
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople1)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople2)});

            place.MakeReservationRequest(reservation1);
            place.AcceptReservationRequest(reservation1, new[] {hall1, hall2});
            Action action = () => place.ValidateMakeReservationRequest(reservation2);

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
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, amountOfPeople1)});

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

            var place = CorrectPlace;
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

            var place = CorrectPlace;
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
            var menuCost = 50;
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)});
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
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)});
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
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)});
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
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)});
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
            var place = CorrectPlace;
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
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)});
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)});
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
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = CorrectMenu;
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) });
            var reservation2 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) });
            var reservation3 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) });
            var reservation4 = new ReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 30) });
            place.MakeReservationRequest(reservation1);
            place.MakeReservationRequest(reservation2);
            place.MakeReservationRequest(reservation3);
            place.MakeReservationRequest(reservation4);

            place.AcceptReservationRequest(reservation1, new List<Hall>() { hall1, hall2 });
            place.MakeEmptyReservation(DateTime.Today);

            var expected = 1;
            var actual = place.EmptyReservations.Count();

            Assert.Equal(expected, actual);
            Assert.True(reservation2.IsRejected);
            Assert.True(reservation1.IsAccepted);
            Assert.False(reservation3.IsAnswered);
            Assert.True(reservation4.IsRejected);
        }

        [Theory]
        [ClassData(typeof(IsPlaceConfiguredShouldReturnFalseTestData))]
        public void IsPlaceConfiguredShouldReturnGivenResult(IEnumerable<Hall> halls, IEnumerable<Menu> menus,
            OccasionTypes occasionTypes, bool expected)
        {
            var place = CorrectPlace;
            foreach (var hall in halls)
            {
                place.AddHall(hall);
            }

            foreach (var menu in menus)
            {
                place.AssignMenu(menu);
            }

            foreach (var occasionType in occasionTypes)
            {
                place.AllowParty(occasionType);
            }

            var actual = place.IsConfigured;

            Assert.Equal(expected, actual);
        }

        private class IsPlaceConfiguredShouldReturnFalseTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return WithAllRequiredData;
                yield return WithoutHalls;
                yield return WithoutMenus;
                yield return WithoutOccasionTypes;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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

        [Fact]
        public void IsFreeShouldWork()
        {
            
        }
    }
}