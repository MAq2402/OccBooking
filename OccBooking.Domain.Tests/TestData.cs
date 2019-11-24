using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Tests
{
    public static class TestData
    {
        public static Address CorrectAddress => new Address("Akacjowa", "Orzesze", "43-100", "śląskie");
        public static Client CorrectClient => new Client("Michal", "Kowalski", "michal@michal.com", "505111111");
        public static Hall CorrectHall => new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1);

        public static Menu CorrectMenu => new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
            MenuType.Vegetarian, 10);

        public static List<MenuOrder> CorrectMenuOrders => new List<MenuOrder>() {new MenuOrder(CorrectMenu, 100)};

        public static ReservationRequest CorrectReservationRequest => new ReservationRequest(
            new Guid("581feae6-c4ba-42d8-a126-eba9bf68f82e"),
            DateTime.Today, CorrectClient,
            OccasionType.FuneralMeal, new List<PlaceAdditionalOption>(), CorrectMenuOrders);

        public static Place CorrectPlace => new Place(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Calvados",
            false, "", CorrectAddress);
    }
}