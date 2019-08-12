using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Common.Types;

namespace OccBooking.Common.Hanlders
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query);
    }
}
