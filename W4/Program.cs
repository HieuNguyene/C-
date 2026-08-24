
using FluentValidation;
using FluentValidation.AspNetCore;
using W4.Repository.Interfaces;
using W4.Service.Interfaces;
using W4.Service.Implementations;
using W4.middleware;
using W4.FluentValidation;
using W4.Repository.Interfaces;
using W4.Service.Interfaces;
using W4.Repository.Implementations;
using W4.Context.Data;
using Microsoft.EntityFrameworkCore;
using W4.Service.Implementations;
namespace W3
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
            builder.Services.AddScoped<IClassRepository, ClassRepository>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IScoreRepository,ScoreRepository>();
            builder.Services.AddScoped<IScoreService,ScoreService>();
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


