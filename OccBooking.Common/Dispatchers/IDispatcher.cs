using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Common.Types;

namespace OccBooking.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task<Result> DispatchAsync(ICommand command);
        Task<Result<T>> DispatchAsync<T>(IQuery<T> query);
    }
}
