using MediatR;

namespace TelegramBridge.Application.Commands.DeleteWebhookSubsctiption;

public record DeleteWebhookSubscriptionRequest(Guid Id) : IRequest;