using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Entities;

namespace OccBooking.Application.Handlers
{
    public class IngredientsHandler : IQueryHandler<IngredientsQuery, IEnumerable<Ingredient>>
    {
        private OccBookingDbContext _dbContext;

        public IngredientsHandler(OccBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<IEnumerable<Ingredient>>> HandleAsync(IngredientsQuery query)
        {
            return Result.Ok<IEnumerable<Ingredient>>(await _dbContext.Ingredients.ToListAsync());
        }
    }
}
