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

namespace OccBooking.Application.Handlers
{
    public class GetHallsHandler : QueryHandler<GetHallsQuery, IEnumerable<HallDto>>
    {
        public GetHallsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<HallDto>>> HandleAsync(GetHallsQuery query)
        {
            var halls = await _dbContext.Halls.Include(h => h.Place).Where(h => h.Place.Id == query.PlaceId)
                .ToListAsync();

            return Result.Ok(_mapper.Map<IEnumerable<HallDto>>(halls));
        }
    }
}