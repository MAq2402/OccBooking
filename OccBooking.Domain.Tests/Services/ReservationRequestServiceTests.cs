using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Moq;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Events;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.Services;
using OccBooking.Domain.ValueObjects;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Services
{
    public class ReservationRequestServiceTests
    {
        [Fact]
        public void ValidateMakeReservationRequest_ShouldThrowException_PlaceIsNotConfigured()
        {
            var mockedHallService = new Mock<IHallService>();
            var sut = new ReservationRequestService(mockedHallService.Object);
            var place = CorrectPlace;

            Action action = () => sut.ValidateMakeReservationRequest(place, null, x => false);

            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place dose not contain all required information for the reservation request",
                execption.Message);
        }

        [Fact(Skip = "Not implemented functionality")]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotHaveGivenMenu()
        {
            var amountOfPeople = 100;
            var mockedHallService = new Mock<IHallService>();
            mockedHallService.Setup(m => m.CalculateCapacity(It.IsAny<List<Hall>>(), It.IsAny<DateTime>()))
                .Returns(amountOfPeople);
            var sut = new ReservationRequestService(mockedHallService.Object);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, place.Id);
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, Guid.NewGuid());

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menuForReservation, amountOfPeople) },
                place.Id
            );

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation, x => true);

            var execption = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain some or all menus in reservation request", execption.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportSuchParty()
        {
            var amountOfPeople = 100;
            var mockedHallService = new Mock<IHallService>();
            mockedHallService.Setup(m => m.CalculateCapacity(It.IsAny<List<Hall>>(), It.IsAny<DateTime>()))
                .Returns(amountOfPeople);
            var sut = new ReservationRequestService(mockedHallService.Object);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, place.Id);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeople) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation, x => true);

            var excpetion = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not allow to organize such an events", excpetion.Message);
        }

        [Fact]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecausePlaceDoesNotSupportOption()
        {
            var amountOfPeople = 100;
            var mockedHallService = new Mock<IHallService>();
            mockedHallService.Setup(m => m.CalculateCapacity(It.IsAny<List<Hall>>(), It.IsAny<DateTime>()))
                .Returns(amountOfPeople);
            var sut = new ReservationRequestService(mockedHallService.Object);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, place.Id);

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));

            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Now,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>() { new PlaceAdditionalOption("Photos", 50) },
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeople) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation, x => true);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place dose not support those options", exception.Message);
        }

        [Theory(Skip = "Not implemented functionality")]
        [InlineData(101, 100)]
        public void ValidateMakeReservationRequestShouldThrowExceptionBecauseOfCapacity_2(
            int amountOfPeopleForReservation, int calculateCapacityResult)
        {
            var mockedHallService = new Mock<IHallService>();
            mockedHallService.Setup(m => m.CalculateCapacity(It.IsAny<List<Hall>>(), It.IsAny<DateTime>()))
                .Returns(calculateCapacityResult);
            var sut = new ReservationRequestService(mockedHallService.Object);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, place.Id);
            place.AllowParty(OccasionType.Wedding);
            var reservation = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, amountOfPeopleForReservation) },
                place.Id);

            Action action = () => sut.ValidateMakeReservationRequest(place, reservation, x => true);

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Making reservation on this date and with this amount of people is impossible",
                exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequest_PlaceDoesNotOwnHall_ShouldThrowException_()
        {
            var mockedHallService = new Mock<IHallService>();
            var sut = new ReservationRequestService(mockedHallService.Object);
            var menuCost = 50;
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, place.Id);
            place.AllowParty(OccasionType.Wedding);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);

            var hall4 = new Hall(Guid.NewGuid(), "Big", 30, new Guid());
            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation1, new List<Hall>() { hall4 });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Place does not contain given halls", exception.Message);
        }

        [Fact]
        public void ValidateAcceptReservationRequestShouldThrowException_EmptyHallList()
        {
            var mockedHallService = new Mock<IHallService>();
            var sut = new ReservationRequestService(mockedHallService.Object);
            var menuCost = 50;
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, place.Id);
            place.AllowParty(OccasionType.Wedding);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() { new MenuOrder(menu, 50) },
                place.Id);

            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation1, new List<Hall>() { });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Halls has not been provided", exception.Message);
        }

        [Fact(Skip = "Not implemented functionality")]
        public void ValidateAcceptReservationRequestShouldThrowException_ReservationIsForDifferentPlace()
        {
            var mockedHallService = new Mock<IHallService>();
            var sut = new ReservationRequestService(mockedHallService.Object);
            var menuCost = 50;
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, place.Id);
            place.AllowParty(OccasionType.Wedding);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            //place.AddHall(hall1);
            //place.AddHall(hall2);
            //place.AddHall(hall3);
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
            var mockedHallService = new Mock<IHallService>();
            var sut = new ReservationRequestService(mockedHallService.Object);
            var menuCost = 50;
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, place.Id);
            place.AllowParty(OccasionType.Wedding);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
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