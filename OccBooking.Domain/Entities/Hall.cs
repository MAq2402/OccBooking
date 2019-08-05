using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Hall : Entity
    {
        private List<HallJoin> possibleJoins = new List<HallJoin>();
        public Hall(Guid id, int capacity) : base(id)
        {
            SetCapacity(capacity);
        }
        public int Capacity { get; private set; }
        public Place Place { get; private set; }
        public IEnumerable<HallJoin> PossibleJoins => possibleJoins;
        private void SetCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new DomainException("Capacity of hall has to be greater than o");
            }
            Capacity = capacity;
        }
        public void AddPossibleJoin(Hall hall)
        {
            if (hall == null)
            {
                throw new DomainException("Hall has not been provided");
            }

            if (possibleJoins.Any(j => j.FirstHall == hall || j.SecondHall == hall))
            {
                throw new DomainException("Provided hall is already possible join to this hall");
            }

            var join = new HallJoin(Guid.NewGuid(), this, hall);
            possibleJoins.Add(join);
            hall.possibleJoins.Add(join);
        }
    }
}
