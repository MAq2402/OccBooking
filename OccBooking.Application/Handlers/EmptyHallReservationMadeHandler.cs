using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Common.Types;
using OccBooking.Domain.Events;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class EmptyHallReservationMadeHandler : IEventHandler<EmptyHallReservationMade>
    {
        private IPlaceRepository _placeRepository;

        public EmptyHallReservationMadeHandler(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task HandleAsync(EmptyHallReservationMade @event)
        {
            var place = await _placeRepository.GetPlaceByHallAsync(@event.HallId);

            place.RejectReservationsRequestsIfNotEnoughCapacity();

            await _placeRepository.SaveAsync();
        }
    }
}
