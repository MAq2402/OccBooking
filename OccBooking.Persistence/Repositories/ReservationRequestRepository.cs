using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Services;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Repositories
{
    public class ReservationRequestRepository : Repository<ReservationRequest>, IReservationRequestRepository
    {
        private readonly IHallRepository _hallRepository;
        private readonly IHallService _hallService;

        public ReservationRequestRepository(OccBookingDbContext dbContext, IHallRepository hallRepository,
            IHallService hallService,
            IEventDispatcher eventDispatcher) : base(
            dbContext, eventDispatcher)
        {
            _hallRepository = hallRepository;
            _hallService = hallService;
        }

        public async Task<ReservationRequest> GetReservationRequestAsync(Guid id)
        {
            return await _dbContext.ReservationRequests.Include(r => r.Place).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ReservationRequest>> GetImpossibleReservationRequestsAsync(Guid placeId)
        {
            var halls = await _hallRepository.GetHallsAsync(placeId);
            return await _dbContext.ReservationRequests.Include(r => r.Place).Where(r =>
                r.Place.Id == placeId &&
                !r.IsAnswered && !DoHallsHaveEnoughCapacity(halls, r.DateTime, r.AmountOfPeople)).ToListAsync();
        }

        private bool DoHallsHaveEnoughCapacity(IEnumerable<Hall> halls, DateTime dateTime, int amountOfPeople)
        {
            return amountOfPeople <= _hallService.CalculateCapacity(halls, dateTime);
        }
    }
}