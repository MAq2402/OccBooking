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
    public class AllowOccasionTypeHandler : ICommandHandler<AllowOccasionTypeCommand>
    {
        private IPlaceRepository _placeRepository;

        public AllowOccasionTypeHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(AllowOccasionTypeCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            place.AllowParty(OccasionType.Create(command.Type));

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}