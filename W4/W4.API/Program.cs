using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using W4.API.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;



using W4.API.Middlewares;

using W4.Infrastructure.Repositories.Implementations;
using W4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using W4.Infrastructure.Services;
using W4.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
namespace W4.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context,configuration) => configuration
            // Mức độ log tối thiểu: ghi từ mức Information trở lên
            .MinimumLevel.Information()
            // Giảm bớt log rác từ nội bộ Microsoft (Chỉ từ Warning trở lên đối với Microsoft)
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
            
            .Enrich.FromLogContext() // Bắt buộc để nhận diện ngữ cảnh request
            .Enrich.WithProperty("Application", "W4.API")
            .Enrich.WithProperty("Environment",context.HostingEnvironment.EnvironmentName)
            // Ghi log ra màn hình console
            .WriteTo.Console()

            // Ghi ra file tự động xoay tròn theo ngày (Rolling file)
            .WriteTo.File(
                path: "log/app-.txt",
                rollingInterval: RollingInterval.Day, // Mỗi ngày từ động tạo 1 file mới
                retainedFileCountLimit: 30, // Tự động giữ file lại 30 ngày gần nhất
                outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{Application}] [{Environment}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}"
                        )
            );

            // Add services to the container.
            builder.Services.AddControllers();
            // Cấu hình JWT
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
            builder.Services.AddAuthentication(options =>
            {
                // Báo cho Server: "Mặc định hãy tìm và kiểm tra thẻ JWT Bearer"
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Khi có API đến tìm thẻ JWT 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;// Nếu ko có hay hết hạn thì đẩy lỗi 401 
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),// Tạo một khóa đối xứng rồi truyền key đã chuyển sang dạng byte

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer, // Kiểm tra xem có đúng nơi phát hành không

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience, // Kiểm tra xem có đúng người sử dụng không

                    // Kiểm tra hết hạn dùng: Quá 10p từ chối lập tức 
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero  //Hết hạn đúng từng giây, không cho trễ   
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("CanManageStudents", policy =>
                    policy.RequireRole("Admin", "Teacher"));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập Access Token của bạn vào đây (Swagger sẽ tự thêm tiền tố Bearer)"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddMemoryCache();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClassValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<StudentQueryValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Đăng ký IDbConnection cho Dapper
            builder.Services.AddScoped<System.Data.IDbConnection>(sp =>
                new Microsoft.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "w4:";
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            // app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}










