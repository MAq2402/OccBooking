using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class HallJoinTests
    {
        [Fact]
        public void ParticipateInShouldWork()
        {
            var hall1 = new Hall(Guid.NewGuid(), "Big", 100);
            var hall2 = new Hall(Guid.NewGuid(), "Big", 100);
            var hall3 = new Hall(Guid.NewGuid(), "Big", 100);

            var hallJoin = new HallJoin(new Guid(),hall1, hall2 );

            Assert.True(hallJoin.ParticipatesIn(hall1));
            Assert.True(hallJoin.ParticipatesIn(hall2));
            Assert.False(hallJoin.ParticipatesIn(hall3));
        }
    }
}
