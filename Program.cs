using AtonWebApi.DAL.Repository.Interfaces;
using AtonWebApi.DAL.Repository.Realization;
using AtonWebApi.Helpers.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

namespace AtonWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AtonWebApi", Version = "v1" });
                opt.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Basic HTTP Auth",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Basic"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Basic"
                            }
                        },
                        new string[] { }
                    }
                });
            });

                
                



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}