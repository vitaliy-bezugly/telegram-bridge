using TelegramBridge.Application.Common.Models;

namespace TelegramBridge.Api.Factories;

public interface IMappingModelsFactory<TSource, TDesignation>
{ 
    PaginatedList<TDesignation> ToPaginationListModel(PaginatedList<TSource> paginatedList);
    TDesignation ToModel(TSource source);
}
