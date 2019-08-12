using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CSharpFunctionalExtensions;
using OccBooking.Common.Hanlders;
using OccBooking.Common.Types;

namespace OccBooking.Common.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private IComponentContext _context;
        public Dispatcher(IComponentContext context)
        {
            _context = context;
        }
        //public async Task<Result> DispatchAsync<T>(T command)
        //{
        //    var handlerType = typeof(ICommandHandler<>)
        //        .MakeGenericType(command.GetType());

        //    dynamic handler = _context.Resolve(handlerType);

        //    return await handler.HandleAsync((dynamic) command);
        //}

        public async Task<Result> DispatchAsync(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

            dynamic handler = _context.Resolve(handlerType);

            return await handler.HandleAsync((dynamic)command);
        }

        //async Task<Result<T>> IQueryDispatcher.DispatchAsync<T>(T query)
        //{
        //    var handlerType = typeof(IQueryHandler<,>)
        //        .MakeGenericType(query.GetType(), typeof(T));

        //    dynamic handler = _context.Resolve(handlerType);

        //    return await handler.HandleAsync((dynamic) query);
        //}

        public async Task<Result<T>> DispatchAsync<T>(IQuery<T> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(T));

            dynamic handler = _context.Resolve(handlerType);

            return await handler.HandleAsync((dynamic)query);
        }
    }
}
