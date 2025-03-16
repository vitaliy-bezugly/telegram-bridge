using System;
using FluentValidation;

namespace TelegramBridge.Application.Queries.WebhookSubscriptions;

public class GetAllWebhookSubscriptionsQueryValidator : AbstractValidator<GetAllWebhookSubscriptionsQuery>
{
    public GetAllWebhookSubscriptionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(50);
        
        RuleFor(x => x.Attribute)
            .Must(x => string.IsNullOrEmpty(x) || x == "id" || x == "name" || x == "createdAt" || x == "updatedAt")
            .WithMessage("Attribute must be null or one of the following: id, name, createdAt, updatedAt.");
        
        RuleFor(x => x.Order)
            .Must(x => string.IsNullOrEmpty(x) || x == "asc" || x == "desc" || x == "ascending" || x == "descending")
            .WithMessage("Order must be null or one of the following: asc, desc, ascending, descending.");
    }
}
