using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class PlaceAdditionalOptionsTests
    {
        [Fact]
        public void EquailtyShouldWork()
        {
            var additionalOption1 = new PlaceAdditionalOption("Photos", 100);
            var additionalOption2 = new PlaceAdditionalOption("Flowers", 100);
            var additionalOptions1 = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() { additionalOption1, additionalOption2 });
            var additionalOptions2 = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() { additionalOption2, additionalOption1 });

            Assert.True(additionalOptions1 == additionalOptions2);
        }

        [Fact]
        public void ExplicitOperatorShouldWork()
        {
            var actual = (PlaceAdditionalOptions)"Flowers,100;Photos,50";

            var expected = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() {
                new PlaceAdditionalOption("Photos",50), new PlaceAdditionalOption("Flowers",100)});

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void ImplicitOperatorShouldWork()
        {
            string actual = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() {
                new PlaceAdditionalOption("Photos",50), new PlaceAdditionalOption("Flowers",100)});
            var expected = "Photos,50;Flowers,100";


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ToStringShouldWork()
        {
            string actual = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() {
                new PlaceAdditionalOption("Photos",50), new PlaceAdditionalOption("Flowers",100)}).ToString();
            var expected = "Photos,50;Flowers,100";


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddOptionShouldWork()
        {
            var newOption = new PlaceAdditionalOption("Flowers", 100);
            var options = new PlaceAdditionalOptions(Enumerable.Empty<PlaceAdditionalOption>());

            var actual = options.AddOption(newOption);
            var expected = (PlaceAdditionalOptions)"Flowers,100";

            Assert.True(actual.Equals(expected));
        }

        [Fact]
        public void AddOptionShoulFail()
        {
            var options = new PlaceAdditionalOptions(Enumerable.Empty<PlaceAdditionalOption>());

            Assert.Throws<DomainException>(() => options.AddOption(null));
        }
    }
}
