using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Entities;

namespace OccBooking.Application.Handlers
{
    public class IngredientsHandler : QueryHandler<IngredientsQuery, IEnumerable<string>>
    {
        public IngredientsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<string>>> HandleAsync(IngredientsQuery query)
        {
            return Result.Ok<IEnumerable<string>>(await _dbContext.Ingredients.Select(i => i.Name).ToListAsync());
        }
    }
}