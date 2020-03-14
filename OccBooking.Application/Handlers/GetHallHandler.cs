using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class GetHallHandler : QueryHandler<GetHallQuery, ExtendedHallDto>
    {
        public GetHallHandler(OccBookingDbContext dbContext, IMapper mapper) : base(
            dbContext, mapper)
        {
        }

        public override async Task<Result<ExtendedHallDto>> HandleAsync(GetHallQuery query)
        {
            var hall = await _dbContext.Halls
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall)
                .FirstOrDefaultAsync(h => h.Id == query.Id);

            if (hall == null)
            {
                return Result.Fail<ExtendedHallDto>("Hall with given Id does not exist");
            }

            var result = MapToResult(hall);

            return Result.Ok(result);
        }

        private ExtendedHallDto MapToResult(Hall hall)
        {
            var result = _mapper.Map<ExtendedHallDto>(hall);
            foreach (var join in hall.PossibleJoins)
            {
                result.Joins.Add(join.FirstHall == hall
                    ? new PossibleJoinDto() {HallId = join.SecondHall.Id, IsPossible = true}
                    : new PossibleJoinDto() {HallId = join.FirstHall.Id, IsPossible = true});
            }

            return result;
        }
    }
}