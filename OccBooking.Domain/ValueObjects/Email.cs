using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            if (string.IsNullOrEmpty(value) || !IsValidEmail(value))
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
        private bool IsValidEmail(string value)
        {
            return new EmailAddressAttribute().IsValid(value);
        }
    }
}
