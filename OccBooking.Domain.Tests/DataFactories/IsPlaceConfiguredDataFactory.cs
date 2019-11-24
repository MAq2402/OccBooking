using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;
using static OccBooking.Domain.Tests.TestData;

namespace OccBooking.Domain.Tests.DataFactories
{
    public static class IsPlaceConfiguredDataFactory
    {
        public static readonly object[] WithAllRequiredData =
        {
            new List<Hall>()
            {
                CorrectHall
            },
            new List<Menu>()
            {
                CorrectMenu
            },
            new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
            true
        };

        public static readonly object[] WithoutHalls =
        {
            new List<Hall>()
            {
            },
            new List<Menu>()
            {
                CorrectMenu
            },
            new OccasionTypes(new List<OccasionType>() {OccasionType.Wedding}),
            false
        };

        public static readonly object[] WithoutMenus =
        {
            new List<Hall>()
            {
                CorrectHall
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
                CorrectHall
            },
            new List<Menu>()
            {
                CorrectMenu
            },
            new OccasionTypes(new List<OccasionType>() {}),
            false
        };
    }
}