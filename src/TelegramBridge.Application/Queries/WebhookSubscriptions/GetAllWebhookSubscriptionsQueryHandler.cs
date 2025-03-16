using MediatR;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Application.Common.Models;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Queries.WebhookSubscriptions;

public class GetAllWebhookSubscriptionsQueryHandler(IRepository<WebhookSubscriptionEntity> repository) 
    : IRequestHandler<GetAllWebhookSubscriptionsQuery, PaginatedList<WebhookSubscriptionEntity>>
{
    public Task<PaginatedList<WebhookSubscriptionEntity>> Handle(GetAllWebhookSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var query = repository.GetQueryable(cancellationToken);
        return PaginatedList<WebhookSubscriptionEntity>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}