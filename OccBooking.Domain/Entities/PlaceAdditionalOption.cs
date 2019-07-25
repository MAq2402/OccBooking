using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class PlaceAdditionalOption
    {
        public PlaceAdditionalOption(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }
        public string Name { get; private set; }
        public decimal Cost { get; private set; }
    }
}
