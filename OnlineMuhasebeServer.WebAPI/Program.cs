
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
                #region Swagger icin Authorization butonu ve altyapýsý bu sekilde kurulur.
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
             Bu metod, ASP.NET Core'a baþka bir assembly'de bulunan controller'larý da yüklemesini söyler.
            Yani: Bu sýnýf hangi projede (DLL'de) tanýmlýysa, ASP.NET Core o DLL'deki controller'larý da tanýmaya baþlar.
            Bu kod, ASP.NET Core uygulamasýnda controller'larý baþka bir assembly (DLL) içinden yüklemek için kullanýlýr.
            Biz bu controler larý web api den alýp presentation a verecez. API projesi tammamen bir konfigurasyon projesi olur.*/

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