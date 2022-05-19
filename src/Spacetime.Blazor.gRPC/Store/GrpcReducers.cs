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

    [ReducerMethod]
    public static GrpcState SaveServices(GrpcState state, SaveServicesAction action)
    {
        return new GrpcState
        {
            IsSaving = true,
            Services = state.Services
        };
    }

    [ReducerMethod]
    public static GrpcState SaveServicesComplete(GrpcState state, SaveServicesCompleteAction action)
    {
        return new GrpcState
        {
            IsSaving = false,
            Services = state.Services
        };
    }
}