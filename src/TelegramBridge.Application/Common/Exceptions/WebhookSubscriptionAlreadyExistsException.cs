namespace TelegramBridge.Application.Common.Exceptions;

public class WebhookSubscriptionAlreadyExistsException : Exception
{
    public WebhookSubscriptionAlreadyExistsException(string url) 
        : base($"Webhook subscription with URL '{url}' already exists.")
    {
        Url = url;
    }
    
    public WebhookSubscriptionAlreadyExistsException(string url, Exception innerException) 
        : base($"Webhook subscription with URL '{url}' already exists.", innerException)
    {
        Url = url;
    }
    
    public string Url { get; init; }
}
