using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Application.Validators;
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

            return services;
        }

        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateArtistCommand).Assembly));

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateArtistCommandValidator>();

            return services;
        }
    }
}
