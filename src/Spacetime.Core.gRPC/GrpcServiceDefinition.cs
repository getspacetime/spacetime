namespace Spacetime.Core.gRPC
{
    public class GrpcServiceDefinition
    {
        public string Name { get; set; }
        public List<GrpcMethodDefinition> Methods { get; set; }
    }
}