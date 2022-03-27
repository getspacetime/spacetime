namespace Spacetime.Core
{
    public class SpacetimeGrpcService : ISpacetimeService
    {
        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            return new SpacetimeResponse
            {
                ElapsedMs = 10,
                Headers = new List<HeaderDto>(),
                ResponseBody = "gRPC response",
                Status = SpacetimeStatus.Ok,
                StatusCode = "200 OK"
            };
        }
    }
}
