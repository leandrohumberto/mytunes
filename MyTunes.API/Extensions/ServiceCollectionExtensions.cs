using Microsoft.EntityFrameworkCore;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyTunesDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("MyTunesCs")));
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateArtistCommand).Assembly));

            return services;
        }
    }
}
