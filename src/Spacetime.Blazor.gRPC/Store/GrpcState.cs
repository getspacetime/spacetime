using Fluxor;
using Spacetime.Core.gRPC;

namespace Spacetime.Blazor.gRPC.Store
{
    [FeatureState]
    public class GrpcState
    {
        public bool IsSaving { get; set; }
        public List<GrpcServiceDefinition> Services { get; set; } = new();
        public List<GrpcRequest> Requests { get; set; } = new();
    }
}
