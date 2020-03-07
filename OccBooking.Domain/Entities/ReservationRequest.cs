using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Enums;
using OccBooking.Domain.Events;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Entities
{
    public class ReservationRequest : AggregateRoot
    {
        private string additionalOptions = string.Empty;
        private List<MenuOrder> _menuOrders;

        private ReservationRequest(Guid id,
            DateTime dateTime,
            Client client,
            OccasionType occasionType,
            IEnumerable<PlaceAdditionalOption> additionalOptions,
            IEnumerable<MenuOrder> menuOrders,
            Guid placeId,
            string placeName = "") : base(id)
        {
            SetDateTime(dateTime);
            SetClient(client);
            SetMenuOrders(menuOrders);
            OccasionType = occasionType;
            AdditionalOptions = new PlaceAdditionalOptions(additionalOptions);
            CalculateCost();
            CalculateAmountOfPeople();
            PlaceId = placeId;
            AddEvent(new ReservationRequestCreated(Id));
        }

        private ReservationRequest()
        {
        }

        public static ReservationRequest MakeReservationRequest(Guid id,
            DateTime dateTime,
            Client client,
            OccasionType occasionType,
            IEnumerable<PlaceAdditionalOption> additionalOptions,
            IEnumerable<MenuOrder> menuOrders,
            Guid placeId,
            string placeName = "")
        {
            return new ReservationRequest(id, dateTime, client, occasionType, additionalOptions, menuOrders, placeId,
                placeName);
        }

        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions) additionalOptions; }
            set { additionalOptions = value; }
        }

        public DateTime DateTime { get; private set; }
        public Client Client { get; private set; }
        public IEnumerable<MenuOrder> MenuOrders => _menuOrders;
        public Guid PlaceId { get; private set; }
        public decimal Cost { get; private set; }
        public bool IsAccepted { get; private set; }
        public bool IsRejected { get; private set; }
        public OccasionType OccasionType { get; private set; }
        public bool IsAnswered => IsRejected || IsAccepted;
        public int AmountOfPeople { get; private set; }

        private void SetDateTime(DateTime dateTime)
        {
            if (dateTime < DateTime.Today)
            {
                throw new DomainException("Could not perform reservation for past date");
            }

            DateTime = dateTime;
        }

        private void SetMenuOrders(IEnumerable<MenuOrder> menuOrders)
        {
            _menuOrders = menuOrders.ToList();
        }

        private void SetClient(Client client)
        {
            if (client == null)
            {
                throw new DomainException("Client has not been provided");
            }

            Client = client;
        }

        public void Accept(Guid placeId, IEnumerable<Guid> hallIds)
        {
            if (IsAnswered)
            {
                throw new DomainException(
                    "Reservation has been already accepted or rejected");
            }

            IsAccepted = true;
            AddEvent(new ReservationRequestAccepted(Id, placeId, hallIds));
        }

        public void Reject()
        {
            if (IsAnswered)
            {
                throw new DomainException(
                    "Reservation has been already accepted or rejected");
            }

            IsRejected = true;
            AddEvent(new ReservationRequestRejected(Id));
        }

        private void CalculateCost()
        {
            Cost = _menuOrders.Sum(o => o.Cost) +
                   AdditionalOptions.Sum(o => o.Cost);
        }

        private void CalculateAmountOfPeople()
        {
            AmountOfPeople = MenuOrders.Sum(o => o.AmountOfPeople);
        }
    }
}