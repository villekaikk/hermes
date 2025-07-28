using Hermes.Domain.Models;

namespace Hermes.Application.Interfaces;

public interface IRequestExecutionService
{
    Task ExecuteRequestAsync(RequestOptions options, CancellationToken token);
}