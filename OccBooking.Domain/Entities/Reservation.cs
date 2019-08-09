using OccBooking.Domain.Enums;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Helpers;

namespace OccBooking.Domain.Entities
{
    public class Reservation : Entity
    {
        private string additionalOptions = string.Empty;
        private List<HallReservations> hallReservations = new List<HallReservations>();

        public Reservation(Guid id,
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

        private Reservation()
        {

        }

        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions) additionalOptions; }
            set { additionalOptions = value; }
        }

        public IEnumerable<HallReservations> HallReservations => hallReservations;
        public IEnumerable<Hall> Halls => hallReservations.Select(x => x.Hall);
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

        public void Accept(IEnumerable<Hall> halls)
        {
            if (IsAnswered)
            {
                throw new ReservationHasBeenAlreadyAnsweredException(
                    "Reservation has been already accepted or rejected");
            }

            if (halls.Sum(h => h.Capacity) < AmountOfPeople)
            {
                throw new ToSmallCapacityException("Halls capacity is to small for this reservation");
            }

            foreach (var hall in halls)
            {
                hallReservations.Add(new HallReservations(){Hall = hall, Reservation = this});
            }
            IsAccepted = true;
        }

        public void Reject()
        {
            if (IsAnswered)
            {
                throw new ReservationHasBeenAlreadyAnsweredException(
                    "Reservation has been already accepted or rejected");
            }

            IsRejected = true;
        }

        public void CalculateCost(decimal placeCostForPerson)
        {
            Cost = AmountOfPeople * placeCostForPerson +
                   Menu.CostForPerson * AmountOfPeople +
                   AdditionalOptions.Sum(o => o.Cost);
        }
    }
}