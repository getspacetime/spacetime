using Grpc.Net.Client;
using Grpc.Core;
using grpc = global::Grpc.Core;

namespace Spacetime.Core
{
    public class SpacetimeGrpcService : ISpacetimeService
    {
        public async Task<SpacetimeResponse> Execute(SpacetimeRequest request)
        {
            var response = new SpacetimeResponse
            {
                ElapsedMs = 10,
                Headers = new List<HeaderDto>(),
                Status = SpacetimeStatus.Ok,
                StatusCode = "200 OK"
            };

            try
            {
               // var reply = await Actual(request);
                var reply = await Test(request);

                response.ResponseBody = reply.Message;
            } catch (Exception ex)
            {
                response.ResponseBody = ex.Message;
                response.Status = SpacetimeStatus.Error;
                response.StatusCode = "500 ERR";
            }


            return response;
        }

        private async Task<HelloReply?> Actual(SpacetimeRequest request)
        {
            using var channel = GrpcChannel.ForAddress(request.URL);
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = request.Name });
            return reply;
        }

        private async Task<HelloReply?> Test(SpacetimeRequest request)
        {
            var __Marshaller_greet_HelloRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Spacetime.Core.HelloRequest.Parser));
            var __Marshaller_greet_HelloReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Spacetime.Core.HelloReply.Parser));

            var serviceName = "greet.Greeter";
            var method = new grpc::Method<global::Spacetime.Core.HelloRequest, global::Spacetime.Core.HelloReply>(
        grpc::MethodType.Unary,
        serviceName,
        "SayHello",
        __Marshaller_greet_HelloRequest,
        __Marshaller_greet_HelloReply);

            using var channel = GrpcChannel.ForAddress(request.URL);
            var callInvoker = channel.CreateCallInvoker();
            return callInvoker.BlockingUnaryCall(method, null, new CallOptions { }, new HelloRequest { Name = request.Name });
        }

        void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
        {
#if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
            if (message is global::Google.Protobuf.IBufferMessage)
            {
                context.SetPayloadLength(message.CalculateSize());
                global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
                context.Complete();
                return;
            }
#endif
            context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
        }
         T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
        {
#if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
            if (SpacetimeGrpcService.__Helper_MessageCache<T>.IsBufferMessage)
            {
                return parser.ParseFrom(context.PayloadAsReadOnlySequence());
            }
#endif
            return parser.ParseFrom(context.PayloadAsNewBuffer());
        }
        static class __Helper_MessageCache<T>
        {
            public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
        }
    }
}
