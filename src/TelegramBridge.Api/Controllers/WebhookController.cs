using MediatR;
using Microsoft.AspNetCore.Mvc;
using TelegramBridge.Api.Constants;
using TelegramBridge.Api.Factories;
using TelegramBridge.Api.Models.Queries;
using TelegramBridge.Api.Models.Requests;
using TelegramBridge.Api.Models.Responses;
using TelegramBridge.Application.Commands.AddWebhookSubscription;
using TelegramBridge.Application.Commands.DeleteWebhookSubsctiption;
using TelegramBridge.Application.Queries.WebhookSubscriptions;
using TelegramBridge.Application.Queries.WebhookSubsctiption;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Api.Controllers;

[ApiController]
public class WebhookController(IMediator mediator, IMappingModelsFactory<WebhookSubscriptionEntity, WebhookResponse> mappingModelsFactory) : ControllerBase
{
    [HttpGet, Route(RouteConstants.Webhook.GetAll)]
    public async Task<IActionResult> GetWebhooksAsync([FromQuery] PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = new GetAllWebhookSubscriptionsQuery(paginationQuery.PageNumber, paginationQuery.PageSize);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(mappingModelsFactory.ToPaginationListModel(result));
    }

    [HttpGet, Route(RouteConstants.Webhook.GetById)]
    public async Task<IActionResult> GetWebhookById([FromRoute] Guid id)
    {
        var query = new GetWebhookSubscriptionRequest(id);
        var result = await mediator.Send(query, CancellationToken.None);
        var response = mappingModelsFactory.ToModel(result);
        return Ok(response);
    }

    [HttpPost, Route("/webhooks")]
    public async Task<IActionResult> CreateWebhook([FromBody] CreateWebhookRequest webhookData, CancellationToken cancellationToken)
    {
        var command = new AddWebhookSubscriptionRequest(webhookData.Url, webhookData.Event, webhookData.Name);
        var result = await mediator.Send(command, cancellationToken);
        var response = mappingModelsFactory.ToModel(result);
        return CreatedAtAction(nameof(GetWebhookById), new { id = result.Id }, response);
    }

    [HttpDelete, Route("/webhooks/{id}")]
    public async Task<IActionResult> DeleteWebhook([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteWebhookSubscriptionRequest(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
