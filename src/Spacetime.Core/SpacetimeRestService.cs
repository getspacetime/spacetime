using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Spacetime.Core.Formatters;
using Spacetime.Core.Services;
using Spacetime.Core.Shared;

namespace Spacetime.Core
{
    public class SpacetimeRestService : ISpacetimeService
    {
        private readonly ILogger<SpacetimeRestService> _log;
        private readonly HttpClient _client;
        private readonly UrlBuilder _urlBuilder;
        private readonly ResponseOptions _defaultResponseOptions = new ();

        private readonly IFormatterFactory _formatter;

        public SpacetimeRestService(
            ILogger<SpacetimeRestService> log, 
            HttpClient client, 
            UrlBuilder urlBuilder, 
            IFormatterFactory formatter)
        {
            _log = log;
            _client = client;
            _urlBuilder = urlBuilder;
            _formatter = formatter;
        }

        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request, ResponseOptions options = null)
        {
            options ??= _defaultResponseOptions;

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

                if (options.Pretty)
                {
                    var formatter = _formatter.Get(GetFormatterType(httpResponse));
                    response.ResponseBody = formatter.Format(responseJson);
                }

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

        private FormatterType GetFormatterType(HttpResponseMessage message)
        {
            // todo: get the content type and use this to determine formatter
            // todo: allow override of formatter using RequestOptions
            return FormatterType.Json;
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
                Method = SupportedHttpMethods.AllMethods[request.Method]
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
