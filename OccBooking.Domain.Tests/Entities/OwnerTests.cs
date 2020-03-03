using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.Entities
{
    public class OwnerTests
    {
        [Theory]
        [InlineData("", "Miciak", "michal@miciak.com", "111111111")]
        [InlineData("Michał", "", "michal@miciak.com", "111111111")]
        [InlineData("Michał", "Miciak", "michal", "111111111")]
        [InlineData("Michał", "Miciak", "michal", "11111111a")]
        public void CreationShouldFail(string firstName, string lastName, string email, string phoneNumber)
        {
            Action action = () => new Owner(Guid.NewGuid(), firstName, lastName, email, phoneNumber);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void CreationShouldWork()
        {
            var owner = new Owner(Guid.NewGuid(), "Michał", "Miciak", "michal@miciak.com", "111111111");

            Assert.Equal("Michał Miciak", owner.Name.FullName);
            Assert.Equal("michal@miciak.com", owner.Email.Value);
            Assert.Equal("111111111", owner.PhoneNumber.Value);
        }
    }
}