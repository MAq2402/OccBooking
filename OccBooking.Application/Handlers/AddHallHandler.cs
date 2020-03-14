using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AddHallHandler : ICommandHandler<AddHallCommand>
    {
        private IHallRepository _hallRepository;

        public AddHallHandler(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result> HandleAsync(AddHallCommand command)
        {
            await _hallRepository.AddAsync(new Hall(Guid.NewGuid(), command.Name, command.Capacity, command.PlaceId));

            await _hallRepository.SaveAsync();

            return Result.Ok();
        }
    }
}