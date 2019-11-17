using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class MakeEmptyReservationsHandler : ICommandHandler<MakeEmptyReservationsCommand>
    {
        private IHallRepository _hallRepository;

        public MakeEmptyReservationsHandler(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result> HandleAsync(MakeEmptyReservationsCommand command)
        {
            var halls = await _hallRepository.GetHallsAsync(command.PlaceId);

            foreach (var hall in halls)
            {
                foreach (var date in command.Dates)
                {
                    hall.MakeEmptyReservation(date.LocalDateTime.Date);
                }
            }

            await _hallRepository.SaveAsync();
            return Result.Ok();
        }
    }
}