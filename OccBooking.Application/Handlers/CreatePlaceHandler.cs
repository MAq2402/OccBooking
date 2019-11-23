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
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class CreatePlaceHandler : ICommandHandler<CreatePlaceCommand>
    {
        private IOwnerRepository _ownerRepository;

        public CreatePlaceHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Result> HandleAsync(CreatePlaceCommand command)
        {
            var place = new Place(command.Id, command.Name, command.HasRooms, command.CostPerPerson,
                command.Description, new Address(command.Street, command.City, command.ZipCode, command.Province));

            var owner = await _ownerRepository.GetAsync(command.OwnerId);

            if (owner == null)
            {
                return Result.Fail("Owner with this id does not exists");
            }

            owner.AddPlace(place);

            await _ownerRepository.SaveAsync();

            return Result.Ok();
        }
    }
}