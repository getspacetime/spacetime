using DynamicGrpc;
using Grpc.Core;

namespace Spacetime.Core.gRPC.Dynamic;

public interface IDynamicGrpcFactory
{
    Task<IDynamicGrpcWrapper> FromServerReflection(ChannelBase channel, DynamicGrpcClientOptions options = null,
        int timeoutInMillis = 10000, CancellationToken cancellationToken = default);
}