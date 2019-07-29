using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class HallLink : Entity
    {
        public Hall Hall { get; private set; }
    }
}
