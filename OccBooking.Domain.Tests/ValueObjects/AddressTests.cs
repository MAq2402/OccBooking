using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class AddressTests
    {
        [Theory]
        [InlineData("", "Mikołów", "43-180", "śląskie")]
        [InlineData("Miarki", "", "43-180", "śląskie")]
        [InlineData("Miarki", "Mikołów", "", "śląskie")]
        [InlineData("Miarki", "Mikołów", "432", "śląskie")]
        [InlineData("Miarki", "Mikołów", "ASD", "śląskie")]
        [InlineData("Miarki", "Mikołów", "43-21", "śląskie")]
        [InlineData("Miarki", "Mikołów", "1-213", "śląskie")]
        [InlineData("Miarki", "Mikołów", "43-180", "")]
        [InlineData("Miarki", "Mikołów", "43-180", "slaskie")]
        public void AddressCreationShouldFail(string street, string city, string zipCode, string province)
        {
            Assert.Throws<DomainException>(() => new Address(street, city, zipCode, province));
        }

        [Theory]
        [InlineData("Miarki", "Mikołów", "43-180", "śląskie")]
        public void AddressCreationShouldWork(string street, string city, string zipCode, string province)
        {
            var address = new Address(street, city, zipCode, province);

            Assert.Equal(street, address.Street);
            Assert.Equal(city, address.City);
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(province, address.Province);
        }
    }
}