using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.Services
{
    public class ReservationRequestService : IReservationRequestService
    {
        private readonly IHallService _hallService;

        public ReservationRequestService(IHallService hallService)
        {
            _hallService = hallService;
        }

        public void ValidateAcceptReservationRequest(Place place, ReservationRequest request, IEnumerable<Hall> halls)
        {
            if (!halls.Any())
            {
                throw new DomainException("Halls has not been provided");
            }

            if (!halls.All(h => h.PlaceId == place.Id))
            {
                throw new DomainException("Place does not contain given halls");
            }

            if (!halls.All(h => h.IsFreeOnDate(request.DateTime)))
            {
                throw new DomainException("Some or all given halls are already reserved");
            }
        }

        public void ValidateMakeReservationRequest(Place place, ReservationRequest request, Func<Guid, bool> isPlaceConfigured)
        {
            if (!isPlaceConfigured(place.Id))
            {
                throw new DomainException("Place dose not contain all required information for the reservation request");
            }

            if (!place.AvailableOccasionTypes.Contains(request.OccasionType))
            {
                throw new DomainException("Place does not allow to organize such an events");
            }

            if (!request.AdditionalOptions.All(o => place.AdditionalOptions.Contains(o)))
            {
                throw new DomainException("Place dose not support those options");
            }
        }
    }
}
