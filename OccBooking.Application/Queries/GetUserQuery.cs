using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public GetUserQuery(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}