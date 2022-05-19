using Google.Protobuf.Reflection;

namespace Spacetime.Core.gRPC
{
    public class GrpcServiceDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<GrpcMethodDefinition> Methods { get; set; } = new();

    }
}