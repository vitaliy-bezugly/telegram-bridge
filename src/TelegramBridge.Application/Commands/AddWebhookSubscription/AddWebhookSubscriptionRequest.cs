using MediatR;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Commands.AddWebhookSubscription;

public record AddWebhookSubscriptionRequest(string url, string @event, string? name) : IRequest<WebhookSubscriptionEntity>
{
    public string Url { get; set; } = url;
    public string Event { get; set; } = @event;
    public string? Name { get; set; } = name;
}
