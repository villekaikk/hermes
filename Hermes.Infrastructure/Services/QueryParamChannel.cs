using Hermes.Application.Interfaces;
using Hermes.Domain.Models;

namespace Hermes.Infrastructure.Services;

public class QueryParamChannel : IQueryParamChannel
{
    public event IQueryParamChannel.QueryParamsUpdatedEvent? QueryParamsUpdated;
    public event IQueryParamChannel.QueryParamsUpdatedEvent? QueryStringUpdated;
    
    public void NotifyQueryParamsUpdated(List<Parameter> queryParams)
    {
        QueryParamsUpdated?.Invoke(queryParams);
    }

    public void NotifyQueryStringUpdated(List<Parameter> queryParams)
    {
        QueryStringUpdated?.Invoke(queryParams);
    }
}