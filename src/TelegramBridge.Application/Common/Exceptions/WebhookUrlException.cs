namespace TelegramBridge.Application.Common.Exceptions;

public class WebhookUrlException : Exception
{
    public WebhookUrlException(string url)
        : base($"Webhook URL '{url}' is not valid. It should return 200 OK on a POST request with a JSON body.")
    {
        Url = url;
    }

    public WebhookUrlException(string url, Exception innerException)
        : base($"Webhook URL '{url}' is not valid. It should return 200 OK on a POST request with a JSON body.", innerException)
    {
        Url = url;
    }

    public string Url { get; init; }
}
