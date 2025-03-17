namespace TelegramBridge.Application.Common.Services;

public interface IWebhookValidationService
{
    Task ValidateWebhookUrlAsync(string url, CancellationToken cancellationToken = default);
}
