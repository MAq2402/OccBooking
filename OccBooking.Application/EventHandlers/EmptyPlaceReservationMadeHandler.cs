using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Types;
using OccBooking.Domain.Events;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.EventHandlers
{
    public class EmptyPlaceReservationMadeHandler : IEventHandler<EmptyPlaceReservationMade>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IReservationRequestRepository _reservationRequestRepository;

        public EmptyPlaceReservationMadeHandler(IPlaceRepository placeRepository, IReservationRequestRepository reservationRequestRepository)
        {
            _placeRepository = placeRepository;
            _reservationRequestRepository = reservationRequestRepository;
        }
        public async Task HandleAsync(EmptyPlaceReservationMade @event)
        {
            var place = await _placeRepository.GetPlaceAsync(@event.AggregateRootId);

            var reservationsToReject = await _reservationRequestRepository.GetReservationRequestsAsync(place.Id, @event.DateTime, false);

            reservationsToReject.ToList().ForEach(r => r.Reject());

            await _reservationRequestRepository.SaveAsync();
        }
    }
}
