using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.Contracts;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class UploadPlaceImageCommand : ICommand
    {
        public UploadPlaceImageCommand(IFile file, Guid placeId)
        {
            File = file;
            PlaceId = placeId;
        }

        public IFile File { get; }
        public Guid PlaceId { get; }
    }
}
