using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Repositories
{
    public class ReservationRequestRepository : Repository<ReservationRequest>, IReservationRequestRepository
    {
        private IHallRepository _hallRepository;

        public ReservationRequestRepository(OccBookingDbContext dbContext, IHallRepository hallRepository,
            IEventDispatcher eventDispatcher) : base(
            dbContext, eventDispatcher)
        {
            _hallRepository = hallRepository;
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
            return amountOfPeople <= CalculateCapacity(halls.Where(h => h.IsFreeOnDate(dateTime)), dateTime);
        }

        private int CalculateCapacity(IEnumerable<Hall> halls, DateTime dateTime)
        {
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h && j.SecondHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h && j.FirstHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }
    }
}