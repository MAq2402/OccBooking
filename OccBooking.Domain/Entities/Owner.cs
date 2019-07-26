using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Owner : Entity
    {
        private List<Place> places = new List<Place>();

        public Owner(Guid id, string firstName, string lastName, string email, string phoneNumber)
        {
            Id = id;
            Name = new PersonName(firstName, lastName);
            Email = new Email(email);
            PhoneNumber = new PhoneNumber(phoneNumber);
        }
        public IEnumerable<Place> Places => places.AsReadOnly();
        public PersonName Name{ get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }

        public void AddPlace(Place place)
        {
            if(place == null)
            {
                throw new DomainException("Place has not been provided");
            }

            places.Add(place);
        }

        public void UpdatePlace(string name, 
            bool isShared, 
            bool hasRooms, 
            bool hasOwnFood, 
            decimal costForPerson, 
            decimal costForRent, 
            int capacity, 
            string description)
        {

        }

        
    }
}
