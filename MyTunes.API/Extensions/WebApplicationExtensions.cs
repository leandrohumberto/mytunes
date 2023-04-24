using MyTunes.API.CustomExceptionMiddleware;

namespace MyTunes.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
