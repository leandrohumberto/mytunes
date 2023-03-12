using Microsoft.EntityFrameworkCore;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Core.Repositories;
using MyTunes.Infrastructure.Persistence;
using MyTunes.Infrastructure.Persistence.Repositories;

namespace MyTunes.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyTunesDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("MyTunesCs")));

            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateArtistCommand).Assembly));

            return services;
        }
    }
}
