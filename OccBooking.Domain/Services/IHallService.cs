using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Domain.Services
{
    public interface IHallService
    {
        int CalculateCapacity(IEnumerable<Hall> halls, DateTime dateTime);
    }
}
