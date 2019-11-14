using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Domain.ValueObjects
{
    public class OccasionType : ValueObject
    {
        private OccasionType(string name)
        {
            Name = name;
        }

        public static OccasionType Create(string name)
        {
            if (AllTypes.All(t => t.Name != name))
            {
                throw new DomainException("Given occasion type does not exist");
            }

            return new OccasionType(name);
        }

        public static OccasionType Wedding => new OccasionType("Wedding");
        public static OccasionType FuneralMeal => new OccasionType("Funereal Meal");
        public static IEnumerable<OccasionType> AllTypes => new[] {Wedding, FuneralMeal};
        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}