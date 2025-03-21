using System;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TelegramBridge.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) when (ex is not OperationCanceledException && ex is not TaskCanceledException)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, $"Request: Unhandled Exception for Request {requestName} {request}");
            throw;
        }
    }
}
