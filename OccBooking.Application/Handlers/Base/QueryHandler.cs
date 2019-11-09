using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using OccBooking.Common.Hanlders;
using OccBooking.Common.Types;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers.Base
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        protected OccBookingDbContext _dbContext;
        protected IMapper _mapper;

        protected QueryHandler(OccBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public abstract Task<Result<TResult>> HandleAsync(TQuery query);
    }
}