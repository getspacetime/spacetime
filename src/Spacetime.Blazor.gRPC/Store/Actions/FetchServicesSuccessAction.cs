using Spacetime.Core.gRPC;

namespace Spacetime.Blazor.gRPC.Store.Actions;

public class FetchServicesSuccessAction
{
    public List<GrpcServiceDefinition> Services { get; set; } = new();
    public List<GrpcRequest> Requests { get; set; } = new();
}