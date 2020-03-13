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
using OccBooking.Domain.Services;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class CalculateMaximumCapacityForDayHandler : QueryHandler<CalculateMaximumCapacityForDayQuery, int>
    {
        private readonly IHallService _hallService;

        public CalculateMaximumCapacityForDayHandler(OccBookingDbContext dbContext, IMapper mapper, IHallService hallService) : base(dbContext,
            mapper)
        {
            _hallService = hallService;
        }

        public override async Task<Result<int>> HandleAsync(CalculateMaximumCapacityForDayQuery query)
        {
            var place = await _dbContext.Places
                .FirstOrDefaultAsync(p => p.Id == query.PlaceId);

            var halls = await _dbContext.Halls
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.HallReservations)
                .Where(h => h.PlaceId == query.PlaceId).ToListAsync();

            if (place == null)
            {
                return Result.Fail<int>("Place with given id does not exist");
            }

            return Result.Ok(_hallService.CalculateCapacity(halls, query.Date));
        }
    }
}