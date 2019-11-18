using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class UpdateHallJoinsHandler : ICommandHandler<UpdateHallJoinsCommand>
    {
        private IHallRepository _hallRepository;

        public UpdateHallJoinsHandler(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<Result> HandleAsync(UpdateHallJoinsCommand command)
        {
            var hall = await _hallRepository.GetHallAsync(command.HallId);

            if (hall == null)
            {
                return Result.Fail("Hall with given id does not exist");
            }

            foreach (var join in command.Joins)
            {
                var otherHall = await _hallRepository.GetHallAsync(join.HallId);
                if (join.IsPossible && !hall.PossibleJoins.Any(j => j.ParticipatesIn(otherHall)))
                {
                    hall.AddPossibleJoin(otherHall);
                }
                else if (!join.IsPossible && hall.PossibleJoins.Any(j => j.ParticipatesIn(otherHall)))
                {
                    var joinToDelete = await _hallRepository.GetJoinAsync(hall, otherHall);
                    await _hallRepository.RemoveHallJoinAsync(joinToDelete);
                }
            }

            await _hallRepository.SaveAsync();
            
            return Result.Ok();
        }
    }
}