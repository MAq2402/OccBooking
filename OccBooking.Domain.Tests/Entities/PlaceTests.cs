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
    public class PlaceTests
    {

        [Fact]
        public void AssignMenuShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());

            place.AssignMenu(menu);

            Assert.Contains(menu, place.Menus);
        }

        [Fact]
        public void AssignMenuShouldFail()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, false, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());

            Action action = () => place.AssignMenu(menu);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            
            place.AllowParty(PartyType.Wedding);

            Assert.Contains(PartyType.Wedding, place.AvaliableParties);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void MakeReservationShouldWork()
        {
            var place = new Place(Guid.NewGuid() ,"", false, false, true, 10, 10, 100, "");
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
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            var menuForReservation = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now, 
                amountOfPeopleForReservation, 
                true, menuForReservation, 
                PartyType.Wedding, 
                new List<PlaceAdditionalOption>());

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportSuchParty()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.FuneralMeal,
                new List<PlaceAdditionalOption>());

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecouseOfAmountOfPeople()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 150;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.Wedding,
                new List<PlaceAdditionalOption>());

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void MakeReservationShouldFailBecousePlaceDoesNotSupportOption()
        {
            var place = new Place(Guid.NewGuid(), "", false, false, true, 10, 10, 100, "");
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 100, new List<Meal>());
            place.AllowParty(PartyType.Wedding);
            place.SupportAdditionalOption(new PlaceAdditionalOption("Photos", 100));
            place.AssignMenu(menu);
            var amountOfPeopleForReservation = 50;

            Action action = () => place.MakeReservation(DateTime.Now,
                amountOfPeopleForReservation,
                true, menu,
                PartyType.Wedding,
                new List<PlaceAdditionalOption>() { new PlaceAdditionalOption("Photos", 50) });

            Assert.Throws<DomainException>(action);
        }
    }
}
