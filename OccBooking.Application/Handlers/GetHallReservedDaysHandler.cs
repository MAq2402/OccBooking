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
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetHallReservedDaysHandler : QueryHandler<GetHallReservedDaysQuery, IEnumerable<DateTime>>
    {
        public GetHallReservedDaysHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<DateTime>>> HandleAsync(GetHallReservedDaysQuery query)
        {
            var hall = await _dbContext.Halls.Include(h => h.HallReservations)
                .FirstOrDefaultAsync(h => h.Id == query.Id);

            return hall == null
                ? Result.Fail<IEnumerable<DateTime>>("Could not find hall with given id")
                : Result.Ok(hall.HallReservations.Select(x => x.Date));
        }
    }
}