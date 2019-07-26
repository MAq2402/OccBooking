using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            if (string.IsNullOrEmpty(value) || !value.Contains("@"))
            {
                throw new DomainException("Email is invalid");
            }

            Value = value;
        }
        public string Value { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
