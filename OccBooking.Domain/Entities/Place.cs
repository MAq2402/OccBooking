using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Events;

namespace OccBooking.Domain.Entities
{
    public class Place : AggregateRoot
    {
        private string additionalOptions = string.Empty;
        private string availableOccasionTypes = string.Empty;
        private List<Menu> menus = new List<Menu>();
        private List<EmptyPlaceReservation> emptyReservations = new List<EmptyPlaceReservation>();

        public Place(Guid id, string name, bool hasRooms, string description,
            Address address, Guid ownerId) : base(id)
        {
            SetName(name);
            HasRooms = hasRooms;
            Description = description;
            Address = address;
            OwnerId = ownerId;
        }

        private Place()
        {
        }

        public IEnumerable<Menu> Menus => menus;
        public IEnumerable<EmptyPlaceReservation> EmptyReservations => emptyReservations;

        public OccasionTypes AvailableOccasionTypes
        {
            get => (OccasionTypes) availableOccasionTypes;
            set => availableOccasionTypes = value;
        }

        public PlaceAdditionalOptions AdditionalOptions
        {
            get => (PlaceAdditionalOptions) additionalOptions;
            set => additionalOptions = value;
        }

        public string Name { get; private set; }
        public bool HasRooms { get; private set; }
        public string Description { get; private set; }
        public Guid OwnerId { get; private set; }
        public Address Address { get; private set; }
        public decimal? MinimalCostPerPerson => menus.Any() ? menus.Min(m => m.CostPerPerson) : (decimal?) null;

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Name has not been provided");
            }

            Name = name;
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
            AvailableOccasionTypes = AvailableOccasionTypes.AddType(partyType);
        }

        public void DisallowParty(OccasionType partyType)
        {
            AvailableOccasionTypes = AvailableOccasionTypes.RemoveType(partyType);
        }

        public void MakeEmptyReservation(DateTime date)
        {
            emptyReservations.Add(new EmptyPlaceReservation(date));

            AddEvent(new EmptyPlaceReservationMade(Id, date));
        }
    }
}