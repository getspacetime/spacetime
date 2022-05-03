using DynamicGrpc;
using Grpc.Core;
using Grpc.Net.Client;

namespace Spacetime.Core.gRPC.Dynamic;

public class DynamicGrpcFactory : IDynamicGrpcFactory
{
    public async Task<IDynamicGrpcWrapper> FromServerReflection(ChannelBase channel, DynamicGrpcClientOptions options = null, int timeoutInMillis = 10000,
        CancellationToken cancellationToken = default)
    {
        var client = await DynamicGrpcClient.FromServerReflection(channel, options, timeoutInMillis, cancellationToken);

        return new DynamicGrpcWrapper(client);
    }
}