using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class MakeEmptyHallReservationsHandler : ICommandHandler<MakeEmptyHallReservationsCommand>
    {
        private IHallRepository _hallRepository;

        public MakeEmptyHallReservationsHandler(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result> HandleAsync(MakeEmptyHallReservationsCommand command)
        {
            var hall = await _hallRepository.GetHallAsync(command.HallId);

            if (hall == null)
            {
                return Result.Fail("Hall with given id does not exist");
            }

            foreach (var date in command.Dates)
            {
                if (hall.HallReservations.All(r =>
                    r.Date != date))
                {
                    hall.MakeEmptyReservation(date);
                }
            }

            await _hallRepository.SaveAsync();
            return Result.Ok();
        }
    }
}
