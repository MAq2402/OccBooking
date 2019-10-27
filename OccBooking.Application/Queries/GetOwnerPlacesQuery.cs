using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetOwnerPlacesQuery : IQuery<IEnumerable<PlaceDto>>
    {
        public GetOwnerPlacesQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }

        public Guid OwnerId { get; }
    }
}