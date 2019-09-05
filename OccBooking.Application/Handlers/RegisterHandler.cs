using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.Contracts;
using OccBooking.Common.Hanlders;

namespace OccBooking.Application.Handlers
{
    public class RegisterHandler : ICommandHandler<RegisterCommand>
    {
        private IAuthService _authService;

        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result> HandleAsync(RegisterCommand command)
        {
            return await _authService.RegisterAsync(command);
        }
    }
}