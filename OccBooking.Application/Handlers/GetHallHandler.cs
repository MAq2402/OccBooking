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
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class GetHallHandler : QueryHandler<GetHallQuery, ExtendedHallDto>
    {
        private IHallRepository _repository;

        public GetHallHandler(IHallRepository repository,OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _repository = repository;
        }

        public override async Task<Result<ExtendedHallDto>> HandleAsync(GetHallQuery query)
        {
            var hall =  await _dbContext.Halls.Include(h => h.Place).Include(h => h.PossibleJoinsWhereIsFirst).ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst).ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond).ThenInclude(j => j.SecondHall).Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall).FirstOrDefaultAsync(h => h.Id == query.Id);

            if (hall == null)
            {
                return Result.Fail<ExtendedHallDto>("Hall with given Id does not exist");
            }

            var joins = _dbContext.HallJoins.Include(j => j.FirstHall).Include(j => j.SecondHall)
                .Where(j => j.FirstHall == hall || j.SecondHall == hall);
            var result = _mapper.Map<ExtendedHallDto>(hall);

            foreach (var join in hall.PossibleJoins)
            {
                result.Joins.Add(join.FirstHall == hall
                    ? new PossibleJoinDto() {HallId = join.SecondHall.Id }
                    : new PossibleJoinDto() {HallId = join.FirstHall.Id });
            }

            return Result.Ok(result);
        }
    }
}