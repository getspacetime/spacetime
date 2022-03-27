using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Core
{

    public class SpacetimeRestService : ISpacetimeService
    {
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
            var client = new HttpClient();
            var httpResponse = await client.GetAsync(request.URL);

            return httpResponse;
        }

        private async Task<HttpResponseMessage> Post(SpacetimeRequest request)
        {
            var client = new HttpClient();

            StringContent? content = null;

            if (!string.IsNullOrWhiteSpace(request.RequestBody))
            {
                content = new StringContent(request.RequestBody);
            }

            var httpResponse = await client.PostAsync(request.URL, content);

            return httpResponse;
        }
    }
}
