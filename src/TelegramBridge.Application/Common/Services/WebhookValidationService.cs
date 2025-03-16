using RestSharp;
using TelegramBridge.Application.Common.Exceptions;

namespace TelegramBridge.Application.Common.Services;

public class WebhookValidationService : IWebhookValidationService
{
    public async Task ValidateWebhookUrlAsync(string url, CancellationToken cancellationToken = default)
    {
        var restRequest = new RestRequest(string.Empty, Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(new { status = "Health check" });
        var client = new RestClient(url);

        var response = await client.ExecuteAsync(restRequest, cancellationToken);
        if (!response.IsSuccessful)
        {
            throw new WebhookUrlException(url);
        }
    }
}