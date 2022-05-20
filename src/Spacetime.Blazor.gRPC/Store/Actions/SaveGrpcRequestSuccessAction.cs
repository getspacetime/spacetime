using Spacetime.Core.gRPC;

namespace Spacetime.Blazor.gRPC.Store.Actions;

public class SaveGrpcRequestSuccessAction
{
    public GrpcRequest Request { get; set; }
}