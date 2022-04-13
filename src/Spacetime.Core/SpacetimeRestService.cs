using System.Diagnostics;
using System.Text.Json;
using Spacetime.Core.Infrastructure;
namespace Spacetime.Core
{

    public class SpacetimeRestService : ISpacetimeService
    {
        private readonly HttpClient _client;
        private readonly UrlBuilder _urlBuilder;
        private readonly Dictionary<string, HttpMethod> _methods = new Dictionary<string, HttpMethod>
        {
            {"GET", HttpMethod.Get },
            {"POST", HttpMethod.Post }
        };

        public SpacetimeRestService(HttpClient client, UrlBuilder urlBuilder)
        {
            _client = client;
            _urlBuilder = urlBuilder;
        }

        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            var response = new SpacetimeResponse();
            var timer = Stopwatch.StartNew();

            try
            {
                var message = BuildHttpRequest(request);
                var httpResponse = await _client.SendAsync(message);

                timer.Stop();

                response.ResponseBody = await httpResponse.Content.ReadAsStringAsync();
                response.ResponseBody = (JsonSerializer.Serialize(
                  JsonSerializer.Deserialize<object>(response.ResponseBody), options: new JsonSerializerOptions
                  {
                      WriteIndented = true
                  }));
                response.Headers = httpResponse.Headers.Select(p => new HeaderDto { Name = p.Key, Value = string.Join(';', p.Value) });
                response.Status = httpResponse.IsSuccessStatusCode ? SpacetimeStatus.Ok : SpacetimeStatus.Error;
                response.StatusCode = httpResponse.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                timer.Stop();
                response.Status = SpacetimeStatus.Error;
                response.ResponseBody = $"Unknown error {ex.Message}";
            }
            finally
            {
                response.ElapsedMs = timer.ElapsedMilliseconds;
            }

            return response;
        }

        private HttpRequestMessage BuildHttpRequest(SpacetimeRequest request)
        {
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
