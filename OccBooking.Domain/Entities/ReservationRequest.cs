using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Entities
{
    public class ReservationRequest : AggregateRoot
    {
        private string additionalOptions = string.Empty;
        private string occasionTypes = string.Empty;

        public ReservationRequest(Guid id,
            DateTime dateTime,
            Client client,
            int amountOfPeople,
            Menu menu,
            OccasionType occasionType,
            IEnumerable<PlaceAdditionalOption> additionalOptions) : base(id)
        {
            SetDateTime(dateTime);
            SetClient(client);
            SetAmountOfPeople(amountOfPeople);
            SetMenu(menu);
            OccasionType = occasionType;
            AdditionalOptions = new PlaceAdditionalOptions(additionalOptions);
        }

        private ReservationRequest()
        {

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
        public decimal Cost { get; private set; }
        public bool IsAccepted { get; private set; }
        public bool IsRejected { get; private set; }
        public OccasionType OccasionType { get; private set; }
        public bool IsAnswered => IsRejected || IsAccepted;

        private void SetDateTime(DateTime dateTime)
        {
            if (dateTime < DateTime.Today)
            {
                throw new DomainException("Could not perform reservation for past date");
            }

            DateTime = dateTime;
        }

        private void SetMenu(Menu menu)
        {
            if (menu == null)
            {
                throw new DomainException("Menu for reservation has not been provided");
            }

            Menu = menu;
        }

        private void SetClient(Client client)
        {
            if (client == null)
            {
                throw new DomainException("Client has not been provided");
            }

            Client = client;
        }

        private void SetAmountOfPeople(int amountOfPeople)
        {
            if (amountOfPeople <= 0)
            {
                throw new DomainException("Reservation amount of people has to be greater than 0");
            }

            AmountOfPeople = amountOfPeople;
        }

        public void Accept()
        {
            if (IsAnswered)
            {
                throw new DomainException(
                    "Reservation has been already accepted or rejected");
            }

            IsAccepted = true;
        }

        public void Reject()
        {
            if (IsAnswered)
            {
                throw new DomainException(
                    "Reservation has been already accepted or rejected");
            }

            IsRejected = true;
        }

        public void CalculateCost(decimal placeCostForPerson)
        {
            Cost = AmountOfPeople * placeCostForPerson +
                   Menu.CostPerPerson * AmountOfPeople +
                   AdditionalOptions.Sum(o => o.Cost);
        }
    }
}
