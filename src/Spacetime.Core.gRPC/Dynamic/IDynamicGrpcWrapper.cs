using Grpc.Core;

namespace Spacetime.Core.gRPC.Dynamic;

public interface IDynamicGrpcWrapper
{
    Task<IDictionary<string, object>> AsyncUnaryCall(string serviceName, string methodName,
        IDictionary<string, object> request);

    IAsyncEnumerable<IDictionary<string, object>> AsyncDynamicCall(string serviceName, string methodName,
        IAsyncEnumerable<IDictionary<string, object>> input, string? host = null, CallOptions? options = null);
    Task<GrpcExploreResult> Explore();
}