using Moq;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class PlaceTests
    {
        private Client client = new Client("Michal", "Miciak", "michal@miciak.com", "511111111");

        [Theory]
        [InlineData("", 10)]
        [InlineData("Calvados", -1)]
        public void CreationShouldFail(string name, int costPerPerson)
        {
            Assert.Throws<DomainException>(() => new Place(Guid.NewGuid(), name, false, costPerPerson, ""));
        }

        [Fact]
        public void CreationShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "Nice place");

            Assert.Equal("Calvados", place.Name);
            Assert.Equal(false, place.HasRooms);
            Assert.Equal(10, place.CostPerPerson);
            Assert.Equal("Nice place", place.Description);
        }

        [Fact]
        public void AssignMenuShouldWork()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");

            Action action = () => place.AssignMenu(null);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");

            place.AllowParty(OccasionType.Wedding);

            Assert.Contains(OccasionType.Wedding, place.AvailableOccasionTypes);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void AddHallShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            place.AddHall(hall);

            Assert.Contains(hall, place.Halls);
        }

        [Fact]
        public void AddHallShouldFail()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");

            Assert.Throws<DomainException>(() => place.AddHall(null));
        }

        [Theory]
        [InlineData(10, 10, 10, new[] {10}, 200)]
        public void MakeReservationShouldWork(int menuCost, int placeCost, int amountOfPeople, int[] hallSizes,
            int expected)
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            foreach (var hallSize in hallSizes)
            {
                var hall = new Hall(Guid.NewGuid(), hallSize);
                place.AddHall(hall);
            }

            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);

            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservation(reservation);
            var actual = reservation.Cost;

            Assert.Contains(reservation, place.Reservations);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeReservationShouldFailBecausePlaceDoesNotHaveGivenMenu()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100,
                new List<Meal>() {meal});
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Now,
                client,
                amountOfPeopleForReservation,
                menuForReservation,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            Action action = () => place.MakeReservation(reservation);

            Assert.Throws<LackOfMenuException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecausePlaceDoesNotSupportSuchParty()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Now,
                client,
                amountOfPeopleForReservation,
                menu,
                OccasionType.FuneralMeal,
                new List<PlaceAdditionalOption>());

            Action action = () => place.MakeReservation(reservation);

            Assert.Throws<PartyIsNotAvaliableException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecausePlaceDoesNotSupportOption()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            var amountOfPeopleForReservation = 50;

            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);

            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Now,
                client,
                amountOfPeopleForReservation,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>() {new PlaceAdditionalOption("Photos", 50)});

            Action action = () => place.MakeReservation(reservation);

            Assert.Throws<NotSupportedAdditionalOptionException>(action);
        }

        [Theory]
        [InlineData(10, 20, 29, 1)]
        [InlineData(10, 20, 1, 1)]
        public void MakeReservationShouldFailBecauseOfImpossibleReservation_1(int hallSize1,
            int hallSize2,
            int amountOfPeople1,
            int amountOfPeople2)
        {
            var hall1 = new Hall(Guid.NewGuid(), hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), hallSize2);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople2,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservation(reservation);
            place.AcceptReservation(reservation, new[] {hall1, hall2});
            Action action = () => place.MakeReservation(reservation1);

            Assert.Throws<ReservationIsImpossibleException>(action);
        }

        [Theory]
        [InlineData(10, 20, 10, 29, 11)]
        [InlineData(10, 20, 10, 1, 11)]
        public void MakeReservationShouldFailBecouseOfImpossibleReservation_2(int hallSize1,
            int hallSize2,
            int hallSize3,
            int amountOfPeople1,
            int amountOfPeople2)
        {
            var hall1 = new Hall(Guid.NewGuid(), hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), hallSize2);
            var hall3 = new Hall(Guid.NewGuid(), hallSize3);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople2,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservation(reservation);
            place.AcceptReservation(reservation, new[] {hall1, hall2});
            Action action = () => place.MakeReservation(reservation1);

            Assert.Throws<ReservationIsImpossibleException>(action);
        }

        [Theory]
        [InlineData(10, 20, 31)]
        public void MakeReservationShouldFailBecouseOfImpossibleReservation_3(int hallSize1,
            int hallSize2,
            int amountOfPeople1)
        {
            var hall1 = new Hall(Guid.NewGuid(), hallSize1);
            var hall2 = new Hall(Guid.NewGuid(), hallSize2);
            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            place.AddHall(hall1);
            place.AddHall(hall2);
            var reservation = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                amountOfPeople1,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            Action action = () => place.MakeReservation(reservation);

            Assert.Throws<ReservationIsImpossibleException>(action);
        }

        [Fact]
        public void CapacityShouldReturnCorrectValue()
        {
            var hall1 = new Hall(Guid.NewGuid(), 10);
            var hall2 = new Hall(Guid.NewGuid(), 20);
            var hall3 = new Hall(Guid.NewGuid(), 30);

            var place = new Place(Guid.NewGuid(), "Calvados", false, 10, "");
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);

            hall1.AddPossibleJoin(hall3);

            var expected = 40;
            var actual = place.Capacity;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AcceptShouldWork()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            var hall1 = new Hall(Guid.NewGuid(), 30);
            var hall2 = new Hall(Guid.NewGuid(), 30);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new Reservation(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservation(reservation1);
            place.MakeReservation(reservation2);
            place.MakeReservation(reservation3);
            place.MakeReservation(reservation4);

            place.AcceptReservation(reservation1, new List<Hall>() {hall1, hall2});

            Assert.True(reservation2.IsRejected);
            Assert.True(reservation1.IsAccepted);
            Assert.False(reservation3.IsAnswered);
            Assert.False(reservation4.IsAnswered);
        }

        [Fact]
        public void AcceptShouldFail_PlaceDoesNotOwnHall()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            var hall1 = new Hall(Guid.NewGuid(), 30);
            var hall2 = new Hall(Guid.NewGuid(), 30);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new Reservation(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservation(reservation1);
            place.MakeReservation(reservation2);
            place.MakeReservation(reservation3);
            place.MakeReservation(reservation4);

            var hall4 = new Hall(Guid.NewGuid(), 30);
            Action action = () => place.AcceptReservation(reservation1, new List<Hall>() {hall4});

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldFail_EmptyHallList()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            var hall1 = new Hall(Guid.NewGuid(), 30);
            var hall2 = new Hall(Guid.NewGuid(), 30);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new Reservation(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservation(reservation1);
            place.MakeReservation(reservation2);
            place.MakeReservation(reservation3);
            place.MakeReservation(reservation4);

            Action action = () => place.AcceptReservation(reservation1, new List<Hall>() { });

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldFail_ReservationIsForDifferentPlace()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            var hall1 = new Hall(Guid.NewGuid(), 30);
            var hall2 = new Hall(Guid.NewGuid(), 30);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new Reservation(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservation(reservation1);
            place.MakeReservation(reservation2);
            place.MakeReservation(reservation3);

            Action action = () => place.AcceptReservation(reservation4, new List<Hall>() {hall1});

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AcceptShouldFail_HallAlreadyReserved()
        {
            var placeCost = 50;
            var menuCost = 50;
            var place = new Place(Guid.NewGuid(), "Calvados", false, placeCost, "");
            var hall1 = new Hall(Guid.NewGuid(), 30);
            var hall2 = new Hall(Guid.NewGuid(), 30);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] {"Cheese"});
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, menuCost, new List<Meal>() {meal});
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            hall1.AddPossibleJoin(hall2);
            hall2.AddPossibleJoin(hall3);
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);
            var reservation1 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation2 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation3 = new Reservation(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                client,
                50,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            var reservation4 = new Reservation(Guid.NewGuid(),
                DateTime.Today,
                client,
                30,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());
            place.MakeReservation(reservation1);
            place.MakeReservation(reservation2);
            place.MakeReservation(reservation3);
            place.MakeReservation(reservation4);

            place.AcceptReservation(reservation4, new List<Hall>() {hall1});
            Action action = () => place.AcceptReservation(reservation4, new List<Hall>() {hall1});

            Assert.Throws<DomainException>(action);
        }
    }
}