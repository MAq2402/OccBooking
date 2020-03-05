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

            if (!DoHallsHaveEnoughCapacity(place.Halls, request.DateTime, request.AmountOfPeople))
            {
                throw new DomainException(
                    "Making reservation on this date and with this amount of people is impossible");
            }

            if (!request.AdditionalOptions.All(o => place.AdditionalOptions.Contains(o)))
            {
                throw new DomainException("Place dose not support those options");
            }
        }

        public bool DoHallsHaveEnoughCapacity(IEnumerable<Hall> halls, DateTime dateTime, int amountOfPeople)
        {
            return amountOfPeople <= CalculateCapacity(halls.Where(h => h.IsFreeOnDate(dateTime)), dateTime);
        }

        private int CalculateCapacity(IEnumerable<Hall> halls, DateTime dateTime)
        {
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h && j.SecondHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h && j.FirstHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }
    }
}
