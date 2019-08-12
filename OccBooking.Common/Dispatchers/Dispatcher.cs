using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CSharpFunctionalExtensions;
using OccBooking.Common.Hanlders;

namespace OccBooking.Common.Dispatchers
{
    public class Dispatcher : ICommandDispatcher, IQueryDispatcher
    {
        private IComponentContext _context;
        public Dispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task<Result> DispatchAsync<T>(T command)
        {
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

            dynamic handler = _context.Resolve(handlerType);

            return await handler.HandleAsync((dynamic) command);
        }

        Task<Result<T>> IQueryDispatcher.DispatchAsync<T>(T query)
        {
            throw new NotImplementedException();
        }
    }
}
