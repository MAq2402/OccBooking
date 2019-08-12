using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace OccBooking.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<Result> DispatchAsync<T>(T command);
    }
}
