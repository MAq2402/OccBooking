using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("595817439a")]
        [InlineData("abcdefghi")]
        [InlineData("50522234$")]
        public void CreationShouldThrowException(string phoneNumber)
        {
            Action action = () => new PhoneNumber(phoneNumber);

            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData("111111111")]
        [InlineData("123456789")]
        public void CreationShouldWork(string phoneNumber)
        {
            var actual = new PhoneNumber(phoneNumber).Value;
            var expected = phoneNumber;

            Assert.Equal(expected, actual);
        }
    }
}
