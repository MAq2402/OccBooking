using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using OccBooking.Application.Handlers;
using OccBooking.Common.Hanlders;
using OccBooking.Common.Types;

namespace OccBooking.Web.Extensions
{
    public static class DIExtensions
    {
        public static void RegisterCommandHandlers(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(TestHandler));

            var commandType = typeof(ICommand);
            var commands = assembly.GetTypes()
                .Where(p => commandType.IsAssignableFrom(p));

            foreach (var command in commands)
            {
                var abstractHandlerType = typeof(ICommandHandler<>)
                    .MakeGenericType(command);

                var concreteHandlerType =
                    assembly.GetTypes().FirstOrDefault(x => abstractHandlerType.IsAssignableFrom(x));

                builder.RegisterType(concreteHandlerType).As(abstractHandlerType);
            }
        }

        public static void RegisterQueryHandlers(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(TestHandler));

            var queryType = typeof(IQuery<>);
            var queries = assembly.GetTypes()
                .Where(p => p.IsAssignableToGenericInterface(queryType));

            foreach (var query in queries)
            {
                var queryResultType = query.GetInterfaces().Single().GenericTypeArguments.Single();
                var abstractHandlerType = typeof(IQueryHandler<,>)
                    .MakeGenericType(query, queryResultType);

                var concreteHandlerType =
                    assembly.GetTypes().FirstOrDefault(x => abstractHandlerType.IsAssignableFrom(x));

                builder.RegisterType(concreteHandlerType).As(abstractHandlerType);
            }
        }

        public static bool IsAssignableToGenericInterface(this Type givenType, Type genericType)
        {
            return givenType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
        }
    }
}