using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class AddHallCommand : ICommand
    {
        public AddHallCommand(string name, int capacity, Guid placeId)
        {
            Name = name;
            Capacity = capacity;
            PlaceId = placeId;
        }

        public Guid PlaceId { get; }
        public string Name { get; }
        public int Capacity { get; }
    }
}