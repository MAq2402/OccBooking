using System;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OccBooking.Application.EventHandlers;
using OccBooking.Application.Mappings.Profiles;
using OccBooking.Application.Services;
using OccBooking.Common.Dispatchers;
using OccBooking.Common.Infrastructure;
using OccBooking.Common.Types;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Repositories;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddAutoMapper(typeof(UserProfile));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<OccBookingDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.ConfigureAuthentication(Configuration,
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Security")["Key"])));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "OccBooking", Version = "v1"}); });

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterAppSettings(Configuration);
            builder.RegisterRepositories();
            builder.RegisterCommandHandlers();
            builder.RegisterQueryHandlers();
            builder.RegisterEventHandlers();
            builder.RegisterDispatchers();
            builder.RegisterServices();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OccBookingDbContext dbContext)
        {
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            dbContext.Database.Migrate();

            app.ConfigureExceptionHandler(env);

            app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OccBooking"); });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}