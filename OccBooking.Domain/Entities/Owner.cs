using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Owner : AggregateRoot
    {
        private List<Place> _places = new List<Place>();

        public Owner(Guid id, string firstName, string lastName, string email, string phoneNumber) : base(id)
        {
            Name = new PersonName(firstName, lastName);
            Email = new Email(email);
            PhoneNumber = new PhoneNumber(phoneNumber);
        }

        private Owner()
        {

        }

        public IEnumerable<Place> Places => _places;
        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }

        public void AddPlace(Place place)
        {
            if (place == null)
            {
                throw new DomainException("Place has not been provided");
            }

            _places.Add(place);
        }
    }
}