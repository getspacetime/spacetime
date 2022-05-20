using Spacetime.Core.gRPC;

namespace Spacetime.Blazor.gRPC.Store.Actions;

public class SaveServicesAction
{
    public List<GrpcServiceDefinition> Services { get; set; } = new();
}