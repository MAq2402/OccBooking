using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class PlaceAdditionalOption : ValueObject
    {
        public PlaceAdditionalOption(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }

        private PlaceAdditionalOption()
        {
        }

        public string Name { get; private set; }
        public decimal Cost { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Cost;
        }

        public static explicit operator PlaceAdditionalOption(string additionalOption)
        {
            var splittedOption = additionalOption.Split(',');
            return new PlaceAdditionalOption(splittedOption[0], Convert.ToDecimal(splittedOption[1]));
        }

        public static explicit operator string(PlaceAdditionalOption additionalOption)
        {
            return $"{additionalOption.Name},{additionalOption.Cost}";
        }
    }
}