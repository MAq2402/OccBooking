using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class CreatePlaceCommand : ICommand
    {
        public CreatePlaceCommand(string name, bool hasRooms, decimal costPerPerson, string description, string street,
            string city, string zipCode, string province, Guid ownerId)
        {
            Id = Guid.NewGuid();
            Name = name;
            HasRooms = hasRooms;
            CostPerPerson = costPerPerson;
            Description = description;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Province = province;
            OwnerId = ownerId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool HasRooms { get; private set; }
        public decimal CostPerPerson { get; private set; }
        public string Description { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Province { get; private set; }
        public Guid OwnerId { get; private set; }
    }
}