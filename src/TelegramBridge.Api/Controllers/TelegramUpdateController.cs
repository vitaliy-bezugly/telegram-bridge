using MediatR;
using Microsoft.AspNetCore.Mvc;
using TelegramBridge.Api.Constants;
using TelegramBridge.Application.Commands.DispatchWebhookNotifications;

namespace TelegramBridge.Api.Controllers;

[ApiController]
public class TelegramUpdateController(IMediator mediator) : ControllerBase
{
    [HttpPost, Route(RouteConstants.TelegramUpdate.ReceiveUpdate)]
    public async Task<IActionResult> ReceiveUpdate(CancellationToken cancellationToken)
    {
        var command = new DispatchWebhookNotificationsCommand(Request.Body);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
