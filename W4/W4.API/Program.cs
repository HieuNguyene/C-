using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using W4.API.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Application.Interfaces;
using W4.Application.Implementations;
using W4.API.Middlewares;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Implementations;
using W4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace W4.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClassValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<StudentQueryValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}










