using System;

namespace TelegramBridge.Application.Common.Exceptions;

public class NotFoundException<T> : Exception
{
    public NotFoundException(Guid id) 
        : base($"{typeof(T).FullName} with {id} was not found.")
    {
        Id = id;
    }
    
    public NotFoundException(Guid id, Exception innerException) 
        : base($"{typeof(T).FullName} with {id} was not found.", innerException)
    {
        Id = id;
    }
    
    public Guid Id { get; init; }
    
    public Type EntityType => typeof(T);
}