using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Types;
using OccBooking.Domain.Events;
using OccBooking.Domain.Services;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.EventHandlers
{
    public class EmptyHallReservationMadeHandler : IEventHandler<EmptyHallReservationMade>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IReservationRequestRepository _reservationRequestRepository;

        public EmptyHallReservationMadeHandler(IPlaceRepository placeRepository, IReservationRequestRepository reservationRequestRepository)
        {
            _placeRepository = placeRepository;
            _reservationRequestRepository = reservationRequestRepository;
        }

        public async Task HandleAsync(EmptyHallReservationMade @event)
        {
            var place = await _placeRepository.GetPlaceByHallAsync(@event.AggregateRootId);

            var reservationsToReject = await _reservationRequestRepository.GetImpossibleReservationRequestsAsync(place.Id, @event.DateTime);

            reservationsToReject.ToList().ForEach(r => r.Reject());

            await _reservationRequestRepository.SaveAsync();
        }
    }
}
