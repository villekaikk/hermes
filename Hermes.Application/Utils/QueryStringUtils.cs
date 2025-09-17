using System.Web;
using Hermes.Domain.Models;

namespace Hermes.Application.Utils;

public static class QueryStringUtils
{
    public static List<Parameter> ParseQueryParams(this string queryString)
    {
        List<Parameter> paramsList = [];

        try
        {
            var paramsCollection = HttpUtility.ParseQueryString(queryString);
            paramsList = paramsCollection.Cast<string>()
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => new Parameter(p, paramsCollection[p] ?? string.Empty))
                .ToList();
        }
        catch (Exception)
        {
            // ignored
        }

        return paramsList;
    }

    public static string ToQueryString(this IEnumerable<Parameter> queryParams) 
        => string.Join("&", queryParams.Where(p => !string.IsNullOrWhiteSpace(p.Key))
            .Select(p => $"{p.Key}={p.Value}")
            .ToList());
}