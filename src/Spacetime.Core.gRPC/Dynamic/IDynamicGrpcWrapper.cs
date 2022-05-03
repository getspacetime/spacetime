using Grpc.Core;

namespace Spacetime.Core.gRPC.Dynamic;

public interface IDynamicGrpcWrapper
{
    Task<IDictionary<string, object>> AsyncUnaryCall(string serviceName, string methodName,
        IDictionary<string, object> request);

    Task<GrpcExploreResult> Explore();
}