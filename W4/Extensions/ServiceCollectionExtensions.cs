using Microsoft.Extensions.DependencyInjection;
using W4.Repository.Interfaces;
using W4.Repository.Implementations;
using W4.Service.Interfaces;
using W4.Service.Implementations;

namespace W4.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IScoreService, ScoreService>();
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
