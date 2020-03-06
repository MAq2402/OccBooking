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

            if (!place.ContainsHalls(halls))
            {
                throw new DomainException("Place does not contain given halls");
            }

            if (place.IsAnyHallReservedOnDate(request.DateTime, halls))
            {
                throw new DomainException("Some or all given halls are already reserved");
            }
        }

        public void ValidateMakeReservationRequest(Place place, ReservationRequest request)
        {
            if (!place.IsConfigured)
            {
                throw new DomainException("Place dose not contain all required information for the reservation request");
            }

            if (!request.MenuOrders.Select(x => x.Menu).All(m => place.Menus.Contains(m)))
            {
                throw new DomainException("Place does not contain some or all menus in reservation request");
            }

            if (!place.AvailableOccasionTypes.Contains(request.OccasionType))
            {
                throw new DomainException("Place does not allow to organize such an events");
            }

            if (request.AmountOfPeople > _hallService.CalculateCapacity(place.Halls, request.DateTime))
            {
                throw new DomainException(
                    "Making reservation on this date and with this amount of people is impossible");
            }

            if (!request.AdditionalOptions.All(o => place.AdditionalOptions.Contains(o)))
            {
                throw new DomainException("Place dose not support those options");
            }
        }
    }
}
