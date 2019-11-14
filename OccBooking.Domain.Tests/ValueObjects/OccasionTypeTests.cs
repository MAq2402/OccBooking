using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class OccasionTypeTests
    {
        [Theory]
        [InlineData("Wedding")]
        [InlineData("Funereal Meal")]
        public void CreateShouldWork(string name)
        {
            var occasionType = OccasionType.Create(name);

            Assert.Equal(name, occasionType.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Some string")]
        [InlineData("123")]
        public void CreateShouldFail(string name)
        {
            Assert.Throws<DomainException>(() => OccasionType.Create(name));
        }
    }
}
