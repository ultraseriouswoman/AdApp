using AdApp.Application.Commands;
using AdApp.Application.Commands.Validators;
using AdApp.Application.Mappers;
using AdApp.Domain.Interfaces;
using AdApp.Infrastructure.Repositories;
using FluentValidation;

namespace AdApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(profile => profile.AddProfile<Mapper>());

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(UploadAdPlatformsCommand).Assembly));

            builder.Services.AddValidatorsFromAssembly(typeof(UploadAdPlatformsCommandValidator).Assembly);

            builder.Services.AddSingleton<IAdPlatformRepository, AdPlatformRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
