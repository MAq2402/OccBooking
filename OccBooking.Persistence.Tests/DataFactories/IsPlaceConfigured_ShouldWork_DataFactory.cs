using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Persistence.Tests.DataFactories
{
    public class IsPlaceConfigured_ShouldWork_DataFactory
    {
        public static readonly object[] WithAllRequiredData =
        {
  
        };

        public static readonly object[] WithoutHalls =
        {
            new List<Hall>()
            {
            },
            new List<Menu>()
            {
                new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
                    MenuType.Vegetarian, 10)
            },
            new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
            false
        };

        public static readonly object[] WithoutMenus =
        {
            new List<Hall>()
            {
                new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1,
                    new Guid("27de5407-24b9-47f4-893d-b0179104f633"))
            },
            new List<Menu>()
            {
            },
            new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
            false
        };

        public static readonly object[] WithoutOccasionTypes =
        {
            new List<Hall>()
            {
                new Hall(new Guid("619e8c4e-69ae-482a-98eb-492afe60352b"), "Big", 1,
                    new Guid("27de5407-24b9-47f4-893d-b0179104f633"))
            },
            new List<Menu>()
            {
                new Menu(new Guid("17d7256c-782f-4832-b2a0-023f8ebb55f0"), "Standard",
                    MenuType.Vegetarian, 10)
            },
            new OccasionTypes(new List<OccasionType>() { }),
            false
        };
    }
}