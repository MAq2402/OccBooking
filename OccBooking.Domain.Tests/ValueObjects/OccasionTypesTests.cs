using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OccBooking.Domain.Exceptions;
using OccBooking.Domain.ValueObjects;
using Xunit;

namespace OccBooking.Domain.Tests.ValueObjects
{
    public class OccasionTypesTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("Wedding", 1)]
        [InlineData("Wedding,Funereal Meal", 2)]
        public void ExplicitOperatorShouldWork(string occasionTypesAsString, int expected)
        {
            var occasionTypes = (OccasionTypes) occasionTypesAsString;

            var actual = occasionTypes.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitOperatorShouldWork()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.FuneralMeal,
                OccasionType.Wedding
            });

            var actual = "Funereal Meal,Wedding";
            string expected = occasionTypes;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ToStringShouldWork()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.FuneralMeal,
                OccasionType.Wedding
            });

            var actual = "Funereal Meal,Wedding";
            var expected = occasionTypes.ToString();

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void AddTypeShouldFail_TypeHasNotBeenProvided()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>());

            Assert.Throws<DomainException>(() => occasionTypes.AddType(null));
        }

        [Fact]
        public void RemoveTypeShouldFail_TypeHasNotBeenProvided()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>());

            Assert.Throws<DomainException>(() => occasionTypes.RemoveType(null));
        }

        [Fact]
        public void AddTypeShouldFail_TypeIsAlreadyAdded()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.Wedding
            });

            Assert.Throws<DomainException>(() => occasionTypes.AddType(OccasionType.Wedding));
        }

        [Fact]
        public void RemoveTypeShouldFail_TypeIsNotInCollection()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>());

            Assert.Throws<DomainException>(() => occasionTypes.RemoveType(OccasionType.Wedding));
        }


        [Fact]
        public void RemoveTypeShouldWork()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.Wedding,
                OccasionType.FuneralMeal
            });

            var expected = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.Wedding,
            });
            var actual = occasionTypes.RemoveType(OccasionType.FuneralMeal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddTypeShouldWork()
        {
            var occasionTypes = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.Wedding
            });

            var expected = new OccasionTypes(new List<OccasionType>()
            {
                OccasionType.Wedding,
                OccasionType.FuneralMeal
            });
            var actual = occasionTypes.AddType(OccasionType.FuneralMeal);

            Assert.Equal(expected, actual);
        }
    }
}