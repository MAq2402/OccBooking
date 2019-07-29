using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class HallJoin : Entity
    {
        public HallJoin(Hall hall)
        {
            Hall = hall;
        }
        public Hall Hall { get; private set; }
    }
}
