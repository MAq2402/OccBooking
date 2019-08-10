using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OccBooking.Domain.Helpers;

namespace OccBooking.Domain.Entities
{
    public class Hall : Entity
    {
        private List<HallJoin> possibleJoinsWhereIsFirst = new List<HallJoin>();
        private List<HallJoin> possibleJoinsWhereIsSecond = new List<HallJoin>();

        private List<HallReservations> hallReservations = new List<HallReservations>();
        public Hall(Guid id, int capacity) : base(id)
        {
            SetCapacity(capacity);
        }

        private Hall()
        {

        }
        public int Capacity { get; private set; }
        public Place Place { get; private set; }
        public IEnumerable<HallJoin> PossibleJoinsWhereIsFirst => possibleJoinsWhereIsFirst;
        public IEnumerable<HallJoin> PossibleJoinsWhereIsSecond => possibleJoinsWhereIsSecond;
        public IEnumerable<HallJoin> PossibleJoins => possibleJoinsWhereIsFirst.Concat(possibleJoinsWhereIsSecond);
        public IEnumerable<HallReservations> HallReservations => hallReservations;
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

            if (PossibleJoins.Any(j => j.FirstHall == hall || j.SecondHall == hall))
            {
                throw new DomainException("Provided hall is already possible join to this hall");
            }

            var join = new HallJoin(Guid.NewGuid(), this, hall);
            possibleJoinsWhereIsFirst.Add(join);
            hall.possibleJoinsWhereIsSecond.Add(join);
        }
    }
}
