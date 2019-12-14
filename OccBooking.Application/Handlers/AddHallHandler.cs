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
        private IPlaceRepository _placeRepository;

        public AddHallHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(AddHallCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with this id does not exist");
            }

            place.AddHall(new Hall(Guid.NewGuid(), command.Name, command.Capacity));

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}