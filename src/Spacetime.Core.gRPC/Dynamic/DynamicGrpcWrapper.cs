using DynamicGrpc;
using Grpc.Core;

namespace Spacetime.Core.gRPC.Dynamic
{
    public class DynamicGrpcWrapper : IDynamicGrpcWrapper
    {
        private readonly DynamicGrpcClient _client;

        public DynamicGrpcWrapper(DynamicGrpcClient client)
        {
            _client = client;
        }

        public async Task<IDictionary<string, object>> AsyncUnaryCall(string serviceName, string methodName,
            IDictionary<string, object> request)
        {
            return await _client.AsyncUnaryCall(serviceName, methodName, request, null, null);
        }

        public IAsyncEnumerable<IDictionary<string, object>> AsyncDynamicCall(string serviceName, string methodName,
            IAsyncEnumerable<IDictionary<string, object>> input, string? host = null, CallOptions? options = null)
        {
            return _client.AsyncDynamicCall(serviceName, methodName, input, host, options);
        }

        public Task<GrpcExploreResult> Explore()
        {
            var result = new GrpcExploreResult();
            var services = _client.Files.SelectMany(p => p.Services.Select(s => s));
            foreach (var service in services)
            {
                var methods = service.Methods.Select(p => new GrpcMethodDefinition
                {
                    Name = p.Name,
                    IsClientStreaming = p.IsClientStreaming,
                    IsServerStreaming = p.IsServerStreaming,
                    FullName = p.FullName
                }).ToList();
                var item = new GrpcServiceDefinition
                {
                    Name = service.Name,
                    FullName = service.FullName,
                    Methods = methods
                };

                result.Services.Add(item);
            }

            return Task.FromResult(result);
        }
    }
}
