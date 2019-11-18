using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class HallJoin : Entity
    {
        public HallJoin(Guid id, Hall firstHall, Hall secondHall) : base(id)
        {
            FirstHall = firstHall;
            SecondHall = secondHall;
        }

        private HallJoin()
        {
        }

        public Hall FirstHall { get; private set; }
        public Hall SecondHall { get; private set; }

        public bool ParticipatesIn(Hall hall)
        {
            return FirstHall == hall || SecondHall == hall;
        }
    }
}