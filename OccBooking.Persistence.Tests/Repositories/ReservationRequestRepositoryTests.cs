using System;
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
using Xunit;

namespace OccBooking.Persistence.Tests.Repositories
{
    public class ReservationRequestRepositoryTests
    {
        public static Client CorrectClient => new Client("Michal", "Kowalski", "michal@michal.com", "505111111");
        public static Address CorrectAddress => new Address("Akacjowa", "Orzesze", "43-100", "śląskie");

        public static Place CorrectPlace => new Place(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Calvados",
            false, "", CorrectAddress, new Guid("4ea10f9e-ae5f-43a1-acfa-c82b678e6ee1"));

        [Fact]
        public async Task GetImpossibleReservationRequestsAsync_ShouldWork()
        {
            var dbContext = GetDatabaseContext();
            var hallRepositoryMock = new Mock<IHallRepository>();
            var hallServiceMock = new Mock<IHallService>();
            var place = CorrectPlace;
            var hall1 = new Hall(Guid.NewGuid(), "Big", 30);
            var menu = new Menu(Guid.NewGuid(), "Vegetarian", MenuType.Vegetarian, 50);
            place.AllowParty(OccasionType.Wedding);
            place.AssignMenu(menu);
            place.AddHall(hall1);
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
            var eventDispatcherMock = new Mock<EventDispatcher>(null);
            var sut = new ReservationRequestRepository(dbContext, hallRepositoryMock.Object, hallServiceMock.Object,
                eventDispatcherMock.Object);

            var result = await sut.GetImpossibleReservationRequestsAsync(place.Id);

            Assert.Contains(reservation2, result);
            Assert.DoesNotContain(reservation1, result);
            Assert.DoesNotContain(reservation3, result);
            Assert.DoesNotContain(reservation4, result);
        }

        private OccBookingDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<OccBookingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var databaseContext = new OccBookingDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}