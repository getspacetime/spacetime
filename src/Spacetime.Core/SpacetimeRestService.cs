using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Spacetime.Core.Infrastructure;
namespace Spacetime.Core
{

    public class SpacetimeRestService : ISpacetimeService
    {
        private readonly ILogger<SpacetimeRestService> _log;
        private readonly HttpClient _client;
        private readonly UrlBuilder _urlBuilder;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private readonly Dictionary<string, HttpMethod> _methods = new Dictionary<string, HttpMethod>
        {
            {"GET", HttpMethod.Get },
            {"POST", HttpMethod.Post }
        };

        public SpacetimeRestService(ILogger<SpacetimeRestService> log, HttpClient client, UrlBuilder urlBuilder)
        {
            _log = log;
            _client = client;
            _urlBuilder = urlBuilder;
        }

        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            _log.LogInformation("Executing request {id} with method {method}", request.Id, request.Method);

            var response = new SpacetimeResponse();
            var timer = Stopwatch.StartNew();

            try
            {
                var message = BuildHttpRequest(request);
                var httpResponse = await _client.SendAsync(message);
                timer.Stop();

                _log.LogInformation("Executed request {id} with method {method} in {time}", request.Id, request.Method, timer.ElapsedMilliseconds);

                var responseJson = await httpResponse.Content.ReadAsStringAsync();
                response.ResponseBody = PrettyPrintJson(responseJson);
                response.Headers = httpResponse.Headers.Select(p => new HeaderDto { Name = p.Key, Value = string.Join(';', p.Value) });
                response.Status = httpResponse.IsSuccessStatusCode ? SpacetimeStatus.Ok : SpacetimeStatus.Error;
                response.StatusCode = httpResponse.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                timer.Stop();

                _log.LogError(ex, "Error executing SpacetimeRequest {id}", request.Id);

                response.Status = SpacetimeStatus.Error;
                response.ResponseBody = $"Unknown error {ex.Message}";
            }
            finally
            {
                response.ElapsedMs = timer.ElapsedMilliseconds;
            }

            return response;
        }

        private string PrettyPrintJson(string json)
        {
            _log.LogInformation("Beautifying JSON response");

            return (JsonSerializer.Serialize(
                  JsonSerializer.Deserialize<object>(json), _jsonOptions));
        }

        private HttpRequestMessage BuildHttpRequest(SpacetimeRequest request)
        {
            _log.LogInformation("Building HTTP Request for request {id}", request.Id);

            var url = _urlBuilder.GetUrl(request);
            var message = new HttpRequestMessage
            {
                RequestUri = new Uri(url),

                // will fail if we add more methods without updating map
                // maybe just switch to storing HttpMethod or another enum later
                Method = _methods[request.Method],
            };

            if (!string.IsNullOrWhiteSpace(request.RequestBody))
            {
                message.Content = new StringContent(request.RequestBody);
            }

            foreach (var header in request.Headers)
            {
                message.Headers.Add(header.Name, header.Value);
            }

            return message;
        }
    }
}
