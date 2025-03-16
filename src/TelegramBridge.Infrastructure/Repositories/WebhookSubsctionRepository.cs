using System;
using Microsoft.EntityFrameworkCore;
using TelegramBridge.Application.Common.Exceptions;
using TelegramBridge.Application.Common.Interfaces;
using TelegramBridge.Domain.Entities;
using TelegramBridge.Infrastructure.Persistence;

namespace TelegramBridge.Infrastructure.Repositories;

public class WebhookSubsctionRepository : IRepository<WebhookSubscriptionEntity>
{
    private readonly AppDbContext _context;

    public WebhookSubsctionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WebhookSubscriptionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.WebhookSubscriptions.ToListAsync(cancellationToken);
    }

    public async Task<WebhookSubscriptionEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.WebhookSubscriptions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException<WebhookSubscriptionEntity>(id);
        }

        return entity;
    }

    public IQueryable<WebhookSubscriptionEntity> GetQueryable(CancellationToken cancellationToken = default)
    {
        return _context.WebhookSubscriptions.AsQueryable();
    }

    public async Task<WebhookSubscriptionEntity> AddAsync(WebhookSubscriptionEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.WebhookSubscriptions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.WebhookSubscriptions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(WebhookSubscriptionEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.WebhookSubscriptions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
