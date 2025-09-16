using Hermes.Domain.Models;

namespace Hermes.Application.Interfaces;

public interface IQueryParamChannel
{
    public delegate void QueryParamsUpdatedEvent(List<QueryParam> queryParams);
    public event QueryParamsUpdatedEvent QueryParamsUpdated;
    public event QueryParamsUpdatedEvent QueryStringUpdated;

    public void NotifyQueryParamsUpdated(List<QueryParam> queryParams);
    public void NotifyQueryStringUpdated(List<QueryParam> queryParams);
}