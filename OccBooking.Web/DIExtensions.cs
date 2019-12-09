using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper.Configuration;
using OccBooking.Application.EventHandlers;
using OccBooking.Application.Handlers;
using OccBooking.Application.Services;
using OccBooking.Common.Dispatchers;
using OccBooking.Common.Hanlders;
using OccBooking.Common.Infrastructure;
using OccBooking.Common.Types;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Web
{
    public static class DiExtensions
    {
        public static void RegisterCommandHandlers(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(RegisterHandler));

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
            var assembly = Assembly.GetAssembly(typeof(RegisterHandler));

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

        private static bool IsAssignableToGenericInterface(this Type givenType, Type genericType)
        {
            return givenType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
        }

        public static void RegisterEventHandlers(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ReservationRequestRejectedEventHandler)))
                .AsClosedTypesOf(typeof(IEventHandler<>));
        }

        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>();
        }

        public static void RegisterDispatchers(this ContainerBuilder builder)
        {
            builder.RegisterType<CqrsDispatcher>().As<ICqrsDispatcher>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
        }

        public static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IRepository<>)))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }

        public static void RegisterAppSettings(this ContainerBuilder builder,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var emailConfigurationSectionSection = configuration.GetSection("Email");
            var appSettings = new AppSettings(emailConfigurationSectionSection["EmailAddress"],
                emailConfigurationSectionSection["EmailPassword"],
                emailConfigurationSectionSection["EmailName"],
                emailConfigurationSectionSection["SmtpHost"],
                int.Parse(emailConfigurationSectionSection["SmtpPort"]));
            builder.Register(c => appSettings).SingleInstance();
        }
    }
}