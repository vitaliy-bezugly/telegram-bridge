using TelegramBridge.Api.Mappings;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Api.Models.Responses;

public class WebhookResponse : IMapFrom<WebhookSubscriptionEntity>
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
