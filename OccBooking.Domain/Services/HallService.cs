using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Domain.Services
{
    public class HallService : IHallService
    {
        public int CalculateCapacity(IEnumerable<Hall> halls, DateTime dateTime)
        {
            halls = halls.Where(h => h.IsFreeOnDate(dateTime));
            return halls.Any()
                ? halls.Max(h =>
                    h.PossibleJoins.Where(j => j.FirstHall == h && j.SecondHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.SecondHall.Capacity) +
                    h.PossibleJoins.Where(j => j.SecondHall == h && j.FirstHall.IsFreeOnDate(dateTime))
                        .Sum(x => x.FirstHall.Capacity) + h.Capacity)
                : 0;
        }
    }
}