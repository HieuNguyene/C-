
using ProductManagment.Repository;
using ProductManagment.Service;
using ProductManagment.Middleware;
using ProductManagment.Logging;
using FluentValidation.AspNetCore;
using FluentValidation;
using ProductManagment.Validation;
namespace ProductManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductSearchRequestValidator>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductRepository, MockProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
