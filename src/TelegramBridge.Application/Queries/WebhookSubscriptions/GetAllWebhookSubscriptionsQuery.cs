using MediatR;
using TelegramBridge.Application.Common.Models;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Application.Queries.WebhookSubscriptions;

public record GetAllWebhookSubscriptionsQuery : IRequest<PaginatedList<WebhookSubscriptionEntity>>
{
    private const int MaxPageSize = 50;

    public GetAllWebhookSubscriptionsQuery(int pageNumber, int pageSize, string? attribute = null, string? order = null)
    {
        PageNumber = pageNumber;
        Attribute = attribute ?? string.Empty;
        Order = order ?? string.Empty;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
    
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string Attribute { get; init; }
    public string Order { get; init; }
}