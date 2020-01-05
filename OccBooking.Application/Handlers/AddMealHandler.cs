using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AddMealHandler : ICommandHandler<AddMealCommand>
    {
        private IMenuRepository _menuRepository;

        public AddMealHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<Result> HandleAsync(AddMealCommand command)
        {
            var menu = await _menuRepository.GetMenuAsync(command.MenuId);

            if (menu == null)
            {
                return Result.Fail("Menu with given id does not exist");
            }

            menu.AddMeal(new Meal(Guid.NewGuid(), command.Name, command.Description, command.Type,
                command.Ingredients));

            await _menuRepository.SaveAsync();

            return Result.Ok();
        }
    }
}