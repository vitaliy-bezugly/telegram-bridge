namespace TelegramBridge.Application.Common.Services;

public interface IDispatcherService
{
    Task DispatchWebhookNotificationAsync<T>(string url, T body, CancellationToken cancellationToken) where T : class;
}
