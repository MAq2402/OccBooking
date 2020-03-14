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

namespace OccBooking.Application.Handlers
{
    public class GetOwnerPlacesHandler : QueryHandler<GetOwnerPlacesQuery, IEnumerable<PlaceDto>>
    {
        public GetOwnerPlacesHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<PlaceDto>>> HandleAsync(GetOwnerPlacesQuery query)
        {
            var owner = await _dbContext.Owners.FirstOrDefaultAsync(o => o.Id == query.OwnerId);
            var places = _dbContext.Places.Where(p => p.OwnerId == query.OwnerId);

            var result = _mapper.Map<IEnumerable<PlaceDto>>(await places.ToListAsync());
            foreach (var item in result)
            {
                item.Owner = _mapper.Map<OwnerDto>(owner);
            }
            return Result.Ok(result);
        }
    }
}