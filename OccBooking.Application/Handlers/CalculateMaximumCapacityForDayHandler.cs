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
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class CalculateMaximumCapacityForDayHandler : QueryHandler<CalculateMaximumCapacityForDayQuery, int>
    {
        public CalculateMaximumCapacityForDayHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext,
            mapper)
        {
        }

        public override async Task<Result<int>> HandleAsync(CalculateMaximumCapacityForDayQuery query)
        {
            var place = await _dbContext.Places
                .FirstOrDefaultAsync(p => p.Id == query.PlaceId);

            var halls = await _dbContext.Halls.Include(h => h.Place)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.HallReservations)
                .Where(h => h.Place.Id == query.PlaceId).ToListAsync();

            if (place == null)
            {
                return Result.Fail<int>("Place with given id does not exist");
            }

            return Result.Ok(place.CalculateCapacity(halls, query.Date));
        }
    }
}