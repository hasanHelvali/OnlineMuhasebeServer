
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using OnlineMuhasebeServer.Presentation;
using System.Security.Cryptography.Xml;

namespace OnlineMuhasebeServer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                #region Swagger icin Authorization butonu ve altyap�s� bu sekilde kurulur.
                setup =>
            {
                var jwtSecuritySheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",



                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {jwtSecuritySheme, Array.Empty<string>() }
                });
            }
                #endregion
            );

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(AssemblyReference).Assembly);
            /*
             Bu metod, ASP.NET Core'a ba�ka bir assembly'de bulunan controller'lar� da y�klemesini s�yler.
            Yani: Bu s�n�f hangi projede (DLL'de) tan�ml�ysa, ASP.NET Core o DLL'deki controller'lar� da tan�maya ba�lar.
            Bu kod, ASP.NET Core uygulamas�nda controller'lar� ba�ka bir assembly (DLL) i�inden y�klemek i�in kullan�l�r.
            Biz bu controler lar� web api den al�p presentation a verecez. API projesi tammamen bir konfigurasyon projesi olur.*/

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}