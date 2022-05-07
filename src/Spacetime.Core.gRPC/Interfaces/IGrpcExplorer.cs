using Spacetime.Core.Infrastructure;

namespace Spacetime.Core.gRPC.Interfaces
{
    public interface IGrpcExplorer
    {
        /// <summary>
        /// Invoke the gRPC method using JSON
        /// </summary>
        /// <param name="host">The service URL, e.g. https://localhost:2187</param>
        /// <param name="service">The name of the service, e.g. greet.Greeter</param>
        /// <param name="method">The name of the method, e.g. SayHello</param>
        /// <param name="json">The response body as JSON</param>
        /// <returns>The message formatted as JSON</returns>
        Task<SpacetimeResponse> Invoke(string host, string service, string method, string json);
        GrpcExploreResult GetExplorer(string importPath, string protoFileName);

        Task<GrpcExploreResult> Explore(string host);
    }
}