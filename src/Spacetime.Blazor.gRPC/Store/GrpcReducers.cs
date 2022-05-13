using Fluxor;

namespace Spacetime.Blazor.gRPC.Store;

public static class GrpcReducers
{
    [ReducerMethod]
    public static GrpcState FetchServicesSuccess(GrpcState state, FetchServicesSuccessAction action)
    {
        return new GrpcState
        {
            Services = action.Services
        };
    }
}