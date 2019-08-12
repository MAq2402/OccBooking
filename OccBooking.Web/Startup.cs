using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OccBooking.Application.Handlers;
using OccBooking.Common.Dispatchers;
using OccBooking.Common.Hanlders;
using OccBooking.Common.Types;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer Container { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<OccBookingDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Connection")));

            var builder = new ContainerBuilder();

            builder.Populate(services);

            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(TestHandler)))
            //    .Where(x => x.IsAssignableTo<ICommandHandler<ICommand>>())
            //    .AsImplementedInterfaces();

            //builder.Register(c =>
            //{
            //    var ctx = c.Resolve<IComponentContext>();

            //    return t =>
            //    {
            //        var handlerType = typeof(ICommandHandler<>).MakeGenericType(t);
            //        return (ICommand)ctx.Resolve(handlerType);
            //    };
            //});
            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

            var assembly = Assembly.GetAssembly(typeof(TestHandler));

           // Assembly.Load(AssemblyName.GetAssemblyName("OccBooking.Application"))

            //foreach (var assembly in assemblies)
            //{
                // builder.RegisterAssemblyTypes(assembly).AssignableTo<IQueryResult>().AsImplementedInterfaces();

                // builder.RegisterAssemblyTypes(assembly).As(typeof(ICommand)).AsImplementedInterfaces();
            // var commands = assembly.GetTypes().Where(x => x == typeof(ICommand));

            var type = typeof(ICommand);
            var commands = assembly.GetTypes()
                .Where(p => type.IsAssignableFrom(p));

            foreach (var command in commands)
            {
                var handlerType = typeof(ICommandHandler<>)
                    .MakeGenericType(command);
                //MethodInfo method = typeof(Type).GetMethod("IsAssignableTo",
                //        BindingFlags.Public | BindingFlags.Static);

                //method = method.MakeGenericMethod(handlerType);
                var concreteHandler = assembly.GetTypes().Where(x => handlerType.IsAssignableFrom(x)).FirstOrDefault();
                builder.RegisterType(concreteHandler).As(handlerType);

                //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(TestHandler)))
                //    .Where(x => (bool)method.Invoke(null, new object[]{x}))
                //    .AsImplementedInterfaces();
                // builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>).MakeGenericType(command)).AsImplementedInterfaces();
            }
           

            //}
            //var genericRequestHandlers = typeof(TestHandler).Assembly
            //    .ExportedTypes
            //    .Where(x => IsGenericRequestHandler(x))
            //    .ToArray();

            //foreach (var genericRequestHandler in genericRequestHandlers)
            //{
            //    builder
            //        .RegisterGeneric(genericRequestHandler)
            //        .AsImplementedInterfaces();
            //}

            //builder.Register<Func<Type, IHandleCommand>>(c =>
            //{
            //    var ctx = c.Resolve<IComponentContext>();

            //    return t =>
            //    {
            //        var handlerType = typeof(IHandleCommand<>).MakeGenericType(t);
            //        return (IHandleCommand)ctx.Resolve(handlerType);
            //    };
            //});

            builder.RegisterType<Dispatcher>()
                .AsSelf();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }
        //private static bool IsGenericRequestHandler(Type t)
        //{
        //    return
        //        t.IsGenericTypeDefinition &&
        //        t.GetInterfaces().Any(i =>
        //        {
        //            return
        //                i.IsGenericType &&
        //                i.GetGenericTypeDefinition() == typeof(ICommandHandler<>);
        //        });
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
