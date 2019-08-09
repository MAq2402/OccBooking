using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class Client : ValueObject
    {
        public Client(string firstName, string lastName, string email, string phoneNumber)
        {
            Name = new PersonName(firstName, lastName);
            Email = new Email(email);
            PhoneNumber = new PhoneNumber(phoneNumber);
        }

        private Client()
        {
        }

        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Email;
            yield return PhoneNumber;
        }
    }
}