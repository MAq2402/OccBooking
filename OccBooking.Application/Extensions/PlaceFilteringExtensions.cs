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
        public static IQueryable<Place> FilterByName(this IQueryable<Place> places, string name)
        {
            return string.IsNullOrEmpty(name) ? places : places.Where(p => p.Name.Contains(name));
        }

        public static IQueryable<Place> FilterByProvince(this IQueryable<Place> places, string province)
        {
            return string.IsNullOrEmpty(province) ? places : places.Where(p => p.Address.Province.Equals(province));
        }

        public static IQueryable<Place> FilterByCity(this IQueryable<Place> places, string city)
        {
            return string.IsNullOrEmpty(city) ? places : places.Where(p => p.Address.City.Equals(city));
        }

        public static IQueryable<Place> FilterByCostPerPerson(this IQueryable<Place> places, decimal? minCostPerPerson,
            decimal? maxCostPerPerson)
        {
            if (minCostPerPerson.HasValue)
            {
                places = places.Where(p => p.Menus.Min(m => m.CostPerPerson) >= minCostPerPerson.Value);
            }

            if (maxCostPerPerson.HasValue)
            {
                places = places.Where(p => p.Menus.Max(m => m.CostPerPerson) <= maxCostPerPerson.Value);
            }

            return places;
        }

        public static IQueryable<Place> FilterByMinCapacity(this IQueryable<Place> places, int? capacity)
        {
            return !capacity.HasValue ? places : places.Where(p => p.Capacity >= capacity.Value);
        }

        public static IQueryable<Place> FilterByOccasionTypes(this IQueryable<Place> places,
            string occasionType)
        {
            return string.IsNullOrEmpty(occasionType)
                ? places
                : places.Where(p => p.AvailableOccasionTypes.Any(t => t.Name == occasionType));
        }
    }
}