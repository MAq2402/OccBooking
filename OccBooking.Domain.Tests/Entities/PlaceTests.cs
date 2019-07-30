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

        [Fact]
        public void AssignMenuShouldWork()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, false, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});

            Action action = () => place.AssignMenu(menu);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            
            place.AllowParty(OccasionType.Wedding);

            Assert.Contains(OccasionType.Wedding, place.AvaliableOccasionTypes);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void AddHallShoudlWork()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            place.AddHall(hall);

            Assert.Contains(hall, place.Halls);
        }

        [Fact]
        public void MakeReservationShouldWork()
        {
            var place = new Place(Guid.NewGuid() , "Calvados", false, false, true, 10, 10, "");
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });
            place.AllowParty(OccasionType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 50;

            var reservation = new Reservation(Guid.NewGuid(), 
                DateTime.Today,
                client,
                amountOfPeopleForReservation,
                menu,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>());

            place.MakeReservation(reservation);

            Assert.Contains(reservation, place.Reservations);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotHaveGivenMenu()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });
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
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportSuchParty()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });
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
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportOption()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() { meal });
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
                new List<PlaceAdditionalOption>() { new PlaceAdditionalOption("Photos", 50) });

            Action action = () => place.MakeReservation(reservation);

            Assert.Throws<NotSupportedAdditionalOptionException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecouseOfImpossibleReservation()
        {
            //var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            //var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            //place.AllowParty(OccasionType.Wedding);
            //place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            //place.AssignMenu(menu);
            //var amountOfPeopleForReservation = 50;

            //place.MakeReservation(DateTime.Today, amountOfPeopleForReservation, true, menu, OccasionType.Wedding, new List<PlaceAdditionalOption>());

            //var expected = true;
            //var actual = place.Reservations.Any(p => p.DateTime == DateTime.Now);

            //Assert.Equal(expected, actual);
            Assert.True(false);
        }

        [Fact]
        public void CheckIfReservationPossibleShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            Assert.True(false);
        }

        [Fact]
        public void CountCapacityForDateShouldWork()
        {
            var hall1 = new Hall(Guid.NewGuid(), 10);
            var hall2 = new Hall(Guid.NewGuid(), 20);
            var hall3 = new Hall(Guid.NewGuid(), 30);
            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");

            Assert.True(false);
        }

        [Fact]
        public void CapacityShouldReturnCorrectValue()
        {
            var hall1 = new Hall(Guid.NewGuid(), 10);
            var hall2 = new Hall(Guid.NewGuid(), 20);
            var hall3 = new Hall(Guid.NewGuid(), 30);    

            var place = new Place(Guid.NewGuid(), "Calvados", false, false, true, 10, 10, "");
            place.AddHall(hall1);
            place.AddHall(hall2);
            place.AddHall(hall3);

            hall1.AddPossibleJoin(hall3);

            var expected = 40;
            var actual = place.Capacity;

            Assert.Equal(expected, actual);
        }
    }
}
