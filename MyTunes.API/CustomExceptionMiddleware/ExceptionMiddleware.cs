using MyTunes.API.Models;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Services;
using System.Net;

namespace MyTunes.API.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILoggerService loggerService)
        {
            _next = requestDelegate;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (LoginFailException ex)
            {
                _loggerService.LogError($"Login failed for credentials {ex.Email} - {ex.Password}: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidUserEmailException ex)
            {
                _loggerService.LogError($"Invalid user email - {ex.Email}: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ArtistNotFoundException ex)
            {
                _loggerService.LogError($"Artist Id not found - {ex.Id}: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Something went wrong:{ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync
                (new ErrorDetails(context.Response.StatusCode, message).ToString()); 
        }
    }
}
