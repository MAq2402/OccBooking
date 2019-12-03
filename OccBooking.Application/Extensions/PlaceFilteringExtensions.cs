using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Extensions
{
    public static class PlaceFilteringExtensions
    {
        public static IEnumerable<Place> FilterByName(this IEnumerable<Place> places, string name)
        {
            return string.IsNullOrEmpty(name) ? places : places.Where(p => p.Name.Contains(name));
        }

        public static IEnumerable<Place> FilterByProvince(this IEnumerable<Place> places, string province)
        {
            return string.IsNullOrEmpty(province) ? places : places.Where(p => p.Address.Province.Equals(province));
        }

        public static IEnumerable<Place> FilterByCity(this IEnumerable<Place> places, string city)
        {
            return string.IsNullOrEmpty(city) ? places : places.Where(p => p.Address.City.Equals(city));
        }

        public static IEnumerable<Place> FilterByCostPerPerson(this IEnumerable<Place> places, decimal? minCostPerPerson,
            decimal? maxCostPerPerson)
        {
            if (minCostPerPerson.HasValue)
            {
                places = places.Where(p => p.MinimalCostPerPerson.HasValue && p.MinimalCostPerPerson >= minCostPerPerson.Value);
            }

            if (maxCostPerPerson.HasValue)
            {
                places = places.Where(p => p.MinimalCostPerPerson.HasValue && p.MinimalCostPerPerson <= maxCostPerPerson.Value);
            }

            return places;
        }

        public static IEnumerable<Place> FilterByMinCapacity(this IEnumerable<Place> places, int? capacity)
        {
            return !capacity.HasValue ? places : places.Where(p => p.Capacity >= capacity.Value);
        }

        public static IEnumerable<Place> FilterByOccasionTypes(this IEnumerable<Place> places,
            string occasionType)
        {
            return string.IsNullOrEmpty(occasionType)
                ? places
                : places.Where(p => p.AvailableOccasionTypes.Any(t => t.Name == occasionType));
        }
    }
}