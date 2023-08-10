using FactoryAPI.Models;
using FactoryAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers(); // Register controllers

        builder.Services.AddDbContext<ApplicationContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Map controllers to endpoints
        });
        app.UseHttpsRedirection();

        app.Run();
    }
}
