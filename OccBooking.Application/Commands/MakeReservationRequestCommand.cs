using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Commands
{
    public class MakeReservationRequestCommand : ICommand
    {
        public MakeReservationRequestCommand(string clientFirstName, string clientLastName, string clientEmail,
            string clientPhoneNumber, DateTime date, IEnumerable<AdditionalOptionDto> options,
            OccasionType occasionType, IEnumerable<MenuOrderDto> menuOrders, Guid placeId)
        {
            ClientFirstName = clientFirstName;
            ClientLastName = clientLastName;
            ClientEmail = clientEmail;
            ClientPhoneNumber = clientPhoneNumber;
            Date = date;
            Options = options ?? new List<AdditionalOptionDto>();
            OccasionType = occasionType;
            MenuOrders = menuOrders ?? new List<MenuOrderDto>();
            PlaceId = placeId;
        }

        public string ClientFirstName { get; }
        public string ClientLastName { get; }
        public string ClientEmail { get; }
        public string ClientPhoneNumber { get; }
        public DateTime Date { get; }
        public IEnumerable<AdditionalOptionDto> Options { get; }
        public OccasionType OccasionType { get; }
        public IEnumerable<MenuOrderDto> MenuOrders { get; }
        public Guid PlaceId { get; }
    }
}