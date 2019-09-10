using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class CreatePlaceCommand : ICommand
    {
        public CreatePlaceCommand(string name, bool hasRooms, decimal costPerPerson, string description)
        {
            Name = name;
            HasRooms = hasRooms;
            CostPerPerson = costPerPerson;
            Description = description;
        }

        public string Name { get; private set; }
        public bool HasRooms { get; private set; }
        public decimal CostPerPerson { get; private set; }
        public string Description { get; private set; }
    }
}