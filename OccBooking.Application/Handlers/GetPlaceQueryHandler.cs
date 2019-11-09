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
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetPlaceQueryHandler : QueryHandler<GetPlaceQuery, PlaceDto>
    {
        public GetPlaceQueryHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<PlaceDto>> HandleAsync(GetPlaceQuery query)
        {
            var place = await _dbContext.Places.FirstOrDefaultAsync(p => p.Id == query.PlaceId);

            return place == null
                ? Result.Fail<PlaceDto>("Place with this id does not exist")
                : Result.Ok(_mapper.Map<PlaceDto>(place));
        }
    }
}