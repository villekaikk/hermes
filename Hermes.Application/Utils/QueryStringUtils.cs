using System.Web;
using Hermes.Domain.Models;

namespace Hermes.Application.Utils;

public static class QueryStringUtils
{
    public static List<QueryParam> ParseQueryParams(this string queryString)
    {
        List<QueryParam> paramsList = [];

        try
        {
            var paramsCollection = HttpUtility.ParseQueryString(queryString);
            paramsList = paramsCollection.Cast<string>()
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => new QueryParam(p, paramsCollection[p] ?? string.Empty))
                .ToList();
        }
        catch (Exception)
        {
            // ignored
        }

        return paramsList;
    }

    public static string ToQueryString(this IEnumerable<QueryParam> queryParams) 
        => string.Join("&", queryParams.Where(p => !string.IsNullOrWhiteSpace(p.Key))
            .Select(p => $"{p.Key}={p.Value}")
            .ToList());
}