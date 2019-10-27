using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetOwnerPlacesHandler : IQueryHandler<GetOwnerPlacesQuery, IEnumerable<PlaceDto>>
    {
        private OccBookingDbContext _dbContext;
        private IMapper _mapper;

        public GetOwnerPlacesHandler(OccBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PlaceDto>>> HandleAsync(GetOwnerPlacesQuery query)
        {
            var places = _dbContext.Places.Include(p => p.Owner).Where(p => p.Owner.Id == query.OwnerId);

            return Result.Ok(_mapper.Map<IEnumerable<PlaceDto>>(await places.ToListAsync()));
        }
    }
}