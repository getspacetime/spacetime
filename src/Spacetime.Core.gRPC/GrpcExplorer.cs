using System.Collections;
using System.Diagnostics;
using System.Text.Json.Nodes;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Spacetime.Core.Formatters;
using Spacetime.Core.gRPC.Dynamic;
using Spacetime.Core.gRPC.Interfaces;

namespace Spacetime.Core.gRPC
{
    public class GrpcExplorer : IGrpcExplorer
    {
        private readonly JsonFormatter _formatter;
        private readonly ILogger<GrpcExplorer> _log;
        private readonly IDynamicGrpcFactory _factory;

        public GrpcExplorer(ILogger<GrpcExplorer> log, IDynamicGrpcFactory factory, JsonFormatter formatter)
        {
            _log = log;
            _factory = factory;
            _formatter = formatter;
        }

        public async Task<GrpcResponse> Invoke(string host, string service, string method, string json)
        {
            _log.LogInformation("Invoking service {host}, {service}, {method}", host, service, method);
            var response = new GrpcResponse() { Status = GrpcStatus.Active };
            Stopwatch stopwatch = null;
            try
            {
                using var channel = GrpcChannel.ForAddress(host);
                var client = await _factory.FromServerReflection(channel);

                var data = ParseJson(json);
                var input = ParseInput(data);

                stopwatch = Stopwatch.StartNew();
                var jsonResponse = string.Empty;
                await foreach (var result in client.AsyncDynamicCall(service, method, ToAsync(input)))
                {
                    jsonResponse = ToJson(result)!.ToJsonString();
                }
                stopwatch.Stop();

                if (!string.IsNullOrWhiteSpace(jsonResponse))
                {
                    jsonResponse = _formatter.Format(jsonResponse);
                }

                response.Status = GrpcStatus.Ok;
                response.ResponseBody = jsonResponse;
                response.ElapsedMs = stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Failed to invoke service {host}, {service}, {method}", host, service, method);

                response.ResponseBody = ex.Message;
                response.Status = GrpcStatus.Error;
                response.ElapsedMs = stopwatch?.ElapsedMilliseconds ?? -1;
            }

            return response;
        }

        // thanks & credit to: https://github.com/xoofx/grpc-curl
        private static List<IDictionary<string, object>> ParseInput(object data)
        {
            // Parse Input
            var input = new List<IDictionary<string, object>>();

            if (data is IEnumerable<object> it)
            {
                int index = 0;
                foreach (var item in it)
                {

                    if (item is IDictionary<string, object> dict)
                    {
                        input.Add(dict);
                    }
                    else
                    {
                        throw new ApplicationException($"Invalid type `{item?.GetType()?.FullName}` from the input array at index [{index}]. Expecting an object.");
                    }
                    index++;
                }
            }
            else if (data is IDictionary<string, object> dict)
            {
                input.Add(dict);
            }
            else
            {
                throw new ApplicationException($"Invalid type `{data?.GetType()?.FullName}` from the input. Expecting an object.");
            }

            return input;
        }

        // thanks & credit to: https://github.com/xoofx/grpc-curl
        private static JsonNode? ToJson(object? requestObject)
        {
            switch (requestObject)
            {
                case int i32: return JsonValue.Create(i32);
                case uint u32: return JsonValue.Create(u32);
                case long i64: return JsonValue.Create(i64);
                case ulong u64: return JsonValue.Create(u64);
                case float f32: return JsonValue.Create(f32);
                case double f64: return JsonValue.Create(f64);
                case short i16: return JsonValue.Create(i16);
                case ushort u16: return JsonValue.Create(u16);
                case sbyte i8: return JsonValue.Create(i8);
                case byte u8: return JsonValue.Create(u8);
                case bool b: return JsonValue.Create(b);
                case string str:
                    return JsonValue.Create(str);
                case IDictionary<string, object> obj: // objects become Dictionary<string,object>
                    var jsonObject = new JsonObject();
                    foreach (var kp in obj)
                    {
                        jsonObject.Add(kp.Key, ToJson(kp.Value));
                    }
                    return jsonObject;
                case IEnumerable array: // arrays become List<object>
                    var jsonArray = new JsonArray();
                    foreach (var o in array)
                    {
                        jsonArray.Add(ToJson(o));
                    }

                    return jsonArray;
                case KeyValuePair<object, object> pair:
                    return JsonValue.Create(pair);
                default: // don't know what to do here
                    return null;
            }
        }

        // thanks & credit to: https://github.com/xoofx/grpc-curl
        private static async IAsyncEnumerable<IDictionary<string, object>> ToAsync(IEnumerable<IDictionary<string, object>> input)
        {
            foreach (var item in input)
            {
                yield return await ValueTask.FromResult(item);
            }
        }

        // thanks & credit to: https://github.com/xoofx/grpc-curl
        private static object? ParseJson(string data)
        {
            try
            {
                var json = JsonNode.Parse(data);
                return ToApiRequest(json);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failing to deserialize JSON data. Reason: {ex.Message}.");
            }
        }

        // thanks & credit to: https://github.com/xoofx/grpc-curl
        private static object? ToApiRequest(JsonNode? requestObject)
        {
            switch (requestObject)
            {
                case JsonObject jObject: // objects become Dictionary<string,object>
                    return jObject.ToDictionary(j => j.Key, j => ToApiRequest(j.Value));
                case JsonArray jArray: // arrays become List<object>
                    return jArray.Select(ToApiRequest).ToList();
                case JsonValue jValue: // values just become the value
                    return jValue.GetValue<object>();
                case null:
                    return null;
                default: // don't know what to do here
                    throw new Exception($"Unsupported type: {requestObject.GetType()}");
            }
        }

        public async Task<GrpcExploreResult> Explore(string host)
        {
            using var channel = GrpcChannel.ForAddress(host);
            var client = await _factory.FromServerReflection(channel);
            var result = await client.Explore();

            // the .NET server reflection for gRPC shows up as a service
            // named ServerReflection - remove this from the list to avoid clutter
            result.Services = result.Services
                .Where(p => !p.Name.Equals("ServerReflection", StringComparison.OrdinalIgnoreCase)).ToList();

            result.Services.ForEach(svc =>
            {
                svc.Id = Guid.NewGuid();

                svc.Methods.ForEach(method =>
                {
                    method.Id = Guid.NewGuid();
                });
            });

            return result;
        }
    }
}