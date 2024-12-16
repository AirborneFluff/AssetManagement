using API.Domain.Shared.Helpers;
using AutoMapper;

namespace API.Domain.Shared.Mappings;

public class PagedListConverter<TSource, TDestination>(IMapper mapper)
    : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
{
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = mapper.Map<List<TDestination>>(source.Items);

        return new PagedList<TDestination>(
            mappedItems,
            source.TotalCount,
            source.CurrentPage,
            source.PageSize
        );
    }
}