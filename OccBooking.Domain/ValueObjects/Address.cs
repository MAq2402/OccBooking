using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private List<string> existingProvinces = new List<string>()
        {
            "dolnośląskie",
            "kujawsko-pomorskie",
            "lubelskie",
            "łódzkie",
            "małopolskie",
            "mazowieckie",
            "opolskie",
            "podkarpackie",
            "podlaskie",
            "pomorskie",
            "śląskie",
            "świetokrzyskie",
            "warmińsko-mazurskie",
            "wielkopolskie",
            "zachodniopomorskie"
        };

        public Address(string street, string city, string zipCode, string province)
        {
            SetStreet(street);
            SetCity(city);
            SetZipCode(zipCode);
            SetProvince(province);
        }

        private Address()
        {

        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Province { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
            yield return Province;
        }

        private void SetStreet(string street)
        {
            if (string.IsNullOrEmpty(street))
            {
                throw new DomainException("Street has not been provided");
            }

            Street = street;
        }

        private void SetCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                throw new DomainException("City has not been provided");
            }

            City = city;
        }

        private void SetZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                throw new DomainException("Zip code has not been provided");
            }

            if (!Regex.IsMatch(zipCode, "[0-9]{2}-[0-9]{3}"))
            {
                throw new DomainException("Zip code is in wrong format");
            }

            ZipCode = zipCode;
        }

        private void SetProvince(string province)
        {
            if (string.IsNullOrEmpty(province))
            {
                throw new DomainException("Province has not been provided");
            }

            if (!existingProvinces.Contains(province))
            {
                throw new DomainException("Given province does not exist");
            }

            Province = province;
        }
    }
}