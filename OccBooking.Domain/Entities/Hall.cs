using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Hall : AggregateRoot
    {
        private List<HallJoin> possibleJoinsWhereIsFirst = new List<HallJoin>();
        private List<HallJoin> possibleJoinsWhereIsSecond = new List<HallJoin>();
        private List<HallReservation> hallReservations = new List<HallReservation>();

        public Hall(Guid id, string name, int capacity) : base(id)
        {
            SetCapacity(capacity);
            SetName(name);
        }

        private Hall()
        {
        }

        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public Place Place { get; private set; }
        public IEnumerable<HallJoin> PossibleJoinsWhereIsFirst => possibleJoinsWhereIsFirst;
        public IEnumerable<HallJoin> PossibleJoinsWhereIsSecond => possibleJoinsWhereIsSecond;
        public IEnumerable<HallJoin> PossibleJoins => possibleJoinsWhereIsFirst.Concat(possibleJoinsWhereIsSecond);
        public IEnumerable<HallReservation> HallReservations => hallReservations;

        private void SetCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new DomainException("Capacity of hall has to be greater than o");
            }

            Capacity = capacity;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Hall name has not been provided");
            }

            Name = name;
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

        public bool IsFreeOnDate(DateTime date)
        {
            return HallReservations.All(hr => hr.ReservationRequest.DateTime != date);
        }

        public void MakeReservation(ReservationRequest request)
        {
            hallReservations.Add(new HallReservation(request));
        }
    }
}