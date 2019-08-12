using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Common.Types;

namespace OccBooking.Common.Hanlders
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<Result> HandleAsync(T command);
    }
}
