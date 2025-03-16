using FluentValidation;

namespace TelegramBridge.Application.Commands.AddWebhookSubscription;

public class AddWebhookSubscriptionValidator : AbstractValidator<AddWebhookSubscriptionRequest>
{
    public AddWebhookSubscriptionValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL cannot be empty.")
            .MaximumLength(200).WithMessage("URL cannot exceed 200 characters.")
            .MinimumLength(5).WithMessage("URL must be at least 5 characters.")
            .Matches(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format.");
        
        RuleFor(x => x.Event)
            .NotEmpty().WithMessage("Event cannot be empty.")
            .Matches(@"^(message_received)$").WithMessage("Event must be 'message_received'.");
        
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Name));
    }
}
