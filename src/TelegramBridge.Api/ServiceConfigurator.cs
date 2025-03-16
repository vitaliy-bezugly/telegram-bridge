using System.Reflection;
using TelegramBridge.Api.ExceptionHandling;
using TelegramBridge.Api.Factories;
using TelegramBridge.Api.Models.Responses;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Api;

public static class ServiceConfigurator
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IMappingModelsFactory<WebhookSubscriptionEntity, WebhookResponse>, WebhookSubscriptionFactory>();
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        return services;
    }
}
