using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.Contracts;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class CreatePlaceHandler : ICommandHandler<CreatePlaceCommand>
    {
        private IPlaceRepository _placeRepository;

        public CreatePlaceHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(CreatePlaceCommand command)
        {
            var place = new Place(command.Id, command.Name, command.HasRooms, command.Description,
                new Address(command.Street, command.City, command.ZipCode, command.Province), command.OwnerId);

            await _placeRepository.AddAsync(place);

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}