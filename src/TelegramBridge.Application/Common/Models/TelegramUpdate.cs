using System.Text.Json.Serialization;

namespace TelegramBridge.Application.Common.Models;

public class TelegramUpdate
{
    [JsonPropertyName("update_id")]
    public long UpdateId { get; set; }
    
    [JsonPropertyName("message")]
    public TelegramMessage? Message { get; set; }
}

public class TelegramMessage
{
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
    
    [JsonPropertyName("from")]
    public TelegramUser? From { get; set; }
    
    [JsonPropertyName("chat")]
    public TelegramChat? Chat { get; set; }
    
    [JsonPropertyName("date")]
    public long Date { get; set; }
    
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    
    [JsonPropertyName("entities")]
    public List<TelegramMessageEntity>? Entities { get; set; }
}

public class TelegramUser
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }
    
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }
}

public class TelegramChat
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public class TelegramMessageEntity
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    
    [JsonPropertyName("length")]
    public int Length { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
