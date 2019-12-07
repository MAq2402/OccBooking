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
    public class GetMenusHandler : QueryHandler<GetMenusQuery, IEnumerable<MenuDto>>
    {
        public GetMenusHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<MenuDto>>> HandleAsync(GetMenusQuery query)
        {
            var menus = await _dbContext.Menus.Include(m => m.Meals).Include(m => m.Place)
                .Where(m => m.Place.Id == query.PlaceId)
                .ToListAsync();

            return Result.Ok(_mapper.Map<IEnumerable<MenuDto>>(menus));
        }
    }
}