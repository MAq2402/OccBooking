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

        public static IQueryable<Place> FilterByCostPerPerson(this IQueryable<Place> places,
            decimal? minCostPerPerson,
            decimal? maxCostPerPerson)
        {
            if (minCostPerPerson.HasValue)
            {
                places = places.Where(p =>
                    p.MinimalCostPerPerson.HasValue && p.MinimalCostPerPerson >= minCostPerPerson.Value);
            }

            if (maxCostPerPerson.HasValue)
            {
                places = places.Where(p =>
                    p.MinimalCostPerPerson.HasValue && p.MinimalCostPerPerson <= maxCostPerPerson.Value);
            }

            return places;
        }

        public static IQueryable<Place> FilterByMinCapacity(this IQueryable<Place> places, IQueryable<Hall> halls,
            int? capacity)
        {
            return !capacity.HasValue
                ? places
                : places.Where(p => CalculateCapacity(halls.Where(h => h.PlaceId == p.Id)) >= capacity.Value);
        }

        private static int CalculateCapacity(IQueryable<Hall> halls)
        {
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h).Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h).Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }

        public static IQueryable<Place> FilterByOccasionTypes(this IQueryable<Place> places,
            string occasionType)
        {
            return string.IsNullOrEmpty(occasionType)
                ? places
                : places.Where(p => p.AvailableOccasionTypes.Any(t => t.Name == occasionType));
        }

        public static IQueryable<Place> FilterByDate(this IQueryable<Place> places, IQueryable<Hall> halls,
            DateTimeOffset? from, DateTimeOffset? to)
        {
            var dates = new List<DateTime>();
            if (from.HasValue && to.HasValue)
            {
                dates = GetDatesRange(from.Value.LocalDateTime, to.Value.LocalDateTime);
            }
            else if (to.HasValue)
            {
                dates = GetDatesRange(DateTime.Today.Date, to.Value.LocalDateTime);
            }

            if (dates.Any())
            {
                places = places.Where(p =>
                    dates.Except(p.EmptyReservations.Select(e => e.Date)).Any());

                places = places.Where(p =>
                    !halls.Any(h => h.PlaceId == p.Id) || dates.Any(d =>
                        halls.Where(h => h.PlaceId == p.Id).Any(h => h.IsFreeOnDate(d))));
            }

            return places;
        }

        private static List<DateTime> GetDatesRange(DateTime from, DateTime to)
        {
            var dates = new List<DateTime>();

            for (var dt = from; dt <= to; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }
    }
}