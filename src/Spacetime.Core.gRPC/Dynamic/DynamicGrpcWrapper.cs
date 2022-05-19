using DynamicGrpc;

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
