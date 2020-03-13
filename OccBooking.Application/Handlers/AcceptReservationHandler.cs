using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.Services;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AcceptReservationHandler : ICommandHandler<AcceptReservationCommand>
    {
        private IReservationRequestRepository _reservationRequestRepository;
        private IHallRepository _hallRepository;
        private IPlaceRepository _placeRepository;
        private IReservationRequestService _reservationRequestService;

        public AcceptReservationHandler(IReservationRequestRepository reservationRequestRepository,
            IHallRepository hallRepository,
            IPlaceRepository placeRepository,
            IReservationRequestService reservationRequestService)
        {
            _reservationRequestRepository = reservationRequestRepository;
            _hallRepository = hallRepository;
            _placeRepository = placeRepository;
            _reservationRequestService = reservationRequestService;
        }

        public async Task<Result> HandleAsync(AcceptReservationCommand command)
        {
            var request = await _reservationRequestRepository.GetReservationRequestAsync(command.ReservationId);

            if (request == null)
            {
                return Result.Fail("Request with given id does not exist");
            }

            var halls = await _hallRepository.GetHallsAsync(command.HallIds);

            var place = await _placeRepository.GetPlaceAsync(request.PlaceId);

            _reservationRequestService.ValidateAcceptReservationRequest(place, request, halls);

            request.Accept(place.Id, halls.Select(h => h.Id));

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}