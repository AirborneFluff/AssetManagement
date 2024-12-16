using Microsoft.EntityFrameworkCore;

namespace API.Domain.Shared.Helpers;

public class PagedList<T>
{
    public List<T> Items { get; private set; }
    public int TotalCount { get; private set; }
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    
    public PagedList(List<T> items, int count, int currentPage, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = currentPage;
    }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        BasePaginationParams pageParams)
    {
        return await CreateAsync(source, pageParams.PageNumber, pageParams.PageSize);
    }
    
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be greater than 0", nameof(pageNumber));
        if (pageSize < 1)
            throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}