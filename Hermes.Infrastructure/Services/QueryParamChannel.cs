using Hermes.Application.Interfaces;
using Hermes.Domain.Models;

namespace Hermes.Infrastructure.Services;

public class QueryParamChannel : IQueryParamChannel
{
    public event IQueryParamChannel.QueryParamsUpdatedEvent? QueryParamsUpdated;
    public event IQueryParamChannel.QueryParamsUpdatedEvent? QueryStringUpdated;
    
    public void NotifyQueryParamsUpdated(List<QueryParam> queryParams)
    {
        QueryParamsUpdated?.Invoke(queryParams);
    }

    public void NotifyQueryStringUpdated(List<QueryParam> queryParams)
    {
        QueryStringUpdated?.Invoke(queryParams);
    }
}