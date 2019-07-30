using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Reservation : Entity
    {
        private string additionalOptions = string.Empty;
        private List<Hall> halls = new List<Hall>();
        public Reservation(Guid id, 
            DateTime dateTime, 
            Client client, 
            int amountOfPeople, 
            Menu menu,
            OccasionType occasionType,
            IEnumerable<PlaceAdditionalOption> additionalOptions)
        {
            Id = id;
            DateTime = dateTime;
            Client = client;
            AmountOfPeople = amountOfPeople;
            Menu = menu;
            OccasionType = occasionType;
            AdditionalOptions = new PlaceAdditionalOptions(additionalOptions);
            
        }
        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions)additionalOptions; }
            set { additionalOptions = value; }
        }
        public IEnumerable<Hall> Halls => halls.AsReadOnly();
        public DateTime DateTime { get; private set; }
        public Client Client { get; private set; }
        public int AmountOfPeople { get; private set; }
        public Place Place { get; private set; }
        public Menu Menu { get; private set; }
        public int Cost { get; private set; }
        public bool IsAccepted { get; private set; }
        public bool IsRejected { get; private set; }
        public OccasionType OccasionType { get; private set; }
        public bool IsAnswered => IsRejected || IsAccepted;
        public void Accept()
        {
            if (IsAnswered)
            {
                throw new DomainException("Reservation has been already accepted or rejected");
            }
            IsAccepted = true;
        }
        public void Reject()
        {
            if (IsAnswered)
            {
                throw new DomainException("Reservation has been already accepted or rejected");
            }
            IsRejected = true;
        }
    }
}
