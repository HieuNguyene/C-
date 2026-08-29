using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using W4.Application.Interfaces;
using W4.Infrastructure.Repositories.Implementations;



namespace W4.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {




            var applicationAssembly = typeof(W4.Application.Features.Students.Commands.CreateStudentCommand).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            return services;
        }
    }
}







