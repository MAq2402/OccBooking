using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class PlaceAdditionalOptionsTests
    {
        [Fact]
        public void PlaceAdditionalOptionsEquailtyShouldWork()
        {
            var additionalOption1 = new PlaceAdditionalOption("Photos", 100);
            var additionalOption2 = new PlaceAdditionalOption("Flowers", 100);
            var additionalOptions1 = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() { additionalOption1, additionalOption2 });
            var additionalOptions2 = new PlaceAdditionalOptions(new List<PlaceAdditionalOption>() { additionalOption2, additionalOption1 });

            Assert.True(additionalOptions1 == additionalOptions2);
        }
    }
}
