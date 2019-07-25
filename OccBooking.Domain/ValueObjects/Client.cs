using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class Client : ValueObject
    {
        public Client()
        {

        }

        public string FirstName { get; private set; }
        public string LastName { get; set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Email;
            yield return PhoneNumber;
        }
    }
}
