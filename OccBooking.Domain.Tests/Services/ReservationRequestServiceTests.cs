using System;
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

namespace OccBooking.Domain.Tests.Services
{
    public class ReservationRequestServiceTests
    {
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

            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation1, new List<Hall>() { });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Halls has not been provided", exception.Message);
        }

        [Fact]
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

            reservation4.Accept(place.Id, new List<Guid>() { hall1.Id });
            hall1.MakeReservation(reservation4);
            Action action = () => sut.ValidateAcceptReservationRequest(place, reservation4, new List<Hall>() { hall1 });

            var exception = Assert.Throws<DomainException>(action);
            Assert.Equal("Some or all given halls are already reserved", exception.Message);
        }
    }
}
