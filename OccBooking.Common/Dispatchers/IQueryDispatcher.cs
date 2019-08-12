using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace OccBooking.Common.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<Result<T>> DispatchAsync<T>(T query);
    }
}
