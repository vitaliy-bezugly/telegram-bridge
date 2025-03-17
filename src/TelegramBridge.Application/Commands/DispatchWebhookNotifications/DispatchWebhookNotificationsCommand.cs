using MediatR;

namespace TelegramBridge.Application.Commands.DispatchWebhookNotifications;

public record DispatchWebhookNotificationsCommand(Stream Body) : IRequest;
