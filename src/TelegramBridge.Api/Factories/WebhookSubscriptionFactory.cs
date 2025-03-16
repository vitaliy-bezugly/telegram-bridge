using AutoMapper;
using TelegramBridge.Api.Models.Responses;
using TelegramBridge.Application.Common.Models;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Api.Factories;

public class WebhookSubscriptionFactory(IMapper mapper) : IMappingModelsFactory<WebhookSubscriptionEntity, WebhookResponse>
{
    public WebhookResponse ToModel(WebhookSubscriptionEntity source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return mapper.Map<WebhookResponse>(source);
    }

    public PaginatedList<WebhookResponse> ToPaginationListModel(PaginatedList<WebhookSubscriptionEntity> paginatedList)
    {
        ArgumentNullException.ThrowIfNull(paginatedList, nameof(paginatedList));
        var mappedItems = mapper.Map<List<WebhookResponse>>(paginatedList.Items);
        return new PaginatedList<WebhookResponse>(mappedItems, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
    }
}
