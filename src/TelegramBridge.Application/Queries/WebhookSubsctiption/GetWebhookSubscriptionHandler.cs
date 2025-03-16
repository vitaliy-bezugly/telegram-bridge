using MediatR;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Queries.WebhookSubsctiption;

public class GetWebhookSubscriptionHandler(IRepository<WebhookSubscriptionEntity> repository) : IRequestHandler<GetWebhookSubscriptionRequest, WebhookSubscriptionEntity>
{
    public Task<WebhookSubscriptionEntity> Handle(GetWebhookSubscriptionRequest request, CancellationToken cancellationToken)
    {
        return repository.GetByIdAsync(request.Id, cancellationToken);
    }
}