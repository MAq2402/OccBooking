using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Entities;

namespace OccBooking.Application.Handlers
{
    public class IngredientsHandler : QueryHandler<IngredientsQuery, IEnumerable<Ingredient>>
    {
        public IngredientsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<Ingredient>>> HandleAsync(IngredientsQuery query)
        {
            return Result.Ok<IEnumerable<Ingredient>>(await _dbContext.Ingredients.ToListAsync());
        }
    }
}