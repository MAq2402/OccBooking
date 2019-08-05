using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class PersonNameTests
    {
        [Theory]
        [InlineData("", "Nowak")]
        [InlineData("Alex", "")]
        public void CreationShouldFail(string firstName, string lastName)
        {
            Assert.Throws<DomainException>(() => new PersonName(firstName, lastName));
        }
        [Fact]
        public void CreationShouldWork()
        {
            var firstName = "Alex";
            var lastName = "Nowak";

            var name = new PersonName(firstName, lastName);

            Assert.Equal(firstName, name.FirstName);
            Assert.Equal(lastName, name.LastName);
            Assert.Equal("Alex Nowak", name.FullName);
        }
    }
}
