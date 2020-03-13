using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Moq;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistence.Repositories;
using OccBooking.Persistence.Tests.Utility;
using Xunit;
using static OccBooking.Persistence.Tests.TestData;

namespace OccBooking.Persistence.Tests.Repositories
{
    public class PlaceRepositoryTests
    {
        [Theory]
        [ClassData(typeof(IsPlaceConfigured_TestData))]
        public void IsPlaceConfigured_ShouldWork(Place place, IEnumerable<Hall> halls, IEnumerable<Menu> menus,
            OccasionTypes occasionTypes, bool expected)
        {
            var dbContext = InMemoryDbContextBuilder.CreateDbContext();
            var eventDispatcherMock = new Mock<EventDispatcher>(null);
            var sut = new PlaceRepository(dbContext, eventDispatcherMock.Object);
            dbContext.Add(place);
            foreach (var hall in halls)
            {
                dbContext.Add(hall);
            }
            foreach (var menu in menus)
            {
                place.AssignMenu(menu);
            }
            foreach (var occasionType in occasionTypes)
            {
                place.AllowParty(occasionType);
            }
            dbContext.SaveChanges();

            var actual = sut.IsPlaceConfigured(place.Id);

            Assert.Equal(expected, actual);
        }

        private class IsPlaceConfigured_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new Place(new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"), "Calvados", false, "", CorrectAddress,
                        new Guid("9d94fca4-71a3-49c2-bacb-6b57be878123")),
                    new List<Hall>()
                    {
                        new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1,
                            new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"))
                    },
                    new List<Menu>()
                    {
                        new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
                            MenuType.Vegetarian, 10)
                    },
                    new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
                    true
                };
                yield return new object[]
                {
                    new Place(new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"), "Calvados", false, "", CorrectAddress,
                        new Guid("9d94fca4-71a3-49c2-bacb-6b57be878123")),
                    new List<Hall>()
                    {
                    },
                    new List<Menu>()
                    {
                        new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
                            MenuType.Vegetarian, 10)
                    },
                    new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
                    false
                };
                yield return new object[]
                {
                    new Place(new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"), "Calvados", false, "", CorrectAddress,
                        new Guid("9d94fca4-71a3-49c2-bacb-6b57be878123")),
                    new List<Hall>()
                    {
                        new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1,
                            new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"))
                    },
                    new List<Menu>()
                    {
                    },
                    new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
                    false
                };
                yield return new object[]
                {
                    new Place(new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"), "Calvados", false, "", CorrectAddress,
                        new Guid("9d94fca4-71a3-49c2-bacb-6b57be878123")),
                    new List<Hall>()
                    {
                        new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1,
                            new Guid("237fcb93-e683-4cda-a3de-a2644ae75f8b"))
                    },
                    new List<Menu>()
                    {
                        new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
                            MenuType.Vegetarian, 10)
                    },
                    new OccasionTypes(new List<OccasionType>() {}),
                    false
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}