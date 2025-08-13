using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Pastella.Backend.Infrastructure.ExternalServices;
using Pastella.Backend.Infrastructure.Repositories;
using System.Text;

namespace Pastella.Backend.WebAPI.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>()
                    .AddScoped<IDesignService, DesignService>()
                    .AddScoped<ICakeService, CakeService>()
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IImageService, ImageService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IDecorationService, DecorationService>()
                    .AddScoped<IBakeryService, BakeryService>()
                    .AddScoped<INotificationService, NotificationService>()
                    .AddScoped<ICommentService, CommentService>()
                    .AddScoped<IOccasionService, OccasionService>()
                    .AddScoped<ICustomCakeService, CustomCakeService>()
                    .AddScoped<IDeliveryService, DeliveryService>()
                    .AddScoped<IFCMService, FCMService>(); // FCM Service eklendi
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? 
                        throw new InvalidOperationException("JWT Key not configured")))
                });
            return services;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers()
                    .Services.AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .AddCors(options => options.AddPolicy("AllowAll", policy => 
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IDesignRepository, DesignRepository>()
                    .AddScoped<ICakeRepository, CakeRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>()
                    .AddScoped<IDesignImageRepository, DesignImageRepository>()
                    .AddScoped<IRepository<Decoration>, DecorationRepository>()
                    .AddScoped<IRepository<Bakery>, BakeryRepository>()
                    .AddScoped<IRepository<Notification>, NotificationRepository>()
                    .AddScoped<IRepository<Comment>, CommentRepository>()
                    .AddScoped<IRepository<Occasion>, OccasionRepository>()
                    .AddScoped<IRepository<DeliveryAddress>, DeliveryAddressRepository>()
                    .AddScoped<IRepository<DeviceToken>, DeviceTokenRepository>(); // DeviceToken repository eklendi
            return services;
        }
    }
}