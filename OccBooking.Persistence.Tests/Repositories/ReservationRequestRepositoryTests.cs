using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Entities;
using OccBooking.Persistence.Repositories;
using Moq;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Events;
using OccBooking.Domain.Services;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistence.Tests.Utility;
using Xunit;
using static OccBooking.Persistence.Tests.TestData;

namespace OccBooking.Persistence.Tests.Repositories
{
    public class ReservationRequestRepositoryTests
    {
        [Fact]
        public async Task GetImpossibleReservationRequestsAsync_ShouldWork()
        {
            var dbContext = InMemoryDbContextBuilder.CreateDbContext();
            var hallRepositoryMock = new Mock<IHallRepository>();
            var hallServiceMock = new Mock<IHallService>();
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30, place.Id);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 50, place.Id);
            place.AllowParty(OccasionType.Wedding);
            var reservation1 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)},
                place.Id);
            var reservation2 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)},
                place.Id);
            var reservation3 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today.AddDays(1),
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 50)},
                place.Id);
            var reservation4 = ReservationRequest.MakeReservationRequest(Guid.NewGuid(),
                DateTime.Today,
                CorrectClient,
                OccasionType.Wedding,
                new List<PlaceAdditionalOption>(),
                new List<MenuOrder>() {new MenuOrder(menu, 30)},
                place.Id);
            dbContext.Add(hall1);
            dbContext.Add(menu);
            dbContext.Add(reservation1);
            dbContext.Add(reservation2);
            dbContext.Add(reservation3);
            dbContext.Add(reservation4);
            dbContext.Add(place);
            reservation1.Accept(place.Id, new List<Guid>() {hall1.Id}.AsEnumerable());
            dbContext.SaveChanges();
            hallRepositoryMock.Setup(m => m.GetHallsAsync(place.Id))
                .ReturnsAsync(new List<Hall>() {hall1});
            hallServiceMock.SetupSequence(m => m.CalculateCapacity(It.IsAny<List<Hall>>(), It.IsAny<DateTime>()))
                .Returns(30)
                .Returns(90)
                .Returns(30);
            var eventDispatcherMock = new Mock<EventPublisher>(null);
            var sut = new ReservationRequestRepository(dbContext, hallRepositoryMock.Object, hallServiceMock.Object,
                eventDispatcherMock.Object);

            var result = await sut.GetImpossibleReservationRequestsAsync(place.Id, DateTime.Today);

            Assert.Contains(reservation2, result);
            Assert.DoesNotContain(reservation1, result);
            Assert.DoesNotContain(reservation3, result);
            Assert.DoesNotContain(reservation4, result);
        }
    }
}