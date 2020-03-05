using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Events;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.Services;
using OccBooking.Domain.ValueObjects;
using Xunit;
using static OccBooking.Domain.Tests.TestData;
using static OccBooking.Domain.Tests.DataFactories.IsPlaceConfiguredDataFactory;

namespace OccBooking.Domain.Tests.Services
{
    public class ReservationRequestServiceTests
    {
        [Theory]
        [ClassData(typeof(ValidateMakeReservationRequest_ShouldThrowException_PlaceDoesNotHaveGivenMenu_Data))]
        public void ValidateMakeReservationRequest_ShouldThrowException_PlaceDoesNotHaveGivenMenu(IEnumerable<Hall> halls, IEnumerable<Menu> menus,
            OccasionTypes occasionTypes, bool isConfiguredResult)
        {
            var sut = new ReservationRequestService();
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

            Action action = () => sut.ValidateMakeReservationRequest(place, null);

            Assert.False(isConfiguredResult);
            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place dose not contain all required information for the reservation request", execption.Message);
        }

        private class ValidateMakeReservationRequest_ShouldThrowException_PlaceDoesNotHaveGivenMenu_Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return WithoutHalls;
                yield return WithoutMenus;
                yield return WithoutOccasionTypes;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotHaveGivenMenu()
        {
            var sut = new ReservationRequestService();
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menuForReservation, 100) },
                place.Id
            );

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation);

            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain some or all menus in reservation request", execption.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportSuchParty()
        {
            var sut = new ReservationRequestService();
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 100) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation);

            var excpetion = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not allow to organize such an events", excpetion.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportOption()
        {
            var sut = new ReservationRequestService();
            var hall = new Hall(Guid.NewGuid(), "Big", 100);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>() { new PlaceAdditionalOption("Photos", 50) },
                new List<MenuOrder>() { new MenuOrder(menu, 100) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place dose not support those options", exception.Message);
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
            var sut = new ReservationRequestService();
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
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeople1) },
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeople2) },
                place.Id);

            hall1.MakeReservation(reservation1);
            hall2.MakeReservation(reservation2);
            Action action = () => sut.ValidateMakeReservationRequest(place, reservation2);

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
            var sut = new ReservationRequestService();
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeople1) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Making reservation on this date and with this amount of people is impossible",
                exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequest_PlaceDoesNotOwnHall_ShouldThrowException_()
        {
            var sut = new ReservationRequestService();
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
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation3 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation4 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 30) },
                place.Id);

            var hall4 = new Hall(Guid.NewGuid(), "Big", 30);
            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation1, new List<Hall>() { hall4 });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain given halls", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_EmptyHallList()
        {
            var sut = new ReservationRequestService();
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
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation3 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation4 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 30) },
                place.Id);

            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation1, new List<Hall>() { });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Halls has not been provided", exception.Message);
        }

        [Fact(Skip="Not implemented functionality")]
        public void ValidateAcceptReservationRequestShouldThrowException_ReservationIsForDifferentPlace()
        {
            var sut = new ReservationRequestService();
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
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation3 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation4 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 30) },
                place.Id);

            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation4, new List<Hall>() { hall1 });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Reservation does not belong to this place", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_HallAlreadyReserved()
        {
            var sut = new ReservationRequestService();
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
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation3 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);
            var reservation4 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 30) },
                place.Id);

            reservation4.Accept(place.Id, new List<Guid>() { hall1.Id });
            hall1.MakeReservation(reservation4);
            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation4, new List<Hall>() { hall1 });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Some or all given halls are already reserved", exception.Message);
        }
    }
}
