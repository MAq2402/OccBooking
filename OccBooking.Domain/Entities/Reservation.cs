using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Reservation : Entity
    {
        private string additionalOptions = string.Empty;
        public Reservation(Guid id, 
            DateTime dateTime, 
            Client client, 
            int amountOfPeople, 
            Menu menu, 
            bool wholePlace, 
            PartyType type,
            IEnumerable<PlaceAdditionalOption> additionalOptions)
        {
            Id = id;
            DateTime = dateTime;
            Client = client;
            AmountOfPeople = amountOfPeople;
            Menu = menu;
            WholePlace = wholePlace;
            Type = type;
            AdditionalOptions = new PlaceAdditionalOptions(additionalOptions);
            
        }
        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions)additionalOptions; }
            set { additionalOptions = value; }
        }
        public DateTime DateTime { get; private set; }
        public Client Client { get; private set; }
        public int AmountOfPeople { get; private set; }
        public Place Place { get; private set; }
        public Menu Menu { get; private set; }
        public int Cost { get; private set; }// => AmountOfPeople * placeCost or placeCost
        public bool IsAccepted { get; private set; }
        public bool WholePlace { get; private set; }
        public PartyType Type { get; private set; }
        public void Accept()
        {

        }
        public void Reject()
        {

        }
    }
}
