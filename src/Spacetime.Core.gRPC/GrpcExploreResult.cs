using Spacetime.Core.Infrastructure;

namespace Spacetime.Core.gRPC
{
    public class GrpcExploreResult
    {
        public List<GrpcServiceDefinition> Services { get; set; } = new ();
    }
}