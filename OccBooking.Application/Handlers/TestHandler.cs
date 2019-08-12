using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;

namespace OccBooking.Application.Handlers
{
    public class TestHandler : ICommandHandler<TestCommand>
    {
        public Task<Result> HandleAsync(TestCommand command)
        {
            Debug.WriteLine("HURA");
            return Task.FromResult(Result.Ok());
        }
    }
}
