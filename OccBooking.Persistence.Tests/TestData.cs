using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistence.Tests
{
    public static class TestData
    {
        public static Client CorrectClient => new Client("Michal", "Kowalski", "michal@michal.com", "505111111");

        public static Address CorrectAddress => new Address("Akacjowa", "Orzesze", "43-100", "śląskie");

        public static Place CorrectPlace => new Place(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Calvados",
            false, "", CorrectAddress, new Guid("4ea10f9e-ae5f-43a1-acfa-c82b678e6ee1"));
    }
}
