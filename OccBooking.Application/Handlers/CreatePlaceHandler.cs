using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.Contracts;
using OccBooking.Common.Hanlders;

namespace OccBooking.Application.Handlers
{
    public class CreatePlaceHandler : ICommandHandler<CreatePlaceCommand>
    {
        public async Task<Result> HandleAsync(CreatePlaceCommand command)
        {
            throw new NotImplementedException();
        }
    }
}