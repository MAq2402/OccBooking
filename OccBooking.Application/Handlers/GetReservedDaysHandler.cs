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
            var halls = await _dbContext.Halls.Include(h => h.Place).Include(h => h.HallReservations)
                .Where(h => h.Place.Id == query.PlaceId).ToListAsync();

            var allReservationDays = halls.SelectMany(h => h.HallReservations).Select(hr => hr.Date).Distinct();

            var result = new List<DateTime>();
            foreach (var day in allReservationDays)
            {
                if (!halls.Any(h => h.IsFreeOnDate(day)))
                {
                    result.Add(day);
                }
            }

            return Result.Ok<IEnumerable<DateTime>>(result);
        }
    }
}