using MediatR;
using Microsoft.AspNetCore.Mvc;
using TelegramBridge.Api.Constants;
using TelegramBridge.Api.Factories;
using TelegramBridge.Api.Models.Queries;
using TelegramBridge.Api.Models.Responses;
using TelegramBridge.Application.Queries.WebhookSubscriptions;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Api.Controllers;

[ApiController]
public class WebhookController(IMediator mediator, IMappingModelsFactory<WebhookSubscriptionEntity, WebhookResponse> mappingModelsFactory) : ControllerBase
{
    [HttpGet, Route(RouteConstants.Webhook.GetAll)]
    public async Task<IActionResult> GetWebhooksAsync([FromQuery] PaginationQuery paginationQuery)
    {
        var query = new GetAllWebhookSubscriptionsQuery(paginationQuery.PageNumber, paginationQuery.PageSize);
        var result = await mediator.Send(query);
        return Ok(mappingModelsFactory.ToPaginationListModel(result));
    }

    [HttpGet, Route(RouteConstants.Webhook.GetById)]
    public IActionResult GetWebhookById(string id)
    {
        return Ok(new { message = $"Webhook details for {id}" });
    }

    // [HttpPost, Route("/webhooks")]
    // public IActionResult CreateWebhook([FromBody] object webhookData)
    // {
    //     return Created("/webhooks", new { message = "Webhook created", data = webhookData });
    // }

    // [HttpDelete, Route("/webhooks/{id}")]
    // public IActionResult DeleteWebhook(string id)
    // {
    //     return Ok(new { message = $"Webhook {id} deleted" });
    // }
}
