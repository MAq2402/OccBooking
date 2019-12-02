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
    public class RejectReservationHandler : ICommandHandler<RejectReservationCommand>
    {
        private IReservationRequestRepository _reservationRequestRepository;

        public RejectReservationHandler(IReservationRequestRepository reservationRequestRepository)
        {
            _reservationRequestRepository = reservationRequestRepository;
        }
        public async Task<Result> HandleAsync(RejectReservationCommand command)
        {
            var request = await _reservationRequestRepository.GetReservationRequestAsync(command.Id);

            if (request == null)
            {
                return Result.Fail("Request with given id does not exist");
            }
            
            request.Reject();

            await _reservationRequestRepository.SaveAsync();

            return Result.Ok();
        }
    }
}
