namespace TelegramBridge.Api.Constants;

public static class RouteConstants
{
    private const string Version = "v1";
    private const string BasePath = $"/api/{Version}";

    public static class Webhook
    {
        public const string GetAll = $"{BasePath}/webhooks";
        public const string GetById = $"{BasePath}/webhooks/{{id:guid}}";
    }

    public static class TelegramUpdate
    {
        public const string ReceiveUpdate = $"{BasePath}/telegram/update";
    }

    public static class Ping
    {
        public const string Get = $"{BasePath}/ping";
    }
}
