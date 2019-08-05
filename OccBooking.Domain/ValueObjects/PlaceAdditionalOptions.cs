using OccBooking.Domain.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class PlaceAdditionalOptions : ValueObject, IEnumerable<PlaceAdditionalOption>
    {
        private List<PlaceAdditionalOption> additionalOptions = new List<PlaceAdditionalOption>();
        public PlaceAdditionalOptions(IEnumerable<PlaceAdditionalOption> additionalOptions)
        {
            this.additionalOptions = additionalOptions.ToList();
        }
        public IEnumerator<PlaceAdditionalOption> GetEnumerator()
        {
            return additionalOptions.GetEnumerator();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var additionalOption in additionalOptions.OrderBy(x => x.Name))
            {
                yield return additionalOption.Name;
                yield return additionalOption.Cost;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static explicit operator PlaceAdditionalOptions(string additionalOptions)
        {
            return string.IsNullOrEmpty(additionalOptions) ?
                new PlaceAdditionalOptions(Enumerable.Empty<PlaceAdditionalOption>()):
                new PlaceAdditionalOptions(additionalOptions.Split(';').Select(x => (PlaceAdditionalOption)x).ToList());
        }

        public static implicit operator string(PlaceAdditionalOptions additionalOptions)
        {
            return string.Join(';', additionalOptions.Select(x => (string)x));
        }
        public PlaceAdditionalOptions AddOption(PlaceAdditionalOption additionalOption)
        {
            if (additionalOption == null)
            {
                throw new DomainException("Option has not been provided");
            }

            additionalOptions.Add(additionalOption);
            return new PlaceAdditionalOptions(additionalOptions);
        }
    }
}
