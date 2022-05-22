using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Exceptions;

namespace TypicalSchoolWebsite_API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await next.Invoke(context);
            }
            catch (BadRequestException exception)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad request");
            }
            catch (UnauthorizedAccessException exception)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized action");
            }
            catch (NotFoundException exception)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Resource not found");
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unknown error");
            }
        }
    }
}
