using OccBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Place : Entity
    {
        public Place(string name, 
            bool isShared, 
            bool hasRooms, 
            bool hasOwnFood, 
            decimal? costForPerson, 
            decimal? costForRent, 
            int capacity, 
            string description)
        {
            Name = name;
            IsShared = isShared;
            HasRooms = hasRooms;
            HasOwnFood = hasOwnFood;
            CostForPerson = CostForPerson;
            CostForRent = costForRent;
            Capacity = capacity;
            Description = description;
            CostForPerson = costForPerson;
        }
        private List<Reservation> reservations = new List<Reservation>();
        private List<Menu> menus = new List<Menu>();
        private List<PartyType> avaliableParties = new List<PartyType>();
        private List<PlaceAdditionalOption> additionalOptions = new List<PlaceAdditionalOption>();
        public IEnumerable<Reservation> Reservations => reservations.AsReadOnly();
        public IEnumerable<Menu> Menus => menus.AsReadOnly();
        public IEnumerable<PartyType> AvaliableParties => avaliableParties.AsReadOnly();
        public IEnumerable<PlaceAdditionalOption> AdditionalOptions => additionalOptions.AsReadOnly();
        public string Name { get; private set; }
        public bool IsShared { get; private set; }
        public bool HasRooms { get; private set; }
        public bool HasOwnFood { get; private set; }
        public decimal? CostForPerson { get; private set; }
        public decimal? CostForRent { get; private set; }
        public int Capacity { get; private set; }
        public string Description { get; private set; }

        public void AssignMenu(Menu menu)
        {

        }

        public void AssignAdditionalOption(PlaceAdditionalOption additionalOption)
        {
           // if(additionalOption)
        }
        public void MakeReservation(DateTime date, 
            int amountOfPeople, 
            bool wholePlace, 
            Menu menu, 
            PartyType partyType, 
            IEnumerable<PlaceAdditionalOption> additionalOptions)
        {

        }

        public void AllowParty(PartyType partyType)
        {

        }
    }
}
