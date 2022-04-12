using System.Diagnostics;
using Spacetime.Core.Infrastructure;
namespace Spacetime.Core
{

    public class SpacetimeRestService : ISpacetimeService
    {
        private readonly UrlBuilder _urlBuilder;

        public SpacetimeRestService(UrlBuilder urlBuilder)
        {
            _urlBuilder = urlBuilder;
        }

        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            var response = new SpacetimeResponse();
            var timer = Stopwatch.StartNew();

            try
            {
                HttpResponseMessage httpResponse;

                switch (request.Method)
                {
                    case "GET":
                        httpResponse = await Get(request);
                        break;
                    case "POST":
                        httpResponse = await Post(request);
                        break;
                    default:
                        httpResponse = await Get(request);
                        break;
                }
                
                timer.Stop();

                response.ResponseBody = await httpResponse.Content.ReadAsStringAsync();
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

        private async Task<HttpResponseMessage> Get(SpacetimeRequest request)
        {
            var client = GetClient(request);
            var url = _urlBuilder.GetUrl(request);
            var httpResponse = await client.GetAsync(url);

            return httpResponse;
        }

        private async Task<HttpResponseMessage> Post(SpacetimeRequest request)
        {
            var client = GetClient(request);

            StringContent? content = null;

            if (!string.IsNullOrWhiteSpace(request.RequestBody))
            {
                content = new StringContent(request.RequestBody);
            }

            var url = _urlBuilder.GetUrl(request);
            var httpResponse = await client.PostAsync(url, content);

            return httpResponse;
        }

        private HttpClient GetClient(SpacetimeRequest request)
        {
            // todo: use ioc container/factory extensions
            var client = new HttpClient();

            foreach (var header in request.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Name, header.Value);
            }

            return client;
        }
    }
}
