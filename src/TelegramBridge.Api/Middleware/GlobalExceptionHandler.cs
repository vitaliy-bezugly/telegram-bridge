using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using TelegramBridge.Application.Common.Exceptions;

namespace TelegramBridge.Api.ExceptionHandling;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
        _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException<>), HandleNotFoundException },
            { typeof(WebhookUrlException), HandleWebhookUrlException },
            { typeof(WebhookSubscriptionAlreadyExistsException), HandleWebhookSubscriptionAlreadyExistsException }
        };
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An exception occurred: {ExceptionMessage}", exception.Message);
        if (await TryExecuteHandler(httpContext, exception))
        {
            return true;
        }

        await HandleDefaultException(httpContext, exception);
        return true;
    }

    private async Task<bool> TryExecuteHandler(HttpContext context, Exception exception)
    {
        var exceptionType = exception.GetType();
        if (exceptionType.IsGenericType &&
            exceptionType.GetGenericTypeDefinition() == typeof(NotFoundException<>))
        {
            await HandleNotFoundException(context, exception);
            return true;
        }

        if (_exceptionHandlers.TryGetValue(exceptionType, out var handler))
        {
            await handler(context, exception);
            return true;
        }

        foreach (var (type, handlerFunc) in _exceptionHandlers)
        {
            if (type.IsAssignableFrom(exceptionType))
            {
                await handlerFunc(context, exception);
                return true;
            }
        }

        return false;
    }

    private record ErrorResponse(
        string Type,
        string Title,
        int Status,
        string? Detail = null);

    private async Task HandleValidationException(HttpContext context, Exception exception)
    {
        var validationException = (ValidationException)exception;
        var errors = validationException.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());

        await WriteJsonResponse(context, HttpStatusCode.BadRequest, new
        {
            type = "ValidationFailure",
            title = "Validation errors occurred",
            status = (int)HttpStatusCode.BadRequest,
            errors
        });
    }

    private async Task HandleNotFoundException(HttpContext context, Exception exception)
    {
        await WriteErrorResponse(
            context,
            HttpStatusCode.NotFound,
            "EntityNotFound",
            "The requested resource was not found",
            exception.Message);
    }

    private async Task HandleWebhookUrlException(HttpContext context, Exception exception)
    {
        var ex = (WebhookUrlException)exception;
        await WriteJsonResponse(context, HttpStatusCode.BadRequest, new
        {
            type = "WebhookUrlError",
            title = "Invalid webhook URL",
            status = (int)HttpStatusCode.BadRequest,
            detail = exception.Message,
            url = ex.Url
        });
    }

    private async Task HandleWebhookSubscriptionAlreadyExistsException(HttpContext context, Exception exception)
    {
        var ex = (WebhookSubscriptionAlreadyExistsException)exception;
        await WriteJsonResponse(context, HttpStatusCode.Conflict, new
        {
            type = "WebhookDuplicate",
            title = "Webhook subscription already exists",
            status = (int)HttpStatusCode.Conflict,
            detail = exception.Message,
            url = ex.Url
        });
    }

    private async Task HandleDefaultException(HttpContext context, Exception exception)
    {
        await WriteErrorResponse(
            context,
            HttpStatusCode.InternalServerError,
            "ServerError",
            "An unexpected error occurred",
            "An error occurred while processing your request. Please try again later.");
    }

    private async Task WriteErrorResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string type,
        string title,
        string detail)
    {
        await WriteJsonResponse(context, statusCode, new ErrorResponse(type, title, (int)statusCode, detail));
    }

    private static async Task WriteJsonResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        object response)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
