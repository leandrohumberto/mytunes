using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MyTunes.API.Extensions;
using MyTunes.API.Filters;
using NLog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCqrs();
builder.Services.AddFluentValidation();
builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.ConfigureLoggerService();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddJsonOptions(x =>
    {
        // Serialize enums as strings in API responses
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTunes.API", Version = "v1" });
    c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("00:00:00") });

    // Add JWT security definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o schema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer",
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.ConfigureCustomExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));

    app.ConfigureCustomExceptionMiddleware();
}

app.UseHttpsRedirection();

// Manter UseAuthentication e UseAuthorization nessa ordem
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
