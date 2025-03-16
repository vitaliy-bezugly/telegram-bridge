using MediatR;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Queries.WebhookSubsctiption;

public record GetWebhookSubscriptionRequest(Guid Id) : IRequest<WebhookSubscriptionEntity>;