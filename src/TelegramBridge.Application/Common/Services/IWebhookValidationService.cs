namespace TelegramBridge.Application.Common.Services;

/// <summary>
/// Service for validating webhook endpoints
/// </summary>
public interface IWebhookValidationService
{
    /// <summary>
    /// Validates if a webhook URL is reachable and accepts requests
    /// </summary>
    /// <param name="url">The webhook URL to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="WebhookUrlException">Thrown when validation fails</exception>
    Task ValidateWebhookUrlAsync(string url, CancellationToken cancellationToken = default);
}
