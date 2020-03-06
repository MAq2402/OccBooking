using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Services;
using OccBooking.Domain.ValueObjects;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Services
{
    public class HallServiceTests
    {
        [Theory]
        [InlineData(10, 20, 10, 10)]
        [InlineData(10, 20, 30, 30)]
        public void CalculateCapacity_ShouldWork_1(int hallSize1, int hallSize2, int hallSize3, int expected)
        {
            var sut = new HallService();
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var hall3 = new Hall(Guid.NewGuid(), "Big", hallSize3);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var place = CorrectPlace;
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            hall1.AddPossibleJoin(hall2);
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 1)},
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 1)},
                place.Id);
            hall1.MakeReservation(reservation1);
            hall2.MakeReservation(reservation2);

            var actual = sut.CalculateCapacity(new List<Hall>() {hall1, hall2, hall3}, DateTime.Today);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 20, 30)]
        public void CalculateCapacity_ShouldWork_2(int hallSize1, int hallSize2, int expected)
        {
            var sut = new HallService();
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var place = CorrectPlace;
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);

            var actual = sut.CalculateCapacity(new List<Hall>() {hall1, hall2}, DateTime.Today);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateCapacity_ShouldWork_3()
        {
            var sut = new HallService();

            var actual = sut.CalculateCapacity(new List<Hall>() { }, DateTime.Today);
            var expected = 0;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 20, 30, 40, 50)]
        [InlineData(10, 20, 30, 51, 51)]
        public void CalculateCapacity_ShouldWork_4(int hallSize1, int hallSize2, int hallSize3, int hallSize4,
            int expected)
        {
            var sut = new HallService();
            var hall1 = new Hall(Guid.NewGuid(), "Big", hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), "Big", hallSize2);
            var hall3 = new Hall(Guid.NewGuid(), "Big", hallSize3);
            var hall4 = new Hall(Guid.NewGuid(), "Big", hallSize4);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100);
            var place = CorrectPlace;
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 1)},
                place.Id);
            hall1.MakeReservation(reservation1);

            var actual = sut.CalculateCapacity(new List<Hall>() {hall1, hall2, hall3, hall4}, DateTime.Today);

            Assert.Equal(expected, actual);
        }
    }
}