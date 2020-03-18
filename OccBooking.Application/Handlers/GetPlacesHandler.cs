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
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetPlacesHandler : QueryHandler<GetPlacesQuery, IEnumerable<PlaceDto>>
    {
        public GetPlacesHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<PlaceDto>>> HandleAsync(GetPlacesQuery query)
        {
            var places = _dbContext.Places
                .Include(p => p.EmptyReservations).AsQueryable();
            var halls = _dbContext.Halls
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.FirstHall)
                .Include(h => h.PossibleJoinsWhereIsFirst)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.SecondHall)
                .Include(h => h.PossibleJoinsWhereIsSecond)
                .ThenInclude(j => j.FirstHall);
            var menus = _dbContext.Menus;

            if (query.PlaceFilter != null)
            {
                places = places.FilterByName(query.PlaceFilter.Name).FilterByProvince(query.PlaceFilter.Province)
                    .FilterByCity(query.PlaceFilter.City)
                    .FilterByCostPerPerson(menus, query.PlaceFilter.MinCostPerPerson, query.PlaceFilter.MaxCostPerPerson)
                    .FilterByMinCapacity(halls, query.PlaceFilter.MinCapacity)
                    .FilterByOccasionTypes(query.PlaceFilter.OccasionType)
                    .FilterByDate(halls, query.PlaceFilter.FreeFrom, query.PlaceFilter.FreeTo);
            }

            var result = new List<PlaceDto>();
            foreach (var place in places)
            {
                var placeForResult = _mapper.Map<PlaceDto>(place);

                var image = await _dbContext.PlaceImages.FirstOrDefaultAsync(i => i.PlaceId == place.Id);
                if (image != null)
                {
                    placeForResult.Image = Convert.ToBase64String(image.Content);
                }

                result.Add(placeForResult);
            }

            return Result.Ok<IEnumerable<PlaceDto>>(result);
        }
    }
}