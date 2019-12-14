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
            var places = _dbContext.Places.Include(p => p.Owner).Where(p => p.Owner.Id == query.OwnerId);

            return Result.Ok(_mapper.Map<IEnumerable<PlaceDto>>(await places.ToListAsync()));
        }
    }
}