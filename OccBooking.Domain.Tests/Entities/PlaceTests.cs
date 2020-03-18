using Moq;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class PlaceTests
    {
        [Theory]
        [InlineData("")]
        public void CreationShouldFail(string name)
        {
            Assert.Throws<DomainException>(() =>
                new Place(Guid.NewGuid(), name, false, "", CorrectAddress, Guid.NewGuid()));
        }

        [Fact]
        public void CreationShouldWork()
        {
            var place = new Place(Guid.NewGuid(), "Calvados", false, "Nice place", CorrectAddress, Guid.NewGuid());

            Assert.Equal("Calvados", place.Name);
            Assert.False(place.HasRooms);
            Assert.Equal("Nice place", place.Description);
        }

        [Fact]
        public void AllowPartyShouldWork()
        {
            var place = CorrectPlace;

            place.AllowParty(OccasionType.Wedding);

            Assert.Contains(OccasionType.Wedding, place.AvailableOccasionTypes);
        }

        [Fact]
        public void DisallowPartyShouldWork()
        {
            var place = CorrectPlace;

            place.AllowParty(OccasionType.Wedding);
            place.AllowParty(OccasionType.FuneralMeal);
            place.DisallowParty(OccasionType.Wedding);

            Assert.DoesNotContain(OccasionType.Wedding, place.AvailableOccasionTypes);
            Assert.Contains(OccasionType.FuneralMeal, place.AvailableOccasionTypes);
        }

        [Fact]
        public void SupportAdditionalOptionShouldWork()
        {
            var place = CorrectPlace;
            var option = new PlaceAdditionalOption("Flowers", 200);

            place.SupportAdditionalOption(option);

            Assert.Contains(option, place.AdditionalOptions);
        }

        [Fact]
        public void MakeEmptyReservationShouldWork()
        {
            var place = CorrectPlace;
            place.MakeEmptyReservation(DateTime.Today);

            var expected = 1;
            var actual = place.EmptyReservations.Count();

            Assert.Equal(expected, actual);
        }
    }
}