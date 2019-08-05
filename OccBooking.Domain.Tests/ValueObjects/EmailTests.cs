using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("michal")]
        [InlineData("michal@")]
        [InlineData("")]
        public void CreationShouldFail(string value)
        {
            Assert.Throws<DomainException>(() => new Email(value));
        }

        [Theory]
        [InlineData("michal@gmail.com")]
        [InlineData("michal@gmail.asd.com")]
        public void CreationShouldWork(string value)
        {
            var email = new Email(value);
            Assert.Equal(email.Value, value);
        }
    }
}
