using Hermes.Domain.Models;

namespace Hermes.Application.Interfaces;

public interface IRequestExecutionService
{
    Task<Response> ExecuteRequestAsync(Request req, CancellationToken token);
}