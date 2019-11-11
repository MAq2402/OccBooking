using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class DisallowOccasionTypeHandler : ICommandHandler<DisallowOccasionTypeCommand>
    {
        private IPlaceRepository _placeRepository;

        public DisallowOccasionTypeHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(DisallowOccasionTypeCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            place.DisallowParty(command.Type);

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}