using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Place : AggregateRoot
    {
        private string additionalOptions = string.Empty;
        private List<ReservationRequest> reservationReqeusts = new List<ReservationRequest>();
        private List<Menu> menus = new List<Menu>();
        private HashSet<OccasionType> availableOccasionTypes = new HashSet<OccasionType>();
        private List<Hall> halls = new List<Hall>();

        public Place(Guid id, string name, bool hasRooms, decimal costPerPerson, string description) : base(id)
        {
            SetName(name);
            HasRooms = hasRooms;
            SetCostPerPerson(costPerPerson);
            Description = description;
        }

        private Place()
        {
        }

        public IEnumerable<ReservationRequest> ReservationsRequests => reservationReqeusts.AsReadOnly();
        public IEnumerable<Menu> Menus => menus.AsReadOnly();
        public IEnumerable<OccasionType> AvailableOccasionTypes => availableOccasionTypes.ToList().AsReadOnly();
        public IEnumerable<Hall> Halls => halls.AsReadOnly();

        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions) additionalOptions; }
            set { additionalOptions = value; }
        }

        public string Name { get; private set; }
        public bool HasRooms { get; private set; }
        public decimal CostPerPerson { get; private set; }
        public string Description { get; private set; }
        public Owner Owner { get; private set; }
        public int Capacity => CalculateCapacity(halls);

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Name has not been provided");
            }

            Name = name;
        }

        private void SetCostPerPerson(decimal costPerPerson)
        {
            if (costPerPerson < 0)
            {
                throw new DomainException("Cost per person can not be lower than zero");
            }

            CostPerPerson = costPerPerson;
        }

        public void AddHall(Hall hall)
        {
            if (hall == null)
            {
                throw new DomainException("Hall has not been provided");
            }

            halls.Add(hall);
        }

        public void AssignMenu(Menu menu)
        {
            if (menu == null)
            {
                throw new DomainException("Menu has not been provided");
            }

            menus.Add(menu);
        }

        public void SupportAdditionalOption(PlaceAdditionalOption additionalOption)
        {
            AdditionalOptions = AdditionalOptions.AddOption(additionalOption);
        }

        public void AllowParty(OccasionType partyType)
        {
            availableOccasionTypes.Add(partyType);
        }

        public void MakeReservationRequest(ReservationRequest request)
        {
            if (!Menus.Contains(reservation.Menu))
            {
                throw new LackOfMenuException("Place does not contain such a menu");
            }

            if (!AvailableOccasionTypes.Contains(reservation.OccasionType))
            {
                throw new PartyIsNotAvaliableException("Place does not allow to organize such an events");
            }

            if (!DoHallsAtDateHaveEnoughCapacity(reservation.DateTime, reservation.AmountOfPeople))
            {
                throw new ReservationIsImpossibleException(
                    "Making reservation on this date and with this amount of people is impossible");
            }

            if (!reservation.AdditionalOptions.All(o => AdditionalOptions.Contains(o)))
            {
                throw new NotSupportedAdditionalOptionException("Place dose not support those options");
            }

            reservation.CalculateCost(CostPerPerson);
            ReservationsRequests.Add(reservation);
        }

        public void AcceptReservationReqeust(ReservationRequest request, IEnumerable<Hall> halls)
        {
            if (!halls.Any())
            {
                throw new DomainException("Halls has not been provided");
            }

            if (!Reservations.Contains(reservation))
            {
                throw new DomainException("Reservation does not belong to this place");
            }

            if (!HasHalls(halls))
            {
                throw new DomainException("Place does not contain given halls");
            }

            if (IsAnyHallReservedOnDate(reservation.DateTime, halls))
            {
                throw new DomainException("Some or all given halls are already reserved");
            }

            reservation.Accept(halls);

            var reservationsToReject = Reservations.Where(r =>
                !r.IsAnswered && !DoHallsAtDateHaveEnoughCapacity(r.DateTime, r.AmountOfPeople));

            foreach (var reservationToReject in reservationsToReject)
            {
                reservationToReject.Reject();
            }
        }

        private bool DoHallsAtDateHaveEnoughCapacity(DateTime dateTime, int amountOfPeople)
        {
            var acceptedReservationsAtGivenDay = Reservations.Where(r => r.DateTime == dateTime && r.IsAccepted);
            IEnumerable<Hall> freeHalls = new List<Hall>();
            if (acceptedReservationsAtGivenDay.Any())
            {
                foreach (var reservation in acceptedReservationsAtGivenDay)
                {
                    freeHalls = halls.Except(reservation.Halls);
                }
            }
            else
            {
                freeHalls = halls;
            }

            return amountOfPeople <= CalculateCapacity(freeHalls, dateTime);
        }

        private int CalculateCapacity(IEnumerable<Hall> halls)
        {
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h).Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h).Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }

        private int CalculateCapacity(IEnumerable<Hall> halls, DateTime dateTime)
        {
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h && IsHallFreeOnDate(j.SecondHall, dateTime))
                        .Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h && IsHallFreeOnDate(j.FirstHall, dateTime))
                        .Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }

        private bool IsHallFreeOnDate(Hall hall, DateTime dateTime)
        {
            return hall.IsFreeOnDate(dateTime);
        }

        private bool HasHalls(IEnumerable<Hall> halls)
        {
            return halls.All(h => Halls.Contains(h));
        }

        private bool IsAnyHallReservedOnDate(DateTime dateTime, IEnumerable<Hall> halls)
        {
            return halls.Any(h => h.IsFreeOnDate(dateTime));
        }
    }
}