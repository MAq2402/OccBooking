using OccBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Reservation : Entity
    {
        private List<PlaceAdditionalOption> additionalOptions = new List<PlaceAdditionalOption>();
        public DateTime Date { get; private set; }
        //KTO
        public int AmountOfPeople { get; private set; }
        public Place Place { get; private set; }
        public Menu Menu { get; private set; }
        public IEnumerable<PlaceAdditionalOption> PlaceAdditionalOptions => additionalOptions.AsReadOnly();
        public int Cost { get; private set; }// => AmountOfPeople * placeCost or placeCost
        public bool IsAccepted { get; private set; }
        public bool WholePlace { get; set; }
        public PartyType MyProperty { get; set; }
        public void Accept()
        {

        }
        public void Reject()
        {

        }
    }
}
