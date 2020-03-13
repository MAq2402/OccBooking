using System;
using System.Collections.Generic;
using System.Linq;
using OccBooking.Application.Extensions;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Application.Tests
{
    public class PlaceFilteringTests
    {
        [Fact]
        public void FilteringByDatesShouldWork()
        {
            var address = new Address("Some", "Some", "43-186", "śląskie");
            var place1 = new Place(Guid.NewGuid(), "Some1", true, "", address, Guid.NewGuid());
            var place2 = new Place(Guid.NewGuid(), "Some2", true, "", address, Guid.NewGuid());
            var place3 = new Place(Guid.NewGuid(), "Some3", true, "", address, Guid.NewGuid());
            var place4 = new Place(Guid.NewGuid(), "Some4", true, "", address, Guid.NewGuid());
            var place5 = new Place(Guid.NewGuid(), "Some5", true, "", address, Guid.NewGuid());

            var hall1 = new Hall(Guid.NewGuid(), "Hall", 100, place1.Id);
            hall1.MakeEmptyReservation(DateTime.Today.Date);

            var hall2 = new Hall(Guid.NewGuid(), "Hall", 100, place2.Id);
            place2.MakeEmptyReservation(DateTime.Today.Date);

            var hall3 = new Hall(Guid.NewGuid(), "Hall", 100, place3.Id);
            place3.MakeEmptyReservation(DateTime.Today.AddDays(-1).Date);

            var hall4 = new Hall(Guid.NewGuid(), "Hall", 100, place4.Id);
            hall4.MakeEmptyReservation(DateTime.Today.AddDays(1).Date);

            var places = new List<Place>() {place1, place2, place3, place4, place5}.AsQueryable();

            var expected = new List<Place>() {place3, place4, place5}.AsQueryable();
            var actual = places.FilterByDate(new List<Hall>() {hall1, hall2, hall3, hall4}.AsQueryable(), null,
                DateTime.Today.Date);

            Assert.Equal(expected, actual);
        }
    }
}