using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Extensions;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
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
            var places = _dbContext.Places.AsQueryable();
            if (query.PlaceFilter != null)
            {
                places = places.FilterByName(query.PlaceFilter.Name).FilterByProvince(query.PlaceFilter.Province)
                    .FilterByCity(query.PlaceFilter.City)
                    .FilterByCostPerPerson(query.PlaceFilter.MinCostPerPerson, query.PlaceFilter.MaxCostPerPerson)
                    .FilterByMinCapacity(query.PlaceFilter.MinCapacity)
                    .FilterByOccassionTypes(query.PlaceFilter.OccassionType);
            }

            return Result.Ok(_mapper.Map<IEnumerable<PlaceDto>>(await places.ToListAsync()));
        }
    }
}