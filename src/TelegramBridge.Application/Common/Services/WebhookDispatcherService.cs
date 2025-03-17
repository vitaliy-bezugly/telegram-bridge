using RestSharp;

namespace TelegramBridge.Application.Common.Services;

public class DispatcherService : IDispatcherService
{
    public Task DispatchWebhookNotificationAsync<T>(string url, T body, CancellationToken cancellationToken) where T : class
    {
        var restRequest = new RestRequest(string.Empty, Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(body);
        var client = new RestClient(url);
        
        return client.ExecuteAsync(restRequest, cancellationToken);
    }

}
