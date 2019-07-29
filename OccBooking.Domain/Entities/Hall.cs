using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Hall : Entity
    {
        private HashSet<HallJoin> possibleJoins = new HashSet<HallJoin>();
        public Hall(Guid id, int capacity)
        {
            Id = id;
            SetCapacity(capacity);
        }

        private void SetCapacity(int capacity)
        {
            if(capacity <= 0)
            {
                throw new DomainException("Capacity of hall has to be greater than o");
            }
            Capacity = capacity;
        }

        public int Capacity { get; private set; }
        public Place Place { get; private set; }
        public IEnumerable<HallJoin> PossibleJoins => possibleJoins.ToList();
        public void AddPossibleJoin(Hall hall)
        {
            if(hall == null)
            {
                throw new DomainException("Hall has not been provied");
            }

            if(possibleJoins.Any(j => j.Hall == hall))
            {
                throw new DomainException("Provided hall is already possible join to this hall");
            }

            if(Place != hall.Place)
            {
                throw new DomainException("Cannot create possible join between halls of different places");
            }
 
            possibleJoins.Add(new HallJoin(hall));
            hall.possibleJoins.Add(new HallJoin(this));
        }
    }
}
