using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.Entities
{
    public class MenuOrder : Entity
    {
        public MenuOrder(Menu menu, int amountOfPeople)
        {
            Menu = menu;
            SetAmountOfPeople(amountOfPeople);
        }

        private MenuOrder()
        {
        }

        public Menu Menu { get; private set; }
        public int AmountOfPeople { get; private set; }
        public ReservationRequest ReservationRequest { get; private set; }
        public decimal Cost => Menu.CostPerPerson * AmountOfPeople;

        private void SetAmountOfPeople(int amountOfPeople)
        {
            if (amountOfPeople <= 0)
            {
                throw new DomainException("Reservation amount of people has to be greater than 0");
            }

            AmountOfPeople = amountOfPeople;
        }
    }
}