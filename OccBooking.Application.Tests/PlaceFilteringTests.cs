using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OccBooking.Application.Extensions;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Application.Tests
{
    public class PlaceFilteringTests
    {
        [Fact]
        //[ClassData(typeof(FilteringByDatesShouldWorkData))]
        public void FilteringByDatesShouldWork()
        {
            var address = new Address("Some", "Some", "43-186", "śląskie");
            var place1 = new Place(Guid.NewGuid(), "Some", true, "", address);
            var place2 = new Place(Guid.NewGuid(), "Some", true, "", address);

            var hall = new Hall(Guid.NewGuid(), "Hall", 100);
            hall.MakeEmptyReservation(DateTime.Today.Date);
            place1.AddHall(hall);
            // OGARNIJ zeby zrobic COMMITA
            var places = new List<Place>() {place1,place2};
            place2.MakeEmptyReservation(DateTime.Today.Date);

            var expected = new List<Place>();
            var actual = places.FilterByDate(null, DateTime.Today.Date);

            Assert.Equal(expected, actual);
        }
        //public class FilteringByDatesShouldWorkData : IEnumerable<object[]>
        //{
        //    public IEnumerator<object[]> GetEnumerator()
        //    {
        //        yield return new object[] new List<Places>() { 1, 2, 3 };
        //        yield return new object[] { -4, -6, -10 };
        //        yield return new object[] { -2, 2, 0 };
        //        yield return new object[] { int.MinValue, -1, int.MaxValue };
        //    }

        //    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        //}
    }
}
