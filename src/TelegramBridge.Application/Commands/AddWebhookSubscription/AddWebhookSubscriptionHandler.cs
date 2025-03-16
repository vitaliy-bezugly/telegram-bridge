using MediatR;
using Microsoft.EntityFrameworkCore;
using TelegramBridge.Application.Common.Exceptions;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Application.Common.Services;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Commands.AddWebhookSubscription;

public class AddWebhookSubscriptionHandler(IRepository<WebhookSubscriptionEntity> repository, IWebhookValidationService webhookValidationService)
    : IRequestHandler<AddWebhookSubscriptionRequest, WebhookSubscriptionEntity>
{
    public async Task<WebhookSubscriptionEntity> Handle(AddWebhookSubscriptionRequest request, CancellationToken cancellationToken)
    {
        if(IsWebhookSubscriptionExistsAsync(request.Url, cancellationToken).Result)
        {
            throw new WebhookSubscriptionAlreadyExistsException(request.Url);
        }

        await webhookValidationService.ValidateWebhookUrlAsync(request.Url, cancellationToken);
        var subscription = new WebhookSubscriptionEntity
        {
            Url = request.Url,
            Event = request.Event,
            Name = request.Name ?? string.Empty
        };

        return await repository.AddAsync(subscription, cancellationToken);
    }

    private async Task<bool> IsWebhookSubscriptionExistsAsync(string url, CancellationToken cancellationToken)
    {
        var queryable = repository.GetQueryable(cancellationToken);
        var exists = await queryable.AnyAsync(x => x.Url == url, cancellationToken);
        return exists;
    }
}
