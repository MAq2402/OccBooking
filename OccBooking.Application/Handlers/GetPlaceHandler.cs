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
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class GetPlaceHandler : QueryHandler<GetPlaceQuery, PlaceDto>
    {
        private readonly IPlaceRepository _placeRepository;

        public GetPlaceHandler(OccBookingDbContext dbContext, IMapper mapper, IPlaceRepository placeRepository) : base(dbContext, mapper)
        {
            _placeRepository = placeRepository;
        }

        public override async Task<Result<PlaceDto>> HandleAsync(GetPlaceQuery query)
        {
            var place = await _dbContext.Places
                .Include(p => p.Menus)
                .FirstOrDefaultAsync(p => p.Id == query.PlaceId);

            var owner = await _dbContext.Owners.FirstOrDefaultAsync(o => o.Id == place.OwnerId);

            var image = await _dbContext.PlaceImages.FirstOrDefaultAsync(i => i.PlaceId == query.PlaceId);

            if (place == null)
            {
                return Result.Fail<PlaceDto>("Place with this id does not exist");
            }

            var result = _mapper.Map<PlaceDto>(place);
            result = _mapper.Map(owner, result);
            result.IsConfigured = _placeRepository.IsPlaceConfigured(place.Id);

            if (image != null)
            {
                result.Image = Convert.ToBase64String(image.Content);
            }

            return Result.Ok(result);
        }
    }
}