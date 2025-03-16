using System;

namespace TelegramBridge.Domain.Entities;

public record WebhookSubscriptionEntity : BaseEntity
{
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
}
