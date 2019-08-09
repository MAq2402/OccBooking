using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string value)
        {
            if (value.Any(x => !char.IsDigit(x)))
            {
                throw new DomainException("Invalid phone number");
            }

            Value = value;
        }

        private PhoneNumber()
        {

        }
        public string Value { get;private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
