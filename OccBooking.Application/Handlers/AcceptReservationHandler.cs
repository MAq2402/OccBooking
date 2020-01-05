using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AcceptReservationHandler : ICommandHandler<AcceptReservationCommand>
    {
        private IReservationRequestRepository _reservationRequestRepository;
        private IHallRepository _hallRepository;
        private IPlaceRepository _placeRepository;

        public AcceptReservationHandler(IReservationRequestRepository reservationRequestRepository,
            IHallRepository hallRepository, IPlaceRepository placeRepository)
        {
            _reservationRequestRepository = reservationRequestRepository;
            _hallRepository = hallRepository;
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(AcceptReservationCommand command)
        {
            var request = await _reservationRequestRepository.GetReservationRequestAsync(command.ReservationId);

            if (request == null)
            {
                return Result.Fail("Request with given id does not exist");
            }

            var halls = await _hallRepository.GetHallsAsync(command.HallIds);

            var place = await _placeRepository.GetPlaceAsync(request.Place.Id);

            place.AcceptReservationRequest(request, halls);

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}