using System;
using System.Collections.Generic;
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
    public class GetPlacesHandler : IQueryHandler<GetPlacesQuery, IEnumerable<PlaceDto>>
    {
        private OccBookingDbContext _dbContext;
        private IMapper _mapper;

        public GetPlacesHandler(OccBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PlaceDto>>> HandleAsync(GetPlacesQuery query)
        {
            var places = await _dbContext.Places.ToListAsync();

            return Result.Ok(_mapper.Map<IEnumerable<PlaceDto>>(places));
        }
    }
}