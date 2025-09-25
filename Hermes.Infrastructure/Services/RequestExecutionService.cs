using System.Net;
using Hermes.Application.Interfaces;
using Hermes.Domain.Models;
using Hermes.Infrastructure.Utils;

namespace Hermes.Infrastructure.Services;

public class RequestExecutionService(IHttpClientFactory clientFactory) : IRequestExecutionService
{
    public async Task<Response> ExecuteRequestAsync(Request req, CancellationToken token)
    {
        var method = req.Method.ToHttpMethod();
        using var client = clientFactory.CreateClient();
        using var request = new HttpRequestMessage(method, req.ToUri());
        foreach (var reqHeader in req.Headers)
        {
            request.Headers.Add(reqHeader.Key, reqHeader.Value);
        }

        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using var response = await client.SendAsync(request, token);
            watch.Stop();
            var elapsed = watch.Elapsed;
            var headers = response.Headers
                .Select(h => new Header(h.Key, h.Value?.ToString() ?? string.Empty))
                .ToList()
                .AsReadOnly();
            return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync(token), headers, elapsed);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while executing the request {ex}");
            return new Response((int)HttpStatusCode.InternalServerError, ex.Message, [], TimeSpan.Zero);
        }
    }
}