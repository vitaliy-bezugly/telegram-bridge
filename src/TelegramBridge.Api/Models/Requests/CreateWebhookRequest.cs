using System.ComponentModel.DataAnnotations;

namespace TelegramBridge.Api.Models.Requests;

public class CreateWebhookRequest 
{
    public string? Name { get; set; }

    [Required, MaxLength(200), MinLength(5), RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$",
        ErrorMessage = "Invalid URL format.")]
    public string Url { get; set; } = string.Empty;

    [Required, RegularExpression(@"^(message_received)$", 
        ErrorMessage = "Event must be 'message_received'.")]
    public string Event { get; set; } = string.Empty;
}