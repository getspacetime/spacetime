using Spacetime.Core.Infrastructure;

namespace Spacetime.Core
{
    public class SpacetimeGrpcService : ISpacetimeService
    {
        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request, ResponseOptions options = null)
        {
            var response = new SpacetimeResponse
            {
                ElapsedMs = 10,
                Headers = new List<HeaderDto>(),
                Status = SpacetimeStatus.Ok,
                StatusCode = "200 OK"
            };

            try
            {
                response.ResponseBody = "not implemented";
            } catch (Exception ex)
            {
                response.ResponseBody = ex.Message;
                response.Status = SpacetimeStatus.Error;
                response.StatusCode = "500 ERR";
            }


            return response;
        }
    }
}
