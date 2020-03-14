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

namespace OccBooking.Application.Handlers
{
    public class GetHallsHandler : QueryHandler<GetHallsQuery, IEnumerable<HallDto>>
    {
        public GetHallsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<HallDto>>> HandleAsync(GetHallsQuery query)
        {
            if (!query.Date.HasValue)
            {
                var halls = await _dbContext.Halls
                    .Include(h => h.PossibleJoinsWhereIsFirst)
                    .ThenInclude(j => j.FirstHall)
                    .Include(h => h.PossibleJoinsWhereIsFirst)
                    .ThenInclude(j => j.SecondHall)
                    .Include(h => h.PossibleJoinsWhereIsSecond)
                    .ThenInclude(j => j.SecondHall)
                    .Include(h => h.PossibleJoinsWhereIsSecond)
                    .ThenInclude(j => j.FirstHall).Where(h => h.PlaceId == query.PlaceId).ToListAsync();

                return Result.Ok(halls.Select(MapToResult));
            }
            else
            {
                var halls = await _dbContext.Halls
                    .Include(h => h.HallReservations)
                    .ThenInclude(hr => hr.ReservationRequest)
                    .Include(h => h.PossibleJoinsWhereIsFirst)
                    .ThenInclude(j => j.FirstHall)
                    .Include(h => h.PossibleJoinsWhereIsFirst)
                    .ThenInclude(j => j.SecondHall)
                    .Include(h => h.PossibleJoinsWhereIsSecond)
                    .ThenInclude(j => j.SecondHall)
                    .Include(h => h.PossibleJoinsWhereIsSecond)
                    .ThenInclude(j => j.FirstHall).Where(h => h.PlaceId == query.PlaceId)
                    .ToListAsync();

                halls = halls.Where(h => h.IsFreeOnDate(query.Date.Value)).ToList();

                return Result.Ok(halls.Select(MapToResult));
            }
        }

        private HallDto MapToResult(Hall hall)
        {
            var result = _mapper.Map<HallDto>(hall);
            foreach (var join in hall.PossibleJoins)
            {
                result.Joins.Add(join.FirstHall == hall
                    ? new HallJoinDto() {HallId = join.SecondHall.Id}
                    : new HallJoinDto() {HallId = join.FirstHall.Id});
            }

            return result;
        }
    }
}