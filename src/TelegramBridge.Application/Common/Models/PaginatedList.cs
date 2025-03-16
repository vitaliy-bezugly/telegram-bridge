using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TelegramBridge.Application.Common.Models;

public class PaginatedList<T>
{
    private readonly List<T> _items;

    public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;

        _items = items;
    }

    public IReadOnlyList<T> Items => _items;

    public int CurrentPage { get; init; }

    public int TotalPages { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize, CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }

    public static async Task<PaginatedList<T>> CreateAsync<TKey>(
        IQueryable<T> source,
        int currentPage,
        int pageSize,
        Expression<Func<T, TKey>> orderBy,
        bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);

        var orderedSource = ascending
            ? source.OrderBy(orderBy)
            : source.OrderByDescending(orderBy);

        var items = await orderedSource
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }
}