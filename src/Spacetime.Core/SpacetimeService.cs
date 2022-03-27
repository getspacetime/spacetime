using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Core
{
    public class SpacetimeService
    {
        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            var response = new SpacetimeResponse();
            var timer = Stopwatch.StartNew();

            try
            {
                var client = new HttpClient();
                var httpResponse = await client.GetAsync(request.URL);
                response.ResponseBody = await httpResponse.Content.ReadAsStringAsync();
                response.Headers = httpResponse.Headers.Select(p => new HeaderDto { Name = p.Key, Value = string.Join(';', p.Value) });
                timer.Stop();

                response.Status = SpacetimeStatus.Ok;
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
    }

    public class SpacetimeResponse
    {
        public IEnumerable<HeaderDto> Headers { get; set; }
        public SpacetimeStatus Status { get; set; }
        public string ResponseBody { get; set; }
        public long ElapsedMs { get; set; }
    }

    public enum SpacetimeStatus
    {
        Unknown,
        Ok,
        Error
    }

    public class HeaderDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
