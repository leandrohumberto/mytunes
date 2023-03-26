using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Application.Validators;
using MyTunes.Core.Repositories;
using MyTunes.Core.Services;
using MyTunes.Infrastructure.Auth;
using MyTunes.Infrastructure.Persistence;
using MyTunes.Infrastructure.Persistence.Repositories;
using System.Text;

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
            services.AddScoped<IAuthService, AuthService>();

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

        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new Exception("Jwt configuration error"))),
                    };
                });

            return services;
        }
    }
}
