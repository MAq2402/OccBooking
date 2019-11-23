using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Entities;

namespace OccBooking.Application.Handlers
{
    public class UploadPlaceImageHandler : ICommandHandler<UploadPlaceImageCommand>
    {
        private OccBookingDbContext _dbContext;

        public UploadPlaceImageHandler(OccBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> HandleAsync(UploadPlaceImageCommand command)
        {
            using (var memoryStream = new MemoryStream())
            {
                await command.File.CopyToAsync(memoryStream);

                var placeImage = new PlaceImage()
                {
                    Id = Guid.NewGuid(),
                    PlaceId = command.PlaceId,
                    Content = memoryStream.ToArray()
                };
                _dbContext.PlaceImages.Add(placeImage);

                await _dbContext.SaveChangesAsync();
            }

            return Result.Ok();
        }
    }
}