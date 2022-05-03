using System.Diagnostics;
using System.Text.Json;
using DynamicGrpc;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Spacetime.gRPC.Wrapper;
using Spacetime.Core.Infrastructure;

namespace Spacetime.Core.gRPC
{
    public class GrpcExplorer : IGrpcExplorer
    {
        private readonly ILogger<GrpcExplorer> _log;
        public GrpcExplorer(ILogger<GrpcExplorer> log)
        {
            _log = log;
        }

        public GrpcExploreResult GetExplorer(string importPath, string protoFileName)
        {
            var explorer = new GrpcExploreResult();
            explorer.Services = ListServices(importPath, protoFileName).Select(p => new GrpcServiceDefinition { Name = p }).ToList();
            foreach (var svc in explorer.Services)
            {
                svc.Methods = ListMethods(importPath, protoFileName, svc.Name).Select(p => new GrpcMethodDefinition { Name = p }).ToList();
            }

            return explorer;
        }

        public async Task<SpacetimeResponse> Invoke(string host, string service, string method, string json)
        {
            _log.LogInformation("Invoking service {host}, {service}, {method}", host, service, method);
            var response = new SpacetimeResponse {Status = SpacetimeStatus.Active};
            Stopwatch stopwatch = null;
            try
            {
                using var channel = GrpcChannel.ForAddress(host);
                var client = await DynamicGrpcClient.FromServerReflection(channel);

                var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                stopwatch = Stopwatch.StartNew();
                var result = await client.AsyncUnaryCall(service, method,
                    parameters ?? new Dictionary<string, object>());
                stopwatch.Stop();

                var resultJson = JsonSerializer.Serialize(result);

                response.Status = SpacetimeStatus.Ok;
                response.ResponseBody = resultJson;
                response.ElapsedMs = stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Failed to invoke service {host}, {service}, {method}", host, service, method);

                response.ResponseBody = ex.Message;
                response.Status = SpacetimeStatus.Error;
                response.ElapsedMs = stopwatch?.ElapsedMilliseconds ?? -1;
            }

            return response;
        }

        public IEnumerable<string> ListServices(string importPath, string protoFileName)
        {
            var curl = new GRPCurl();
            var result = curl.ListServices(importPath, protoFileName);
            return result.Items.Select(p => p.Name);
        }

        public IEnumerable<string> ListMethods(string importPath, string protoFileName, string svc)
        {
            var curl = new GRPCurl();
            var result = curl.ListMethods(importPath, protoFileName, svc);
            return result.Items.Select(p => p.Name);
        }
    }
}