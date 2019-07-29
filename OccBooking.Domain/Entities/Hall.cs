using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Hall : Entity
    {
        public Hall(Guid id, int capacity)
        {
            Id = id;
            Capacity = capacity;
        }
        public int Capacity { get; private set; }
        public Place Place { get; private set; }
        public IEnumerable<HallLink> PossibleLinks { get; private set; }
    }
}
