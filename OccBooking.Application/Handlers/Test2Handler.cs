using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;

namespace OccBooking.Application.Handlers
{
    public class Test2Handler : IQueryHandler<TestQuery, string>
    {
        public Task<Result<string>> HandleAsync(TestQuery query)
        {
            return Task.FromResult(Result.Ok("Super"));
        }
    }
}
