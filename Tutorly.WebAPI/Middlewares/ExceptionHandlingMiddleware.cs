﻿
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.WebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

            }catch(NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }catch(OutOfSpaceException outOfSpace)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(outOfSpace.Message);

            }catch(WrongUserDataInputException dataInputEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(dataInputEx.Message);
            }
            catch(ForbidException forbid)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbid.Message);
            }            
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
