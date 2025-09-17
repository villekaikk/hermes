using Hermes.Domain.Models;

namespace Hermes.Application.Interfaces;

public interface IQueryParamChannel
{
    public delegate void QueryParamsUpdatedEvent(List<Parameter> queryParams);
    public event QueryParamsUpdatedEvent QueryParamsUpdated;
    public event QueryParamsUpdatedEvent QueryStringUpdated;

    public void NotifyQueryParamsUpdated(List<Parameter> queryParams);
    public void NotifyQueryStringUpdated(List<Parameter> queryParams);
}