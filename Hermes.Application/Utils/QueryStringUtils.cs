using System.Web;
using Hermes.Domain.Models;

namespace Hermes.Application.Utils;

public static class QueryStringUtils
{
    public static bool TryParseQueryParams(this string queryString, out List<QueryParam> paramsList)
    {
        if (!queryString.Contains('='))
        {
            paramsList = [];
            return false;
        }
            
        try
        {
            var paramsCollection = HttpUtility.ParseQueryString(queryString);
            
            paramsList = paramsCollection.Cast<string>().Select(p => new QueryParam(p, paramsCollection[p]!)).ToList();
            return true;
        }
        catch (Exception)
        {
            paramsList = [];
            return false;
        }
    }

    public static string BuildQueryString(this List<QueryParam> paramsList)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var param in paramsList)
        {
            queryString[param.Key] = param.Value;
        }
        return queryString?.ToString() ?? string.Empty;
    }
}