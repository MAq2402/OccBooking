using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Place : Entity
    {

        private string additionalOptions = string.Empty;
        private List<Reservation> reservations = new List<Reservation>();
        private List<Menu> menus = new List<Menu>();
        private HashSet<OccasionType> avaliableOccasionTypes = new HashSet<OccasionType>();
        private List<Hall> halls = new List<Hall>();
        public Place(Guid id,
            string name,
            bool isShared,
            bool hasRooms,
            bool hasOwnFood,
            decimal? costForPerson,
            decimal? costForRent,
            string description)
        {
            Id = id;
            SetName(name);
            IsShared = isShared;
            HasRooms = hasRooms;
            HasOwnFood = hasOwnFood;
            CostForPerson = costForPerson;
            CostForRent = costForRent;
            Description = description;
            CostForPerson = costForPerson;
        }

        public IEnumerable<Reservation> Reservations => reservations.AsReadOnly();
        public IEnumerable<Menu> Menus => HasOwnFood ? menus.AsReadOnly() : Enumerable.Empty<Menu>();
        public IEnumerable<OccasionType> AvaliableOccasionTypes => avaliableOccasionTypes.ToList().AsReadOnly();
        public IEnumerable<Hall> Halls => halls.AsReadOnly();
        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions)additionalOptions; }
            set { additionalOptions = value; }
        }
        public string Name { get; private set; }
        public bool IsShared { get; private set; }
        public bool HasRooms { get; private set; }
        public bool HasOwnFood { get; private set; }
        public decimal? CostForPerson { get; private set; }
        public decimal? CostForRent { get; private set; }
        public string Description { get; private set; }
        public int Capacity => halls.Max(h => h.PossibleJoins.Where(j => j.FirstHall == h).Sum(x => x.SecondHall.Capacity) +
                                    h.PossibleJoins.Where(j => j.SecondHall == h).Sum(x => x.FirstHall.Capacity) +
                                    h.Capacity);
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Name has not been provided");
            }
            Name = name;
        }
        public void AddHall(Hall hall)
        {
            halls.Add(hall);
        }
        public void AssignMenu(Menu menu)
        {
            if (!HasOwnFood)
            {
                throw new DomainException("Assigning menu faild becouse place does not have own food");
            }

            menus.Add(menu);
        }

        public void SupportAdditionalOption(PlaceAdditionalOption additionalOption)
        {
            AdditionalOptions = AdditionalOptions.AddOption(additionalOption);
        }
        public void AllowParty(OccasionType partyType)
        {
            avaliableOccasionTypes.Add(partyType);
        }
        public void MakeReservation(Reservation reservation)
        {
            if (!Menus.Contains(reservation.Menu))
            {
                throw new LackOfMenuException("Place does not contain such a menu");
            }

            if (!AvaliableOccasionTypes.Contains(reservation.OccasionType))
            {
                throw new PartyIsNotAvaliableException("Place does not allow to organize such an events");
            }

            if (!CheckIfReservationIsPossible(reservation.DateTime, reservation.AmountOfPeople))
            {
                throw new ReservationIsImpossibleException("Making reservation on this date and with this amount of people is impossible");
            }

            if (!reservation.AdditionalOptions.All(o => AdditionalOptions.Contains(o)))
            {
                throw new NotSupportedAdditionalOptionException("Place dose not support those options");
            }

            reservations.Add(reservation);
        }
        public bool CheckIfReservationIsPossible(DateTime dateTime, int amountOfPeople)
        {
            return true;
        }
        public int CountCapacityForDate(DateTime date)
        {
            return 0;
        }
    }
}
