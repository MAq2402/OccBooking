using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class MakeEmptyReservationsHandler : ICommandHandler<MakeEmptyReservationsCommand>
    {
        private IPlaceRepository _placeRepository;

        public MakeEmptyReservationsHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(MakeEmptyReservationsCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            foreach (var date in command.Dates)
            {
                if (place.EmptyReservations.All(r =>
                    r.Date != date.LocalDateTime))
                {
                    place.MakeEmptyReservation(date.LocalDateTime);
                }
            }

            await _placeRepository.SaveAsync();
            return Result.Ok();
        }
    }
}