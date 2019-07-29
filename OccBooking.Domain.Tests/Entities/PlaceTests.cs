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

        [Fact]
        public void AssignMenuShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var meal = new Meal(Guid.NewGuid(), "Dumplings", "", MealType.Main, new[] { "Cheese" });
            var place = new Place(Guid.NewGuid(), "", false, false, false, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>() {meal});

            Action action = () => place.AssignMenu(menu);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            
            place.AllowParty(PartyType.Wedding);

            Assert.Contains(PartyType.Wedding, place.AvaliableParties);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void AddHallShoudlWork()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            place.AddHall(hall);

            Assert.Contains(hall, place.Halls);
        }

        [Fact]
        public void MakeReservationShouldWork()
        {
            var place = new Place(Guid.NewGuid() ,"", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 50;

            place.MakeReservation(DateTime.Today, amountOfPeopleForReservation, true, menu, PartyType.Wedding, new List<PlaceAdditionalOption>());

            var expected = true;
            var actual = place.Reservations.Any(p => p.DateTime == DateTime.Now);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotHaveGivenMenu()
        {
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now, 
                amountOfPeopleForReservation, 
                true, menuForReservation, 
                PartyType.Wedding, 
                new List<PlaceAdditionalOption>());

            Assert.Throws<LackOfMenuException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportSuchParty()
        {
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.FuneralMeal,
                new List<PlaceAdditionalOption>());

            Assert.Throws<PartyIsNotAvaliableException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecouseOfAmountOfPeople()
        {
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);
            var amountOfPeopleForReservation = 150;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.Wedding,
                new List<PlaceAdditionalOption>());

            Assert.Throws<ToSmallCapacityException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportOption()
        {
            var hall = new Hall(Guid.NewGuid(), 100);
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            place.AddHall(hall);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.Wedding,
                new List<PlaceAdditionalOption>() { new PlaceAdditionalOption("Photos", 50) });

            Assert.Throws<NotSupportedAdditionalOptionException>(action);
        }
    }
}
