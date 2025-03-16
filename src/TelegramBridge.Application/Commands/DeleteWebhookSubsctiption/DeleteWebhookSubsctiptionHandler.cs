using System;
using MediatR;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Commands.DeleteWebhookSubsctiption;

public class DeleteWebhookSubsctiptionHandler(IRepository<WebhookSubscriptionEntity> repository) : IRequestHandler<DeleteWebhookSubscriptionRequest>
{
    public async Task Handle(DeleteWebhookSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var webhookSubscription = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (webhookSubscription == null)
        {
            throw new ArgumentException($"Webhook subscription with id {request.Id} not found.");
        }

        await repository.DeleteAsync(request.Id, cancellationToken);
    }
}