using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicGrpc;
using Grpc.Core;
using Spacetime.Core.Infrastructure;

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
                var methods = service.Methods.Select(p => new GrpcMethodDefinition {Name = p.FullName}).ToList();
                var item = new GrpcServiceDefinition
                {
                    Name = service.Name,
                    Methods = methods
                };

                result.Services.Add(item);
            }

            return Task.FromResult(result);
        }
    }
}
