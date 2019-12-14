using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AddOptionHandler : ICommandHandler<AddOptionCommand>
    {
        private IPlaceRepository _placeRepository;

        public AddOptionHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(AddOptionCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            place.SupportAdditionalOption(new PlaceAdditionalOption(command.Name, command.Cost));

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}