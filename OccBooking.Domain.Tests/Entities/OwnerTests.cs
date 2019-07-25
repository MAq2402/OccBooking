using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class OwnerTests
    {
        [Fact]

        public void AddPlaceShouldThrowDomainException()
        {
            var owner = new Owner();

            Action action = () => owner.AddPlace(null);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddPlaceShouldWork()
        {
            var owner = new Owner();
            var place = new Place(Guid.NewGuid() ,"",false,false,false, 0, 0 , 0, "");

            owner.AddPlace(place);
            var expected = true;
            var actual = owner.Places.Contains(place);

            Assert.Equal(expected, actual);
        }
    }
}
