﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using OccBooking.Domain.Exceptions;

namespace OccBooking.Web
{
    public static class ExceptionHandlingExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                    if (exception != null)
                    {
                        if (exception is DomainException)
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(exception.Message);
                        }
                        else
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync("Unexpected error occurred");
                        }
                    }
                });
            });
        }
    }
}