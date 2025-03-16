using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TelegramBridge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Domain.Entities;
using TelegramBridge.Infrastructure.Repositories;

namespace TelegramBridge.Infrastructure;

public static class ServiceConfigurator
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = Environment.GetEnvironmentVariable("DB_CONN");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is required to configure the database.");
            }
        }

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IRepository<WebhookSubscriptionEntity>, WebhookSubsctionRepository>();
        
        return services;
    }
}
