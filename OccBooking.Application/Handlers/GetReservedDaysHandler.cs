using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetReservedDaysHandler : QueryHandler<GetReservedDaysQuery, IEnumerable<DateTime>>
    {
        public GetReservedDaysHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<DateTime>>> HandleAsync(GetReservedDaysQuery query)
        {
            var place = await _dbContext.Places.Include(p => p.EmptyReservations)
                .FirstOrDefaultAsync(p => p.Id == query.PlaceId);

            if (place == null)
            {
                return Result.Fail<IEnumerable<DateTime>>("Could not find place with given Id");
            }

            var halls = await _dbContext.Halls.Include(h => h.Place)
                .Include(h => h.HallReservations)
                .ThenInclude(hr => hr.ReservationRequest)
                .Where(h => h.Place.Id == query.PlaceId).ToListAsync();

            var reservedDaysFromHalls = GetReservedDaysFromHallReservations(halls);
            var reservedDaysFromPlace = place.EmptyReservations.Select(r => r.Date);
            var result = reservedDaysFromPlace.Concat(reservedDaysFromHalls).Distinct();


            return Result.Ok(result);
        }

        private IEnumerable<DateTime> GetReservedDaysFromHallReservations(IEnumerable<Hall> halls)
        {
            var allReservationDays = halls.SelectMany(h => h.HallReservations)
                .Select(hr => hr.ReservationRequest.DateTime).Distinct();

            var result = new List<DateTime>();
            foreach (var day in allReservationDays)
            {
                if (!halls.Any(h => h.IsFreeOnDate(day)))
                {
                    result.Add(day);
                }
            }

            return result;
        }
    }
}