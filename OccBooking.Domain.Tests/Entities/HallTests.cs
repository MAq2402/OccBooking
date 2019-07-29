using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OccBooking.Domain.Tests.Entities
{
    public class HallTests
    {
        [Fact]
        public void AddPossibleJoinShouldWork()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var hallToJoin = new Hall(Guid.NewGuid(), 20);

            hall.AddPossibleJoin(hallToJoin);

            var expected = 1;

            Assert.Equal(expected, hall.PossibleJoins.Count());
            Assert.Equal(expected, hallToJoin.PossibleJoins.Count());
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecouseOfDuplicationOfPossibleJoins()
        {
            var hall = new Hall(Guid.NewGuid(), 10);
            var hallToJoin = new Hall(Guid.NewGuid(), 20);

            hall.AddPossibleJoin(hallToJoin);
            Action action = () => hall.AddPossibleJoin(hallToJoin);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecouseOfHallsBeingFromTwoDifferentPlaces()
        {
            var firstPlace = new Place(Guid.NewGuid(), "", false, false, false, 10, 10, "");
            var secondPlace = new Place(Guid.NewGuid(), "", false, false, false, 10, 10, "");          
            var hall = new Hall(Guid.NewGuid(), 10);
            var hallToJoin = new Hall(Guid.NewGuid(), 20);
            firstPlace.AddHall(hall);
            secondPlace.AddHall(hallToJoin);

            hall.AddPossibleJoin(hallToJoin);
            Action action = () => hall.AddPossibleJoin(hallToJoin);

            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddPossibleJoinShouldFailBecouseOfNullValue()
        {
            var hall = new Hall(Guid.NewGuid(), 10);

            Action action = () => hall.AddPossibleJoin(null);

            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(int.MinValue)]
        public void CreationOfHallShouldFail(int capacity)
        {
            Assert.Throws<DomainException>(() => new Hall(Guid.NewGuid(), capacity));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public void CreationOfHallShouldWork(int capacity)
        {
            var hall = new Hall(Guid.NewGuid(), capacity);

            Assert.NotNull(hall);
        }
    }
}
