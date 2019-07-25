﻿using OccBooking.Domain.Enums;
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
        public Place(Guid id,
            string name, 
            bool isShared, 
            bool hasRooms, 
            bool hasOwnFood, 
            decimal? costForPerson, 
            decimal? costForRent, 
            int capacity, 
            string description)
        {
            Id = id;
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
        private HashSet<PartyType> avaliableParties = new HashSet<PartyType>();
        public IEnumerable<Reservation> Reservations => reservations.AsReadOnly();
        public IEnumerable<Menu> Menus => HasOwnFood ? menus.AsReadOnly() : Enumerable.Empty<Menu>();
        public IEnumerable<PartyType> AvaliableParties => avaliableParties.ToList().AsReadOnly();
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
        public int Capacity { get; private set; }
        public string Description { get; private set; }

        public void AssignMenu(Menu menu)
        {
            if(!HasOwnFood)
            {
                throw new DomainException("Assigning menu faild becouse place does not have own food");
            }

            menus.Add(menu);
        }

        public void SupportAdditionalOption(PlaceAdditionalOption additionalOption)
        {
            AdditionalOptions = AdditionalOptions.AddOption(additionalOption);
        }
        public void AllowParty(PartyType partyType)
        {
            avaliableParties.Add(partyType);
        }
        public void MakeReservation(DateTime dateTime, 
            int amountOfPeople, 
            bool wholePlace, 
            Menu menu, 
            PartyType partyType, 
            IEnumerable<PlaceAdditionalOption> additionalOptions)
        {
            if (!Menus.Contains(menu))
            {
                throw new DomainException("Place does not contain such a menu");
            }

            if (!AvaliableParties.Contains(partyType))
            {
                throw new DomainException("Place does not allow to organize such an events");
            }

            if(amountOfPeople > Capacity)
            {
                throw new DomainException("Place capacity is less than amount of people in reservation request");
            }
            
            //TO BE DISCUSSED
            //if(Reservations.Any(r => r.DateTime.Date == dateTime.Date))
            //{

            //}

            if(!additionalOptions.All(o => AdditionalOptions.Contains(o)))
            {
                throw new DomainException("Place dose not support those options");
            }
        }
    }
}
