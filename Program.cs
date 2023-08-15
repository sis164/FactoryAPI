using FactoryAPI.Models;
using FactoryAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FactoryAPI.Utilities;
using Microsoft.IdentityModel.Tokens;

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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // ��������, ����� �� �������������� �������� ��� ��������� ������(SSL)
                ValidateIssuer = true,
                // ������, �������������� ��������
                ValidIssuer = AuthOptions.ISSUER,

                // ����� �� �������������� ����������� ������
                ValidateAudience = true,
                // ��������� ����������� ������
                ValidAudience = AuthOptions.AUDIENCE,
                // ����� �� �������������� ����� �������������
                ValidateLifetime = true,

                // ��������� ����� ������������
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                // ��������� ����� ������������
                ValidateIssuerSigningKey = true,
            };
        });

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
