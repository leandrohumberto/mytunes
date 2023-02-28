using MyTunes.Application.Services.Implementations;
using MyTunes.Application.Services.Interfaces;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MyTunesDbContext>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
