

using Microsoft.AspNetCore.Mvc;
using NetXUnit.Webapi.Models;
using NetXUnit.Webapi.Services;
using NetXUnit.Webapi.Services.Implementations;

namespace NetXUnit.Webapi;

public class Program
{
    static readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        builder.Services.AddSingleton<JwtTokenManager>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IUserService, UserService>();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        var apiGroup= app.MapGroup("/api");
        apiGroup.MapGet("/users", () =>
        {
            return;
        });
        apiGroup.MapPost("/users/user", ([FromServices]IUserService service) =>
        {
            return;
        });
        apiGroup.MapPost("/account/login", async ([FromServices]IAuthService service, [FromBody]LoginModel model) =>
        {
            var response= (await service.LoginAsync(model.Email, model.Password))
             .Then(token => new UserLoggedIn() { Token = token })
             .Else(errorMessage => new UserLoggedIn() { ErrorMessage = string.Join("\n", errorMessage.Select(e=> $"{e.Code}:{e.Description}")) })
             .Value;
        })
            .Produces<UserLoggedIn>();

        apiGroup.MapPost("/account/token/refresh", () =>
        {
            return;
        });
        apiGroup.MapPost("/account/logout", () =>
        {
            return;
        });

        app.Run();

    }
    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
