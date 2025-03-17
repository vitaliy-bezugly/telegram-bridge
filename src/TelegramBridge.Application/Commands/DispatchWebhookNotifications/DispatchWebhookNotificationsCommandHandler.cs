using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Application.Common.Models;
using TelegramBridge.Application.Common.Services;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Commands.DispatchWebhookNotifications;

public class DispatchWebhookNotificationsCommandHandler(
    IRepository<WebhookSubscriptionEntity> repository, 
    IDispatcherService webhookDispatcherService,
    ILogger<DispatchWebhookNotificationsCommandHandler> logger) : IRequestHandler<DispatchWebhookNotificationsCommand>
{
    public async Task Handle(DispatchWebhookNotificationsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Body, nameof(request.Body));

        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync(cancellationToken);
                
        TelegramUpdate? update = null;
        try
        {
            update = JsonSerializer.Deserialize<TelegramUpdate>(body);
            if (update == null)
            {
                logger.LogError($"Failed to deserialize Telegram update from body: {body}");
                return;
            }
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, $"Error deserializing Telegram update from body: {body}");
            return;
        }

        var webhookSubscriptions = await repository.GetAllAsync(cancellationToken);
        if (webhookSubscriptions == null || !webhookSubscriptions.Any())
        {
            return;
        }
                
        var dispatchTasks = webhookSubscriptions
            .Select(subscription => DispatchToWebhookAsync(subscription.Url, update, cancellationToken))
            .ToArray();
        
        await Task.WhenAll(dispatchTasks);
    }
    
    private async Task DispatchToWebhookAsync(string webhookUrl, TelegramUpdate update, CancellationToken cancellationToken)
    {
        try
        {
            await webhookDispatcherService.DispatchWebhookNotificationAsync(webhookUrl, update, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error dispatching notification to webhook: {webhookUrl}");
        }
    }
}