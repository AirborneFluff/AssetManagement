using System.Text.Json;
using API.Domain.Shared.Helpers;

namespace API.Extensions;

public static class HttpResponseExtensions
{
    public static void AddPaginationHeaders<T>(this HttpResponse response, PagedList<T> list)
    {
        var pageHeader = new
        {
            TotalCount = list.TotalCount,
            PageSize = list.PageSize,
            CurrentPage = list.CurrentPage,
            TotalPages = list.TotalPages,
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageHeader, options));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}