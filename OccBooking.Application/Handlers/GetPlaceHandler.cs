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
    public class GetPlaceHandler : QueryHandler<GetPlaceQuery, PlaceDto>
    {
        public GetPlaceHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<PlaceDto>> HandleAsync(GetPlaceQuery query)
        {
            var place = await _dbContext.Places.FirstOrDefaultAsync(p => p.Id == query.PlaceId);
            var image = await _dbContext.PlaceImages.FirstOrDefaultAsync(i => i.PlaceId == query.PlaceId);

            if (place == null)
            {
                return Result.Fail<PlaceDto>("Place with this id does not exist");
            }

            var result = _mapper.Map<PlaceDto>(place);

            if (image != null)
            {
                result.Image = Convert.ToBase64String(image.Content);
            }

            return Result.Ok(result);
        }
    }
}