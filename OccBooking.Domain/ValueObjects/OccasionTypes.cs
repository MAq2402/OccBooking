using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.ValueObjects
{
    public class OccasionTypes : ValueObject, IEnumerable<OccasionType>
    {
        private List<OccasionType> occasionTypes = new List<OccasionType>();

        public OccasionTypes(IEnumerable<OccasionType> occasionTypes)
        {
            this.occasionTypes = occasionTypes.ToList();
        }

        private OccasionTypes()
        {
        }

        public static explicit operator OccasionTypes(string occasionTypes)
        {
            return string.IsNullOrEmpty(occasionTypes)
                ? new OccasionTypes(Enumerable.Empty<OccasionType>())
                : new OccasionTypes(occasionTypes.Split(',').Select(x => OccasionType.Create(x))
                    .ToList());
        }

        public static implicit operator string(OccasionTypes occasionTypes)
        {
            return occasionTypes == null ? string.Empty : string.Join(',', occasionTypes.Select(x => x.Name));
        }

        public OccasionTypes AddType(OccasionType type)
        {
            if (type == null)
            {
                throw new DomainException("Type has not been provided");
            }

            if (occasionTypes.Contains(type))
            {
                throw new DomainException("Type is already added");
            }

            occasionTypes.Add(type);
            return new OccasionTypes(occasionTypes);
        }

        public OccasionTypes RemoveType(OccasionType type)
        {
            if (type == null)
            {
                throw new DomainException("Type has not been provided");
            }

            if (!occasionTypes.Contains(type))
            {
                throw new DomainException("Type is not in the collection");
            }

            occasionTypes.Remove(type);
            return new OccasionTypes(occasionTypes);
        }

        public override string ToString()
        {
            return string.Join(',', occasionTypes.Select(x => x.Name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var type in occasionTypes)
            {
                yield return type.Name;
            }
        }

        public IEnumerator<OccasionType> GetEnumerator()
        {
            return occasionTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}